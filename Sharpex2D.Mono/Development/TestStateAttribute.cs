using System;

namespace Sharpex2D
{
    public class TestStateAttribute : Attribute
    {
        /// <summary>
        ///     Initializes a new TestStateAttribute class.
        /// </summary>
        /// <param name="state">The TestState.</param>
        public TestStateAttribute(TestState state)
        {
            State = state;
        }

        public TestState State { private set; get; }
    }
}