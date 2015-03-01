namespace Sharpex2D.Rendering.OpenGL
{
    public class OpenGLGraphicsManager : GraphicsManager
    {
        public override bool IsSupported
        {
            get
            {
                return true;
                //lie
            }
        }

        public override IRenderer Create()
        {
            return new OpenGLRenderer();
        }
    }
}
