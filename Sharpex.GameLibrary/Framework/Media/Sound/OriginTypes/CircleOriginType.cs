using System;

namespace SharpexGL.Framework.Media.Sound.OriginTypes
{
    public class CircleOriginType : IOriginType
    {
        #region IOriginType Implementation

        /// <summary>
        /// Gets the Identifer.
        /// </summary>
        public Guid Guid { get; private set; }

        #endregion

        public CircleOriginType()
        {
            Guid = new Guid("74F64914-A3E5-4A1F-95F3-C8CEAC8E8F6A");
            Radius = 10;
        }

        /// <summary>
        /// Sets or gets the Circle Radius.
        /// </summary>
        public float Radius { set; get; }
    }
}
