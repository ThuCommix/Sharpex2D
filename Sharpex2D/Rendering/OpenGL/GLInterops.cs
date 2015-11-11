// Copyright (c) 2012-2015 Sharpex2D - Kevin Scholz (ThuCommix)
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the 'Software'), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED 'AS IS', WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Security;

namespace Sharpex2D.Framework.Rendering.OpenGL
{
    [SuppressUnmanagedCodeSecurity]
    internal static class GLInterops
    {
        #region WGL

        /// <summary>
        /// Make the specified render context current.
        /// </summary>
        /// <param name="hdc">The handle to the device context.</param>
        /// <param name="hrc">The handle to the render context.</param>
        /// <returns></returns>
        [DllImport("opengl32.dll")]
        public static extern int wglMakeCurrent(IntPtr hdc, IntPtr hrc);

        /// <summary>
        /// Creates a render context from the device context.
        /// </summary>
        /// <param name="hdc">The handle to the device context.</param>
        /// <returns>The handle to the render context.</returns>
        [DllImport("opengl32.dll", SetLastError = true)]
        public static extern IntPtr wglCreateContext(IntPtr hdc);

        /// <summary>
        /// Deletes the render context.
        /// </summary>
        /// <param name="hrc">The handle to the render context.</param>
        /// <returns></returns>
        [DllImport("opengl32.dll")]
        public static extern int wglDeleteContext(IntPtr hrc);

        /// <summary>
        /// Gets a pointer to the requested function.
        /// </summary>
        /// <param name="name">The Title.</param>
        /// <returns>IntPtr.</returns>
        [DllImport("opengl32.dll", SetLastError = true)]
        public static extern IntPtr wglGetProcAddress(string name);

        #endregion

        #region GDI

        /// <summary>
        /// Chooses the PixelFormat.
        /// </summary>
        /// <param name="deviceContext">The DeviceContext.</param>
        /// <param name="pixelFormatDescriptor">The PixelFormatDescriptor.</param>
        /// <returns>True on success.</returns>
        [DllImport("gdi32.dll", SetLastError = true)]
        public static extern int ChoosePixelFormat(IntPtr deviceContext, ref PixelFormatDescriptor pixelFormatDescriptor);

        /// <summary>
        /// Sets the PixelFormat.
        /// </summary>
        /// <param name="deviceContext">The DeviceContext.</param>
        /// <param name="pixelFormat">The PixelFormat.</param>
        /// <param name="pixelFormatDescriptor">The PixelFormatDescriptor.</param>
        /// <returns>True on success.</returns>
        [DllImport("gdi32.dll", EntryPoint = "SetPixelFormat", SetLastError = true)]
        public static extern bool SetPixelFormat(IntPtr deviceContext, int pixelFormat,
            ref PixelFormatDescriptor pixelFormatDescriptor);

        /// <summary>
        /// Swaps the buffers.
        /// </summary>
        /// <param name="hdc">The HDC.</param>
        /// <returns>Int32.</returns>
        [DllImport("gdi32.dll")]
        public static extern int SwapBuffers(IntPtr hdc);

        /// <summary>
        /// Gets the DC.
        /// </summary>
        /// <param name="hWnd">The WindowHandle.</param>
        /// <returns>Handle to DC.</returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr GetDC(IntPtr hWnd);

        /// <summary>
        /// Releases the DC.
        /// </summary>
        /// <param name="hWnd">The WindowHandle.</param>
        /// <param name="hdc">The HDC.</param>
        /// <returns>Int32.</returns>
        [DllImport("user32.dll")]
        public static extern int ReleaseDC(IntPtr hWnd, IntPtr hdc);

        #endregion

        #region OpenGL32

        #region Delegates

        private delegate void glActiveTexture(uint texture);

        private delegate void glAttachShader(uint program, uint shader);

        private delegate void glBindBuffer(uint target, uint buffer);

        private delegate void glBindFragDataLocation(uint program, uint color, string name);

