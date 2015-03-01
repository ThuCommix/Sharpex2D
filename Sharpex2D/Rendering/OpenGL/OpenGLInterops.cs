// Copyright (c) 2012-2014 Sharpex2D - Kevin Scholz (ThuCommix)
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
using System.Runtime.InteropServices;
using System.Security;

namespace Sharpex2D.Rendering.OpenGL
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    internal static class OpenGLInterops
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
        /// <param name="name">The Name.</param>
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
        [DllImport("gdi32.dll", SetLastError = true), SuppressUnmanagedCodeSecurity]
        public static extern int ChoosePixelFormat(IntPtr deviceContext, ref PixelFormatDescriptor pixelFormatDescriptor);

        /// <summary>
        /// Sets the PixelFormat.
        /// </summary>
        /// <param name="deviceContext">The DeviceContext.</param>
        /// <param name="pixelFormat">The PixelFormat.</param>
        /// <param name="pixelFormatDescriptor">The PixelFormatDescriptor.</param>
        /// <returns>True on success.</returns>
        [DllImport("gdi32.dll", EntryPoint = "SetPixelFormat", SetLastError = true), SuppressUnmanagedCodeSecurity]
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

        private delegate IntPtr wglCreateContextAttribsARB(IntPtr hdc, IntPtr hShareContext, int[] attribList);

        private delegate void glBindFragDataLocation(uint program, uint color, string name);

        private delegate void glBindBuffer(uint target, uint buffer);

        private delegate void glDeleteBuffers(int n, uint[] buffers);

        private delegate void glGenBuffers(int n, uint[] buffers);


        private delegate void glBufferData(uint target, int size, IntPtr data, uint usage);

        private delegate void glAttachShader(uint program, uint shader);

        private delegate void glCompileShader(uint shader);

        private delegate uint glCreateProgram();

        private delegate uint glCreateShader(uint type);

        private delegate void glDeleteProgram(uint program);

        private delegate void glDeleteShader(uint shader);

        private delegate void glEnableVertexAttribArray(uint index);

        private delegate int glGetAttribLocation(uint program, string name);

        private delegate int glGetUniformLocation(uint program, string name);

        private delegate void glLinkProgram(uint program);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, ThrowOnUnmappableChar = true)]
        private delegate void glShaderSource(uint shader, int count, string[] source, int[] length);

        private delegate void glUseProgram(uint program);

        private delegate void glUniform1f(int location, float v0);

        private delegate void glUniform2f(int location, float v0, float v1);

        private delegate void glUniform3f(int location, float v0, float v1, float v2);

        private delegate void glUniform4f(int location, float v0, float v1, float v2, float v3);

        private delegate void glUniformMatrix4fv(int location, int count, bool transpose, float[] value);

        private delegate void glVertexAttribPointer(
            uint index, int size, uint type, bool normalized, int stride, IntPtr pointer);

        private delegate void glBindVertexArray(uint array);

        private delegate void glDeleteVertexArrays(int n, uint[] arrays);

        private delegate void glGenVertexArrays(int n, uint[] arrays);

        private delegate void glActiveTexture(uint texture);

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

            var delegateType = typeof (T);
            if (!_extensions.ContainsKey(delegateType.Name))
            {
                var hfunc = wglGetProcAddress(delegateType.Name);
                if (hfunc == IntPtr.Zero)
                {
                    throw new MissingMethodException(delegateType.Name);
                }

                var del = Marshal.GetDelegateForFunctionPointer(hfunc, delegateType);
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
        public static extern void glBindTexture(uint target, uint texture);

        [DllImport("opengl32.dll", SetLastError = true)]
        public static extern void glBlendFunc(uint sfactor, uint dfactor);

        [DllImport("opengl32.dll", SetLastError = true)]
        public static extern void glClear(uint mask);

        [DllImport("opengl32.dll", SetLastError = true)]
        public static extern void glClearColor(float red, float green, float blue, float alpha);

        [DllImport("opengl32.dll", SetLastError = true)]
        public static extern void glDrawElements(uint mode, int count, uint type, IntPtr indices);

        [DllImport("opengl32.dll", SetLastError = true)]
        public static extern void glEnable(uint cap);

        [DllImport("opengl32.dll", SetLastError = true)]
        public static extern void glGenTextures(int n, uint[] textures);

        [DllImport("opengl32.dll", SetLastError = true)]
        public static extern uint glGetError();

        [DllImport("opengl32.dll", SetLastError = true)]
        public static extern void glTexImage2D(uint target, int level, uint internalformat, int width, int height,
            int border, uint format, uint type, IntPtr pixels);

        [DllImport("opengl32.dll", SetLastError = true)]
        public static extern void glTexParameterf(uint target, uint pname, float param);

        [DllImport("opengl32.dll", SetLastError = true)]
        public static extern void glTexParameteri(uint target, uint pname, int param);

        [DllImport("opengl32.dll", SetLastError = true)]
        public static extern void glViewport(int x, int y, int width, int height);

        #endregion

        #region Wrapped Methods

        /// <summary>
        /// Binds the selected buffer.
        /// </summary>
        /// <param name="target">The Target.</param>
        /// <param name="buffer">The Buffer.</param>
        public static void BindBuffer(uint target, uint buffer)
        {
            InvokeExtensionMethod<glBindBuffer>(target, buffer);
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
        public static void BufferData(uint target, float[] data, uint usage)
        {
            IntPtr p = Marshal.AllocHGlobal(data.Length*sizeof (float));
            Marshal.Copy(data, 0, p, data.Length);
            InvokeExtensionMethod<glBufferData>(target, data.Length*sizeof (float), p, usage);
            Marshal.FreeHGlobal(p);
        }

        /// <summary>
        /// Sends the data to the GPU.
        /// </summary>
        /// <param name="target">The Target.</param>
        /// <param name="data">The Data.</param>
        /// <param name="usage">The Usage.</param>
        public static void BufferData(uint target, ushort[] data, uint usage)
        {
            var dataSize = data.Length*sizeof (ushort);
            IntPtr p = Marshal.AllocHGlobal(dataSize);
            var shortData = new short[data.Length];
            Buffer.BlockCopy(data, 0, shortData, 0, dataSize);
            Marshal.Copy(shortData, 0, p, data.Length);
            InvokeExtensionMethod<glBufferData>(target, dataSize, p, usage);
            Marshal.FreeHGlobal(p);
        }

        /// <summary>
        /// Binds the fragment shader data location.
        /// </summary>
        /// <param name="program">The Program.</param>
        /// <param name="color">The Color.</param>
        /// <param name="name">The Name.</param>
        public static void BindFragDataLocation(uint program, uint color, string name)
        {
            InvokeExtensionMethod<glBindFragDataLocation>(program, color, name);
        }

        /// <summary>
        /// Binds the texture to a sampler.
        /// </summary>
        /// <param name="texture">The SamplerId.</param>
        public static void ActiveTexture(uint texture)
        {
            InvokeExtensionMethod<glActiveTexture>(texture);
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
        /// <param name="name">The Name.</param>
        /// <returns>The Id pointing to the attribute.</returns>
        public static int GetAttribLocation(uint program, string name)
        {
            return (int) InvokeExtensionMethod<glGetAttribLocation>(program, name);
        }

        /// <summary>
        /// Gets the location of an uniform.
        /// </summary>
        /// <param name="program">The Program.</param>
        /// <param name="name">The Name.</param>
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
        public static void Uniform1(int location, float v0)
        {
            InvokeExtensionMethod<glUniform1f>(location, v0);
        }

        /// <summary>
        /// Sets the uniform.
        /// </summary>
        /// <param name="location">The Location.</param>
        /// <param name="v0">The first Parameter.</param>
        /// <param name="v1">The second Parameter.</param>
        public static void Uniform2(int location, float v0, float v1)
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
        public static void Uniform3(int location, float v0, float v1, float v2)
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
        public static void Uniform4(int location, float v0, float v1, float v2, float v3)
        {
            InvokeExtensionMethod<glUniform4f>(location, v0, v1, v2, v3);
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
        public static void VertexAttribPointer(uint index, int size, uint type, bool normalized, int stride,
            IntPtr pointer)
        {
            InvokeExtensionMethod<glVertexAttribPointer>(index, size, type, normalized, stride, pointer);
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

        public const uint GL_COLOR_BUFFER_BIT = 0x00004000;
        public const uint GL_TEXTURE0 = 0x84C0;
        public const uint GL_REPEAT = 0x2901;
        public const uint GL_TEXTURE_WRAP_S = 0x2802;
        public const uint GL_TEXTURE_WRAP_T = 0x2803;
        public const uint GL_NEAREST = 9728u;
        public const uint GL_LINEAR = 9729u;
        public const uint GL_TEXTURE_MAG_FILTER = 10240u;
        public const uint GL_TEXTURE_MIN_FILTER = 10241u;
        public const uint GL_TEXTURE_2D = 3553u;
        public const uint GL_SRC_ALPHA = 0x0302;
        public const uint GL_ONE_MINUS_SRC_ALPHA = 0x0303;
        public const uint GL_BLEND = 0x0BE2;
        public const uint GL_TRIANGLES = 0x0004;
        public const uint GL_TRUE = 1;
        public const uint GL_FALSE = 0;
        public const uint GL_UNSIGNED_BYTE = 0x1401;
        public const uint GL_UNSIGNED_SHORT = 0x1403;
        public const uint GL_UNSIGNED_INT = 0x1405;
        public const uint GL_FLOAT = 0x1406;
        public const uint GL_MAJOR_VERSION = 0x821B;
        public const uint GL_MINOR_VERSION = 0x821C;

        public const uint GL_RGBA = 0x1908;
        public const uint GL_RGBA8 = 32856u;
        public const uint GL_BGRA = 32993u;
        public const uint GL_RGBA32UI = 0x8D70;
        public const uint GL_RGB32UI = 0x8D71;
        public const uint GL_RGBA16UI = 0x8D76;
        public const uint GL_RGB16UI = 0x8D77;
        public const uint GL_RGBA8UI = 0x8D7C;
        public const uint GL_RGB8UI = 0x8D7D;
        public const uint GL_RGBA32I = 0x8D82;
        public const uint GL_RGB32I = 0x8D83;
        public const uint GL_RGBA16I = 0x8D88;
        public const uint GL_RGB16I = 0x8D89;
        public const uint GL_RGBA8I = 0x8D8E;
        public const uint GL_RGB8I = 0x8D8F;


        public const uint GL_ARRAY_BUFFER = 0x8892;
        public const uint GL_ELEMENT_ARRAY_BUFFER = 0x8893;
        public const uint GL_ARRAY_BUFFER_BINDING = 0x8894;
        public const uint GL_STATIC_DRAW = 0x88E4;
        public const uint GL_FRAGMENT_SHADER = 0x8B30;
        public const uint GL_VERTEX_SHADER = 0x8B31;


        #endregion
    }
}
