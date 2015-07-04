using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Postbuild.Adjuster
{
    public sealed class BottomPanel : Panel
    {
        public BottomPanel()
        {
            Size = new Size(100, 30);
            Dock = DockStyle.Bottom;
            Paint += BottomPanel_Paint;
            BackColor = SystemColors.Control;
        }

        void BottomPanel_Paint(object sender, PaintEventArgs e)
        {
            using (var pen = new Pen(new SolidBrush(Color.LightGray)))
            {
                e.Graphics.DrawLine(pen, 0, 1, Width, 1);
            }
        }
    }
}
