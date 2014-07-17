using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Sharpex2D.GameService;

namespace FlyingBirdLauncher
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox2.SelectedIndex = 1;
            comboBox1.SelectedIndex = 1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!File.Exists("FlyingBird.exe"))
            {
                MessageBox.Show("FlyingBird.exe konnte nicht gefunden werden.", "FlyingBird Launcher",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            LaunchParameters parameters =
                LaunchParameters.CreateParameters(new[]
                {new LaunchParameter("Resolution", comboBox2.Text), new LaunchParameter("Device", comboBox1.Text)});

            var process = new Process();
            process.StartInfo.Arguments = parameters.ToString();
            process.StartInfo.FileName = "FlyingBird.exe";
            process.StartInfo.UseShellExecute = true;

            process.Start();
            Application.Exit();
        }
    }
}