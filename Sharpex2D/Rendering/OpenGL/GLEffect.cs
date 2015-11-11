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

namespace Sharpex2D.Framework.Rendering.OpenGL
{
    internal class GLEffect
    {
        /// <summary>
        /// Gets the id
        /// </summary>
        public uint Id { private set; get; }

        /// <summary>
        /// Gets the name
        /// </summary>
        public string Name { get; }

        private readonly Dictionary<string, int> _propertyLocations;
        private readonly string _fragmentCode;
        private readonly string _vertexCode;
        private readonly float[] _matrixf;

        /// <summary>
        /// Initializes a new GLEffect class
        /// </summary>
        /// <param name="fragment">The fragment shader source code</param>
        /// <param name="vertex">The vertex shader source code</param>
        /// <param name="effectName">The effect name</param>
        public GLEffect(string fragment, string vertex, string effectName)
        {
            _propertyLocations = new Dictionary<string, int>();
            Name = effectName;
            _fragmentCode = fragment;
            _vertexCode = vertex;
            _matrixf = new float[16];
        }

        /// <summary>
        /// Deconstructs the GLEffect class
        /// </summary>
        ~GLEffect()
        {
            Dispose(false);
        }

        /// <summary>
        /// Gets the attribute location
        /// </summary>
        /// <param name="identifier">The identifier</param>
        /// <returns>UInt.</returns>
        public uint GetAttribLocation(string identifier)
        {
            return (uint)GLInterops.GetAttribLocation(Id, identifier);
        }

        /// <summary>
        /// Binds the effect
        /// </summary>
        public void Bind()
        {
            GLInterops.UseProgram(Id);
        }

        /// <summary>
        /// Unbinds the effect
        /// </summary>
        public void Unbind()
        {
            GLInterops.UseProgram(0);
        }

        /// <summary>
        /// Disposes the effect
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes the effect
        /// </summary>
        /// <param name="disposing">The disposing state</param>
        protected void Dispose(bool disposing)
        {
            try
            {
                GLInterops.DeleteProgram(Id);
            }
            catch
            {
                Logger.Instance.Warn("Unable to dispose effect.");
            }
        }

        /// <summary>
        /// Compiles the effect.
        /// </summary>
        public void Compile()
        {
            try
            {
                //create vertex shader
                var vertexShader = GLInterops.CreateShader(0x8B31);

                //create fragment shader
                var fragmentShader = GLInterops.CreateShader(0x8B30);

                //compile
                GLInterops.ShaderSource(vertexShader, _vertexCode);
                GLInterops.CompileShader(vertexShader);
                GLInterops.ShaderSource(fragmentShader, _fragmentCode);
                GLInterops.CompileShader(fragmentShader);

                //link
                Id = GLInterops.CreateProgram();
                GLInterops.AttachShader(Id, vertexShader);
                GLInterops.AttachShader(Id, fragmentShader);
                GLInterops.BindFragDataLocation(Id, 0, "outColor");
                GLInterops.LinkProgram(Id);

            }
            catch
            {
                throw new InvalidOperationException($"Unable to compile effect {Name}.");
            }
        }


        /// <summary>
        /// Sets the data
        /// </summary>
        /// <param name="identifier">The identifier</param>
        /// <param name="v0">The first value</param>
        public void SetData(string identifier, float v0)
        {
            GLInterops.FloatUniform1(GetPropertyLocation(identifier), v0);
        }

        /// <summary>
        /// Sets the data
        /// </summary>
        /// <param name="identifier">The identifier</param>
        /// <param name="v0">The first value</param>
        /// <param name="v1">The second value</param>
        public void SetData(string identifier, float v0, float v1)
        {
            GLInterops.FloatUniform2(GetPropertyLocation(identifier), v0, v1);
        }

        /// <summary>
        /// Sets the data
        /// </summary>
        /// <param name="identifier">The identifier</param>
        /// <param name="v0">The first value</param>
        /// <param name="v1">The second value</param>
        /// <param name="v2">The third value</param>
        public void SetData(string identifier, float v0, float v1, float v2)
        {
            GLInterops.FloatUniform3(GetPropertyLocation(identifier), v0, v1, v2);
        }

        /// <summary>
        /// Sets the data
        /// </summary>
        /// <param name="identifier">The identifier</param>
        /// <param name="v0">The first value</param>
        /// <param name="v1">The second value</param>
        /// <param name="v2">The third value</param>
        /// <param name="v3">The 4th value</param>
        public void SetData(string identifier, float v0, float v1, float v2, float v3)
        {
            GLInterops.FloatUniform4(GetPropertyLocation(identifier), v0, v1, v2, v3);
        }

        /// <summary>
        /// Sets the data
        /// </summary>
        /// <param name="identifier">The identifier</param>
        /// <param name="value">The value</param>
        public void SetData(string identifier, Matrix2x3 value)
        {
            _matrixf[0] = value[0, 0];
            _matrixf[1] = value[1, 0];
            _matrixf[2] = 0;
            _matrixf[3] = 0;

            _matrixf[4] = value[0, 1];
            _matrixf[5] = value[1, 1];
            _matrixf[6] = 0;
            _matrixf[7] = 0;

            _matrixf[8] = 0;
            _matrixf[9] = 0;
            _matrixf[10] = 1;
            _matrixf[11] = 0;

            _matrixf[12] = value.OffsetX;
            _matrixf[13] = value.OffsetY;
            _matrixf[14] = 0;
            _matrixf[15] = 1;

            GLInterops.UniformMatrix4(GetPropertyLocation(identifier), 1, false, _matrixf);
        }

        /// <summary>
        /// Sets the data
        /// </summary>
        /// <param name="identifier">The identifier</param>
        /// <param name="v0">The first value</param>
        public void SetData(string identifier, int v0)
        {
            GLInterops.IntUniform1(GetPropertyLocation(identifier), v0);
        }

        /// <summary>
        /// Sets the data
        /// </summary>
        /// <param name="identifier">The identifier</param>
        /// <param name="v0">The first value</param>
        /// <param name="v1">The second value</param>
        public void SetData(string identifier, int v0, int v1)
        {
            GLInterops.IntUniform2(GetPropertyLocation(identifier), v0, v1);
        }

        /// <summary>
        /// Sets the data
        /// </summary>
        /// <param name="identifier">The identifier</param>
        /// <param name="v0">The first value</param>
        /// <param name="v1">The second value</param>
        /// <param name="v2">The third value</param>
        public void SetData(string identifier, int v0, int v1, int v2)
        {
            GLInterops.IntUniform3(GetPropertyLocation(identifier), v0, v1, v2);
        }

        /// <summary>
        /// Sets the data
        /// </summary>
        /// <param name="identifier">The identifier</param>
        /// <param name="v0">The first value</param>
        /// <param name="v1">The second value</param>
        /// <param name="v2">The third value</param>
        /// <param name="v3">The 4th value</param>
        public void SetData(string identifier, int v0, int v1, int v2, int v3)
        {
            GLInterops.IntUniform4(GetPropertyLocation(identifier), v0, v1, v2, v3);
        }

        /// <summary>
        /// Gets the property location
        /// </summary>
        /// <param name="identifier">The identifier</param>
        /// <returns>Returns the int as property location</returns>
        private int GetPropertyLocation(string identifier)
        {
            if (!_propertyLocations.ContainsKey(identifier))
            {
                _propertyLocations.Add(identifier, GLInterops.GetUniformLocation(Id, identifier));
            }

            return _propertyLocations[identifier];
        }
    }
}
