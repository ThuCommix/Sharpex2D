using System.Threading;

namespace SharpexGL.Framework.Rendering
{
    public class AccurateFpsCounter
    {
        private bool _flag;
        /// <summary>
        /// Gets or sets the drawcount.
        /// </summary>
        private int Draws
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the frames per second.
        /// </summary>
        public int FramesPerSecond
        {
            get;
            set;
        }
        /// <summary>
        /// Initializes a new DrawInfo.
        /// </summary>
        public AccurateFpsCounter()
        {
            Draws = 0;
            FramesPerSecond = 0;
        }
        /// <summary>
        /// Starts the counter.
        /// </summary>
        public void Start()
        {
            var thread = new Thread(Intervaller);
            _flag = true;
            thread.Start();
        }
        /// <summary>
        /// The Intervaller void.
        /// </summary>
        private void Intervaller()
        {
            while (_flag)
            {
                Thread.Sleep(1000);
                FramesPerSecond = Draws;
                Draws = 0;
            }
        }
        /// <summary>
        /// Stops the counter.
        /// </summary>
        public void Stop()
        {
            _flag = false;
        }
        /// <summary>
        /// Resets the counter.
        /// </summary>
        public void Reset()
        {
            _flag = false;
            Draws = 0;
        }
        /// <summary>
        /// Adds a new draw to counter.
        /// </summary>
        public void AddDraw()
        {
            Draws++;
        }
    }
}
