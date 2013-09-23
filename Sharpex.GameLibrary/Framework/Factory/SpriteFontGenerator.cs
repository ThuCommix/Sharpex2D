using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SharpexGL.Framework.Factory
{
    public static class SpriteFontGenerator
    {
        public static void Show()
        {
            SpriteFontDialog spriteFontDialog = new SpriteFontDialog();
            if (spriteFontDialog.ShowDialog() == DialogResult.OK)
            {
            }
        }
    }
}