        private delegate void glBindVertexArray(uint array);

        private delegate void glBufferData(uint target, int size, IntPtr data, uint usage);

        private delegate void glCompileShader(uint shader);

        private delegate uint glCreateProgram();

        private delegate uint glCreateShader(uint type);

        private delegate void glDeleteBuffers(int n, uint[] buffers);

        private delegate void glDeleteProgram(uint program);

        private delegate void glDeleteShader(uint shader);

        private delegate void glDeleteVertexArrays(int n, uint[] arrays);

        private delegate void glEnableVertexAttribArray(uint index);

        private delegate void glGenBuffers(int n, uint[] buffers);

        private delegate void glGenVertexArrays(int n, uint[] arrays);

        private delegate int glGetAttribLocation(uint program, string name);

        private delegate int glGetUniformLocation(uint program, string name);

        private delegate void glLinkProgram(uint program);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, ThrowOnUnmappableChar = true)]
        private delegate void glShaderSource(uint shader, int count, string[] source, int[] length);

        private delegate void glUniform1f(int location, float v0);

        private delegate void glUniform2f(int location, float v0, float v1);

        private delegate void glUniform3f(int location, float v0, float v1, float v2);

        private delegate void glUniform4f(int location, float v0, float v1, float v2, float v3);

        private delegate void glUniformMatrix4fv(int location, int count, bool transpose, float[] value);

        private delegate void glUniform1i(int location, int v0);

        private delegate void glUniform2i(int location, int v0);

        private delegate void glUniform3i(int location, int v0);

        private delegate void glUniform4i(int location, int v0);

        private delegate void glUseProgram(uint program);

        private delegate void glVertexAttribPointer(
            uint index, int size, uint type, bool normalized, int stride, IntPtr pointer);

        private delegate IntPtr wglCreateContextAttribsARB(IntPtr hdc, IntPtr hShareContext, int[] attribList);

        #endregion

        private static Dictionary<string, Delegate> _extensions;

        /// <summary>
        /// Invokes an extension method and return it's result.
        /// </summary>
        /// <typeparam name="T">The delegate type.</typeparam>
        /// <param name="parameters">The Parameters.</param>
        /// <returns>Object.</returns>
        public static object InvokeExtensionMethod<T>(params object[] parameters)
        {
            if (_extensions == null) _extensions = new Dictionary<string, Delegate>();

            Type delegateType = typeof (T);
            if (!_extensions.ContainsKey(delegateType.Name))
            {
                IntPtr hfunc = wglGetProcAddress(delegateType.Name);
                if (hfunc == IntPtr.Zero)
                {
                    throw new MissingMethodException(delegateType.Name);
                }

                Delegate del = Marshal.GetDelegateForFunctionPointer(hfunc, delegateType);
                _extensions.Add(delegateType.Name, del);
            }

            return _extensions[delegateType.Name].DynamicInvoke(parameters);
        }

        /// <summary>
        /// Creates a Context with Attributes.
        /// </summary>
        /// <param name="hdc">The HDC.</param>
        /// <param name="hShareContext">The SharedContextHandle.</param>
        /// <param name="attribList">The AttributeList.</param>
        /// <returns>A handle to the elevated context.</returns>
        public static IntPtr CreateContextWithAttributes(IntPtr hdc, IntPtr hShareContext, int[] attribList)
        {
            return (IntPtr) InvokeExtensionMethod<wglCreateContextAttribsARB>(hdc, hShareContext, attribList);
        }

        #endregion

        #region DllImport

        [DllImport("opengl32.dll", SetLastError = true)]
        private static extern void glDisable(uint cap);

        [DllImport("opengl32.dll", SetLastError = true)]
        private static extern void glTexSubImage2D(uint target, int level, int xoffset, int yoffset, int width,
            int height, uint format, uint type, IntPtr pixels);

        [DllImport("opengl32.dll", SetLastError = true)]
        private static extern void glGetTexImage(uint target, int level, uint format, uint type, IntPtr pixels);

        [DllImport("opengl32.dll", SetLastError = true)]
        private static extern void glBindTexture(uint target, uint texture);

        [DllImport("opengl32.dll", SetLastError = true)]
        private static extern void glBlendFunc(uint sfactor, uint dfactor);

        [DllImport("opengl32.dll", SetLastError = true)]
        private static extern void glClear(uint mask);

        [DllImport("opengl32.dll", SetLastError = true)]
        private static extern void glClearColor(float red, float green, float blue, float alpha);

        [DllImport("opengl32.dll", SetLastError = true)]
        private static extern void glDrawElements(uint mode, int count, uint type, IntPtr indices);

        [DllImport("opengl32.dll", SetLastError = true)]
        private static extern void glEnable(uint cap);

        [DllImport("opengl32.dll", SetLastError = true)]
        private static extern void glGenTextures(int n, uint[] textures);

        [DllImport("opengl32.dll", SetLastError = true)]
        private static extern uint glGetError();

        [DllImport("opengl32.dll", SetLastError = true)]
        private static extern void glTexImage2D(uint target, int level, uint internalformat, int width, int height,
            int border, uint format, uint type, IntPtr pixels);

        [DllImport("opengl32.dll", SetLastError = true)]
        private static extern void glTexParameterf(uint target, uint pname, float param);

        [DllImport("opengl32.dll", SetLastError = true)]
        private static extern void glTexParameteri(uint target, uint pname, int param);

        [DllImport("opengl32.dll", SetLastError = true)]
        private static extern void glViewport(int x, int y, int width, int height);

        [HandleProcessCorruptedStateExceptions]
        [DllImport("opengl32.dll", SetLastError = true)]
        private static extern void glDeleteTextures(int n, uint[] textures);

        #endregion

        #region Wrapped Methods

        /// <summary>
        /// Binds the selected buffer.
        /// </summary>
        /// <param name="target">The Target.</param>
        /// <param name="buffer">The Buffer.</param>
        public static void BindBuffer(BufferTarget target, uint buffer)
        {
            InvokeExtensionMethod<glBindBuffer>((uint) target, buffer);
        }

        /// <summary>
        /// Deletes a texture.
        /// </summary>
        /// <param name="textureId">The TextureId.</param>
        public static void DeleteTexture(uint textureId)
        {
            DeleteTextures(new[] {textureId});
        }

        /// <summary>
        /// Deletes a set of textures.
        /// </summary>
        /// <param name="textureIds">The TextureIds.</param>
        public static void DeleteTextures(uint[] textureIds)
        {
            glDeleteTextures(textureIds.Length, textureIds);
        }

        /// <summary>
        /// Disables the specified option.
        /// </summary>
        /// <param name="cap">The Option.</param>
        public static void Disable(uint cap)
        {
            glDisable(cap);
        }

        /// <summary>
        /// Sets the texture parameter.
        /// </summary>
        /// <param name="target">The Target.</param>
        /// <param name="pname">The ParameterName.</param>
        /// <param name="param">The Parameter.</param>
        public static void TexParameterI(TextureParam target, TextureParam pname, int param)
        {
            glTexParameteri((uint) target, (uint) pname, param);
        }

        /// <summary>
        /// Sets the texture parameter.
        /// </summary>
        /// <param name="target">The Target.</param>
        /// <param name="pname">The ParameterName.</param>
        /// <param name="param">The Parameter.</param>
        public static void TexParameterF(TextureParam target, TextureParam pname, TextureParam param)
        {
            glTexParameterf((uint) target, (uint) pname, (float) param);
        }

        /// <summary>
        /// Enables the specified option.
        /// </summary>
        /// <param name="cap">The Option.</param>
        public static void Enable(uint cap)
        {
            glEnable(cap);
        }

        /// <summary>
        /// Enables alpha blending.
        /// </summary>
        public static void EnableBlend()
        {
            glEnable(0x0BE2);
        }

        /// <summary>
        /// Generates buffer for a texture.
        /// </summary>
        /// <returns>The Id.</returns>
        public static uint GenTexture()
        {
            var textures = new uint[1];
            glGenTextures(1, textures);
            return textures[0];
        }

        /// <summary>
        /// Generates buffer for n texture.
        /// </summary>
        /// <returns>The Ids.</returns>
        public static uint[] GenTextures(int n)
        {
            var textures = new uint[n];
            glGenTextures(n, textures);
            return textures;
        }

        /// <summary>
        /// Draws specified elements.
        /// </summary>
        /// <param name="mode">The Mode.</param>
        /// <param name="count">The Count.</param>
        /// <param name="type">The Type.</param>
        /// <param name="indices">The Indices.</param>
        public static void DrawElements(DrawMode mode, int count, DataTypes type, IntPtr indices)
        {
            glDrawElements((uint) mode, count, (uint) type, indices);
        }

        /// <summary>
        /// Sets the clear color.
        /// </summary>
        /// <param name="color">The Color.</param>
        public static void ClearColor(GLColor color)
        {
            glClearColor(color.R, color.G, color.B, color.A);
        }

        /// <summary>
        /// Sets the blend function.
        /// </summary>
        public static void AlphaBlend()
        {
            glBlendFunc(0x0302, 0x0303);
        }

        /// <summary>
        /// Clears the buffer.
        /// </summary>
        public static void Clear()
        {
            glClear(0x00004000);
        }

        /// <summary>
        /// Gets the error.
        /// </summary>
        /// <returns>OpenGLError.</returns>
        public static GLError GetError()
        {
            return (GLError) glGetError();
        }

        /// <summary>
        /// Binds a texture.
        /// </summary>
        /// <param name="target">The Target.</param>
        /// <param name="texture">The Texture.</param>
        public static void BindTexture(TextureParam target, uint texture)
        {
            glBindTexture((uint) target, texture);
        }

        /// <summary>
        /// Textures an image.
        /// </summary>
        /// <param name="target">The Target.</param>
        /// <param name="internalformat">The InternalFormat.</param>
        /// <param name="width">The Width.</param>
        /// <param name="height">The Height.</param>
        /// <param name="format">The Format.</param>
        /// <param name="type">The Type.</param>
        /// <param name="pixels">The Pixels.</param>
        public static void TexImage2D(TextureParam target, ColorFormat internalformat, int width, int height,
            ColorFormat format,
            DataTypes type, IntPtr pixels)
        {
            glTexImage2D((uint) target, 0, (uint) internalformat, width, height, 0, (uint) format, (uint) type, pixels);
        }

        /// <summary>
        /// Sets the Viewport.
        /// </summary>
        /// <param name="x">The X.</param>
        /// <param name="y">The Y.</param>
        /// <param name="width">The Width.</param>
        /// <param name="height">The Height.</param>
        public static void Viewport(int x, int y, int width, int height)
        {
            glViewport(x, y, width, height);
        }

        /// <summary>
        /// Gets the textured image.
        /// </summary>
        /// <param name="target">The Target.</param>
        /// <param name="format">The Format.</param>
        /// <param name="type">The Type.</param>
        /// <param name="pixels">The Pixels.</param>
        public static void GetTexImage(TextureParam target, ColorFormat format, DataTypes type, object pixels)
        {
            GCHandle pData = GCHandle.Alloc(pixels, GCHandleType.Pinned);
            try
            {
                glGetTexImage((uint) target, 0, (uint) format, (uint) type, pData.AddrOfPinnedObject());
            }
            finally
            {
                pData.Free();
            }
        }

        /// <summary>
        /// Textures a sub image.
        /// </summary>
        /// <param name="target">The Target.</param>
        /// <param name="level">The Level.</param>
        /// <param name="xoffset">The XOffset.</param>
        /// <param name="yoffset">The YOffset.</param>
        /// <param name="width">The Width.</param>
        /// <param name="height">The Height.</param>
        /// <param name="format">The Format.</param>
        /// <param name="type">The Type.</param>
        /// <param name="pixels">The Pixels.</param>
        public static void TexSubImage2D(TextureParam target, int level, int xoffset, int yoffset, int width, int height,
            ColorFormat format, DataTypes type, object pixels)
        {
            GCHandle pData = GCHandle.Alloc(pixels, GCHandleType.Pinned);
            try
            {
                glTexSubImage2D((uint) target, level, xoffset, yoffset, width, height, (uint) format, (uint) type,
                    pData.AddrOfPinnedObject());
            }
            finally
            {
                pData.Free();
            }
        }

        /// <summary>
        /// Deletes the buffer.
        /// </summary>
        /// <param name="n">The Amount.</param>
        /// <param name="buffers">The Buffers.</param>
        public static void DeleteBuffers(int n, uint[] buffers)
        {
            InvokeExtensionMethod<glDeleteBuffers>(n, buffers);
        }

        /// <summary>
        /// Generates n buffers.
        /// </summary>
        /// <param name="n">The Amount.</param>
        /// <param name="buffers">The Buffers.</param>
        public static void GenBuffers(int n, uint[] buffers)
        {
            InvokeExtensionMethod<glGenBuffers>(n, buffers);
        }

        /// <summary>
        /// Sends the data to the GPU.
        /// </summary>
        /// <param name="target">The Target.</param>
        /// <param name="data">The Data.</param>
        /// <param name="usage">The Usage.</param>
        public static void BufferData(BufferTarget target, float[] data, DrawMode usage)
        {
            IntPtr p = Marshal.AllocHGlobal(data.Length*sizeof (float));
            Marshal.Copy(data, 0, p, data.Length);
            InvokeExtensionMethod<glBufferData>((uint) target, data.Length*sizeof (float), p, (uint) usage);
            Marshal.FreeHGlobal(p);
        }

        /// <summary>
        /// Sends the data to the GPU.
        /// </summary>
        /// <param name="target">The Target.</param>
        /// <param name="data">The Data.</param>
        /// <param name="usage">The Usage.</param>
        public static void BufferData(BufferTarget target, ushort[] data, DrawMode usage)
        {
            int dataSize = data.Length*sizeof (ushort);
            IntPtr p = Marshal.AllocHGlobal(dataSize);
            var shortData = new short[data.Length];
            Buffer.BlockCopy(data, 0, shortData, 0, dataSize);
            Marshal.Copy(shortData, 0, p, data.Length);
            InvokeExtensionMethod<glBufferData>((uint) target, dataSize, p, (uint) usage);
            Marshal.FreeHGlobal(p);
        }

        /// <summary>
        /// Binds the fragment shader data location.
        /// </summary>
        /// <param name="program">The Program.</param>
        /// <param name="color">The Color.</param>
        /// <param name="name">The Title.</param>
        public static void BindFragDataLocation(uint program, uint color, string name)
        {
            InvokeExtensionMethod<glBindFragDataLocation>(program, color, name);
        }

        /// <summary>
        /// Binds the texture to a sampler.
        /// </summary>
        /// <param name="texture">The SamplerId.</param>
        public static void ActiveTexture(TextureParam texture)
        {
            InvokeExtensionMethod<glActiveTexture>((uint) texture);
        }

        /// <summary>
        /// Attaches a shader to the specified program.
        /// </summary>
        /// <param name="program">The Program.</param>
        /// <param name="shader">The Shader.</param>
        public static void AttachShader(uint program, uint shader)
        {
            InvokeExtensionMethod<glAttachShader>(program, shader);
        }

        /// <summary>
        /// Compiles a shader.
        /// </summary>
        /// <param name="shader">The Shader.</param>
        public static void CompileShader(uint shader)
        {
            InvokeExtensionMethod<glCompileShader>(shader);
        }

        /// <summary>
        /// Creates a new shader program.
        /// </summary>
        /// <returns>The Id.</returns>
        public static uint CreateProgram()
        {
            return (uint) InvokeExtensionMethod<glCreateProgram>();
        }

        /// <summary>
        /// Create a shader.
        /// </summary>
        /// <param name="type">The type specifing whether the shader is a fragment - or vertexshader.</param>
        /// <returns>The Id.</returns>
        public static uint CreateShader(uint type)
        {
            return (uint) InvokeExtensionMethod<glCreateShader>(type);
        }

        /// <summary>
        /// Deletes a program.
        /// </summary>
        /// <param name="program">The Program.</param>
        public static void DeleteProgram(uint program)
        {
            InvokeExtensionMethod<glDeleteProgram>(program);
        }

        /// <summary>
        /// Deletes a shader.
        /// </summary>
        /// <param name="shader">The Shader.</param>
        public static void DeleteShader(uint shader)
        {
            InvokeExtensionMethod<glDeleteShader>(shader);
        }

        /// <summary>
        /// Enables a vertex attribute array.
        /// </summary>
        /// <param name="index">The Index.</param>
        public static void EnableVertexAttribArray(uint index)
        {
            InvokeExtensionMethod<glEnableVertexAttribArray>(index);
        }

        /// <summary>
        /// Gets the attribute location.
        /// </summary>
        /// <param name="program">The Program.</param>
        /// <param name="name">The Title.</param>
        /// <returns>The Id pointing to the attribute.</returns>
        public static int GetAttribLocation(uint program, string name)
        {
            return (int) InvokeExtensionMethod<glGetAttribLocation>(program, name);
        }

        /// <summary>
        /// Gets the location of an uniform.
        /// </summary>
        /// <param name="program">The Program.</param>
        /// <param name="name">The Title.</param>
        /// <returns>The Id pointing to the uniform.</returns>
        public static int GetUniformLocation(uint program, string name)
        {
            return (int) InvokeExtensionMethod<glGetUniformLocation>(program, name);
        }

        /// <summary>
        /// Links the programm.
        /// </summary>
        /// <param name="program">The Program.</param>
        public static void LinkProgram(uint program)
        {
            InvokeExtensionMethod<glLinkProgram>(program);
        }

        /// <summary>
        /// Replace the source code in a shader.
        /// </summary>
        /// <param name="shader">The Shader.</param>
        /// <param name="source">The Source.</param>
        public static void ShaderSource(uint shader, string source)
        {
            InvokeExtensionMethod<glShaderSource>(shader, 1, new[] {source}, new[] {source.Length});
        }

        /// <summary>
        /// Uses the program for drawing.
        /// </summary>
        /// <param name="program">The Program.</param>
        public static void UseProgram(uint program)
        {
            InvokeExtensionMethod<glUseProgram>(program);
        }

        /// <summary>
        /// Sets the uniform.
        /// </summary>
        /// <param name="location">The Location.</param>
        /// <param name="v0">The first Parameter.</param>
        public static void FloatUniform1(int location, float v0)
        {
            InvokeExtensionMethod<glUniform1f>(location, v0);
        }

        /// <summary>
        /// Sets the uniform.
        /// </summary>
        /// <param name="location">The Location.</param>
        /// <param name="v0">The first Parameter.</param>
        /// <param name="v1">The second Parameter.</param>
        public static void FloatUniform2(int location, float v0, float v1)
        {
            InvokeExtensionMethod<glUniform2f>(location, v0, v1);
        }

        /// <summary>
        /// Sets the uniform.
        /// </summary>
        /// <param name="location">The Location.</param>
        /// <param name="v0">The first Parameter.</param>
        /// <param name="v1">The second Parameter.</param>
        /// <param name="v2">The third Parameter.</param>
        public static void FloatUniform3(int location, float v0, float v1, float v2)
        {
            InvokeExtensionMethod<glUniform3f>(location, v0, v1, v2);
        }

        /// <summary>
        /// Sets the uniform.
        /// </summary>
        /// <param name="location">The Location.</param>
        /// <param name="v0">The first Parameter.</param>
        /// <param name="v1">The second Parameter.</param>
        /// <param name="v2">The third Parameter.</param>
        /// <param name="v3">The fourth Parameter.</param>
        public static void FloatUniform4(int location, float v0, float v1, float v2, float v3)
        {
            InvokeExtensionMethod<glUniform4f>(location, v0, v1, v2, v3);
        }

        /// <summary>
        /// Sets the uniform.
        /// </summary>
        /// <param name="location">The Location.</param>
        /// <param name="v0">The first Parameter.</param>
        public static void IntUniform1(int location, int v0)
        {
            InvokeExtensionMethod<glUniform1i>(location, v0);
        }

        /// <summary>
        /// Sets the uniform.
        /// </summary>
        /// <param name="location">The Location.</param>
        /// <param name="v0">The first Parameter.</param>
        /// <param name="v1">The second Parameter.</param>
        public static void IntUniform2(int location, int v0, int v1)
        {
            InvokeExtensionMethod<glUniform2i>(location, v0, v1);
        }

        /// <summary>
        /// Sets the uniform.
        /// </summary>
        /// <param name="location">The Location.</param>
        /// <param name="v0">The first Parameter.</param>
        /// <param name="v1">The second Parameter.</param>
        /// <param name="v2">The third Parameter.</param>
        public static void IntUniform3(int location, int v0, int v1, int v2)
        {
            InvokeExtensionMethod<glUniform3i>(location, v0, v1, v2);
        }

        /// <summary>
        /// Sets the uniform.
        /// </summary>
        /// <param name="location">The Location.</param>
        /// <param name="v0">The first Parameter.</param>
        /// <param name="v1">The second Parameter.</param>
        /// <param name="v2">The third Parameter.</param>
        /// <param name="v3">The fourth Parameter.</param>
        public static void IntUniform4(int location, int v0, int v1, int v2, int v3)
        {
            InvokeExtensionMethod<glUniform4i>(location, v0, v1, v2, v3);
        }

        /// <summary>
        /// Sets the uniform.
        /// </summary>
        /// <param name="location">The Location.</param>
        /// <param name="count">The Count.</param>
        /// <param name="transpose">The State.</param>
        /// <param name="value">The Values.</param>
        public static void UniformMatrix4(int location, int count, bool transpose, float[] value)
        {
            InvokeExtensionMethod<glUniformMatrix4fv>(location, count, transpose, value);
        }

        /// <summary>
        /// Sets a vertex attribute pointer.
        /// </summary>
        /// <param name="index">The Index.</param>
        /// <param name="size">The Size.</param>
        /// <param name="type">The Type.</param>
        /// <param name="normalized">The State.</param>
        /// <param name="stride">The Stride.</param>
        /// <param name="pointer">The Pointer.</param>
        public static void VertexAttribPointer(uint index, int size, DataTypes type, bool normalized, int stride,
            IntPtr pointer)
        {
            InvokeExtensionMethod<glVertexAttribPointer>(index, size, (uint) type, normalized, stride, pointer);
        }

        /// <summary>
        /// Binds a vertex array.
        /// </summary>
        /// <param name="array">The Array.</param>
        public static void BindVertexArray(uint array)
        {
            InvokeExtensionMethod<glBindVertexArray>(array);
        }

        /// <summary>
        /// Deletes n vertex arrays.
        /// </summary>
        /// <param name="n">The Amount.</param>
        /// <param name="arrays">The Arrays.</param>
        public static void DeleteVertexArrays(int n, uint[] arrays)
        {
            InvokeExtensionMethod<glDeleteVertexArrays>(n, arrays);
        }

        /// <summary>
        /// Generates n vertex arrays.
        /// </summary>
        /// <param name="n">The Amount.</param>
        /// <param name="arrays">The Arrays.</param>
        public static void GenVertexArrays(int n, uint[] arrays)
        {
            InvokeExtensionMethod<glGenVertexArrays>(n, arrays);
        }

        #endregion
    }
}
