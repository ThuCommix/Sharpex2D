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
using System.Linq;
using System.Reflection;
using Sharpex2D.Framework;

namespace ContentPipeline.Actions
{
    public class OptionsAction : IAction
    {
        /// <summary>
        /// Gets the option name.
        /// </summary>
        public string Option => "--options";

        /// <summary>
        /// Executes the action.
        /// </summary>
        /// <param name="args">The Arguments.</param>
        /// <returns>Application ExitCode.</returns>
        public int Execute(string[] args)
        {
            var actions = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(x => typeof (IAction).IsAssignableFrom(x) && !x.IsInterface)
                .Select(type => (IAction) Activator.CreateInstance(type));

            foreach (var action in actions)
            {
                Console.WriteLine(action.Option);
            }

            return 0;
        }
    }
}

