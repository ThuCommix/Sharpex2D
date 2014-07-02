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
using Sharpex2D.Framework.Debug.Logging;

namespace Sharpex2D.Framework.Game.Services
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public class LaunchParameters : IGameService
    {
        private readonly Dictionary<string, string> _parameters;

        /// <summary>
        ///     Initializes a new LaunchParameters class.
        /// </summary>
        internal LaunchParameters()
        {
            _parameters = new Dictionary<string, string>();
            ParseCommandLineParameters(Environment.GetCommandLineArgs());
        }

        /// <summary>
        ///     Initializes a new LaunchParameters class.
        /// </summary>
        /// <param name="parameters">The Parameters.</param>
        internal LaunchParameters(IEnumerable<LaunchParameter> parameters)
        {
            _parameters = new Dictionary<string, string>();
            foreach (LaunchParameter parameter in parameters)
            {
                _parameters.Add(parameter.Key, parameter.Value);
            }
        }

        /// <summary>
        ///     Returns a launch parameter specified by the key.
        /// </summary>
        /// <param name="key">The Key.</param>
        /// <returns>String.</returns>
        public string this[string key]
        {
            get { return _parameters[key]; }
        }

        /// <summary>
        ///     Parses the command line parameters.
        /// </summary>
        /// <param name="value">The Parameters.</param>
        private void ParseCommandLineParameters(IList<string> value)
        {
            _parameters.Add("ExecuteablePath", value[0]);

            if (value.Count > 1)
            {
                for (int i = 1; i <= value.Count - 1; i++)
                {
                    if (i + 1 <= value.Count - 1)
                    {
                        _parameters.Add(value[i], value[i + 1]);
                    }
                    else
                    {
                        LogManager.GetClassLogger().Warn("Invalid launch parameter ({0}).", value[i]);
                    }
                }
            }
        }

        /// <summary>
        ///     Indicating whether the key is available.
        /// </summary>
        /// <param name="key">The Key.</param>
        /// <returns>True if the key is available.</returns>
        public bool KeyAvailable(string key)
        {
            return _parameters.ContainsKey(key);
        }

        /// <summary>
        ///     Converts the LaunchParameters into string.
        /// </summary>
        /// <returns>String.</returns>
        public override string ToString()
        {
            string result = "";

            foreach (var parameter in _parameters)
            {
                result = parameter.Key + " " + parameter.Value + " ";
            }

            result = result.TrimEnd(' ');

            return result;
        }

        /// <summary>
        ///     Creates a new LaunchParameters object.
        /// </summary>
        /// <param name="parameters">The Parameters.</param>
        /// <returns>LaunchParameters.</returns>
        public static LaunchParameters CreateParameters(LaunchParameter[] parameters)
        {
            return new LaunchParameters(parameters);
        }
    }
}