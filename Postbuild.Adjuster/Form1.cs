using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Postbuild.Adjuster
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var opf = new OpenFileDialog {Filter = ".csproj|.vbproj"})
            {
                if (opf.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    textBox1.Text = opf.FileName;
                }
            }
        }
    }
}
