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
using Sharpex2D.Framework.Debug.Logging;

namespace Sharpex2D.Framework.Rendering.OpenGL.Shaders
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    internal class ShaderProgram
    {
        /// <summary>
        /// Initializes a new ShaderProgram class.
        /// </summary>
        public ShaderProgram()
        {
            Id = OpenGLInterops.CreateProgram();
        }

        /// <summary>
        /// Gets the shader program Id.
        /// </summary>
        public uint Id { private set; get; }

        /// <summary>
        /// Links a vertex shader and a fragment shader to this program.
        /// </summary>
        /// <param name="vShader">The VertexShader.</param>
        /// <param name="fShader">The FragmentShader.</param>
        public void Link(VertexShader vShader, FragmentShader fShader)
        {
            OpenGLInterops.AttachShader(Id, vShader.Id);
            OpenGLInterops.AttachShader(Id, fShader.Id);
            OpenGLInterops.BindFragDataLocation(Id, 0, "outColor");
            OpenGLInterops.LinkProgram(Id);
        }

        /// <summary>
        /// Sets an uniform.
        /// </summary>
        /// <param name="name">The Title.</param>
        /// <param name="data">The Data.</param>
        public void SetUniform(string name, params float[] data)
        {
            int propertyLoc = OpenGLInterops.GetUniformLocation(Id, name);

            switch (data.Length)
            {
                case 1:
                    OpenGLInterops.Uniform1(propertyLoc, data[0]);
                    break;
                case 2:
                    OpenGLInterops.Uniform2(propertyLoc, data[0], data[1]);
                    break;
                case 3:
                    OpenGLInterops.Uniform3(propertyLoc, data[0], data[1], data[2]);
                    break;
                case 4:
                    OpenGLInterops.Uniform4(propertyLoc, data[0], data[1], data[2], data[3]);
                    break;
                default:
                    throw new InvalidOperationException("Invalid argument length.");
            }
        }

        /// <summary>
        /// Sets an Uniform matrix.
        /// </summary>
        /// <param name="name">The Title.</param>
        /// <param name="data">The Data.</param>
        public void SetUniformMatrix(string name, float[] data)
        {
            int propertyLoc = OpenGLInterops.GetUniformLocation(Id, name);
            OpenGLInterops.UniformMatrix4(propertyLoc, 1, false, data);
        }

        /// <summary>
        /// Gets the attribute location.
        /// </summary>
        /// <param name="name">The Title.</param>
        /// <returns>UInt.</returns>
        public uint GetAttribLocation(string name)
        {
            return (uint) OpenGLInterops.GetAttribLocation(Id, name);
        }

        /// <summary>
        /// Binds the shader program.
        /// </summary>
        public void Bind()
        {
            OpenGLInterops.UseProgram(Id);
        }

        /// <summary>
        /// Unbinds the shader program.
        /// </summary>
        public void Unbind()
        {
            OpenGLInterops.UseProgram(0);
        }

        /// <summary>
        /// Deletes the shader program.
        /// </summary>
        public void Delete()
        {
            try
            {
                OpenGLInterops.DeleteProgram(Id);
            }
            catch
            {
                LogManager.GetClassLogger().Warn("Unable to dispose.");
            }
        }
    }
}