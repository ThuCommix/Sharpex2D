using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using SharpexGL.Framework.Content.Serialization;
using SharpexGL.Framework.Rendering;

namespace SharpexGL.Framework.Factory
{
    internal class TextureDialog : Form
    {
        private IContainer components = null;
        private Button button1;
        private Button button2;
        private PictureBox pictureBox1;
        public TextureDialog()
        {
            this.InitializeComponent();
        }
        private void TextureDialog_Load(object sender, EventArgs e)
        {
        }
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "PNG(*.png)|*.png|Jpg(*.jpg)|*.jpg|Jpeg(*.jpeg)|*.jpeg|Bmp(*.bmp)|*.bmp";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.pictureBox1.BackgroundImage = Image.FromFile(openFileDialog.FileName);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Texture(*.snb)|*.snb";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                Texture graph = new Texture
                {
                    Texture2D = (Bitmap)this.pictureBox1.BackgroundImage
                };

                using (var fileStream = new FileStream(saveFileDialog.FileName, FileMode.Create))
                {
                    new TextureSerializer().Write(new BinaryWriter(fileStream), graph);
                }
            }
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TextureDialog));
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(290, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Load";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(290, 41);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Save";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(272, 224);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // TextureDialog
            // 
            this.ClientSize = new System.Drawing.Size(377, 248);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(393, 286);
            this.MinimumSize = new System.Drawing.Size(393, 286);
            this.Name = "TextureDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Texture Generator";
            this.Load += new System.EventHandler(this.TextureDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }
    }
}
