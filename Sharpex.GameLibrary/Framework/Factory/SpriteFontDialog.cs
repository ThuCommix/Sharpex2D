using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;
using SharpexGL.Framework.Content.Serialization;
using SharpexGL.Framework.Rendering.Font;

namespace SharpexGL.Framework.Factory
{
    internal class SpriteFontDialog : Form
    {
        private SpriteFont _currentSpriteFont;
        private IContainer components = null;
        private ComboBox comboBox1;
        private ComboBox comboBox2;
        private ComboBox comboBox3;
        private PictureBox pictureBox1;
        private NumericUpDown numericUpDown1;
        private NumericUpDown numericUpDown2;
        private Label label1;
        private Label label2;
        private Button button2;
        public SpriteFontDialog()
        {
            this.InitializeComponent();
        }
        private void SpriteFontDialog_Load(object sender, EventArgs e)
        {
            this.RetrieveFonts();
            this.comboBox1.SelectedIndex = 0;
            this.comboBox2.SelectedIndex = 0;
            this.comboBox3.SelectedIndex = 0;
        }
        private void RetrieveFonts()
        {
            FontFamily[] families = FontFamily.Families;
            FontFamily[] array = families;
            for (int i = 0; i < array.Length; i++)
            {
                FontFamily fontFamily = array[i];
                this.comboBox1.Items.Add(fontFamily.Name);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (this.comboBox3.SelectedItem.ToString() == "Regular")
            {
                this._currentSpriteFont = new SpriteFont((int)this.numericUpDown1.Value, (int)this.numericUpDown2.Value, "This is a new SpriteFont rendered Text", new Font(this.comboBox1.SelectedItem.ToString(), (float)Convert.ToInt32(this.comboBox2.SelectedItem.ToString()), FontStyle.Regular), Rendering.Color.Black);
            }
            if (this.comboBox3.SelectedItem.ToString() == "Italic")
            {
                this._currentSpriteFont = new SpriteFont((int)this.numericUpDown1.Value, (int)this.numericUpDown2.Value, "This is a new SpriteFont rendered Text", new Font(this.comboBox1.SelectedItem.ToString(), (float)Convert.ToInt32(this.comboBox2.SelectedItem.ToString()), FontStyle.Italic), Rendering.Color.Black);
            }
            if (this.comboBox3.SelectedItem.ToString() == "Bold")
            {
                this._currentSpriteFont = new SpriteFont((int)this.numericUpDown1.Value, (int)this.numericUpDown2.Value, "This is a new SpriteFont rendered Text", new Font(this.comboBox1.SelectedItem.ToString(), (float)Convert.ToInt32(this.comboBox2.SelectedItem.ToString()), FontStyle.Bold), Rendering.Color.Black);
            }
            this.pictureBox1.BackgroundImageLayout = ImageLayout.Center;
            this.pictureBox1.BackgroundImage = this._currentSpriteFont.Render().Texture2D;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "SpriteFont (*.snb)|*.snb";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (var fileStream = new FileStream(saveFileDialog.FileName, FileMode.Create))
                {
                    new SpriteFontSerializer().Write(new BinaryWriter(fileStream), _currentSpriteFont);
                }
            }
        }
        private void Changed()
        {
            try
            {
                if (this.comboBox3.SelectedItem.ToString() == "Regular")
                {
                    this._currentSpriteFont = new SpriteFont((int)this.numericUpDown1.Value, (int)this.numericUpDown2.Value, "This is a new SpriteFont rendered Text", new Font(this.comboBox1.SelectedItem.ToString(), (float)Convert.ToInt32(this.comboBox2.SelectedItem.ToString()), FontStyle.Regular), Rendering.Color.Black);
                }
                if (this.comboBox3.SelectedItem.ToString() == "Italic")
                {
                    this._currentSpriteFont = new SpriteFont((int)this.numericUpDown1.Value, (int)this.numericUpDown2.Value, "This is a new SpriteFont rendered Text", new Font(this.comboBox1.SelectedItem.ToString(), (float)Convert.ToInt32(this.comboBox2.SelectedItem.ToString()), FontStyle.Italic), Rendering.Color.Black);
                }
                if (this.comboBox3.SelectedItem.ToString() == "Bold")
                {
                    this._currentSpriteFont = new SpriteFont((int)this.numericUpDown1.Value, (int)this.numericUpDown2.Value, "This is a new SpriteFont rendered Text", new Font(this.comboBox1.SelectedItem.ToString(), (float)Convert.ToInt32(this.comboBox2.SelectedItem.ToString()), FontStyle.Bold), Rendering.Color.Black);
                }
                this.pictureBox1.BackgroundImageLayout = ImageLayout.Center;
                this.pictureBox1.BackgroundImage = this._currentSpriteFont.Render().Texture2D;
                this.button2.Enabled = true;
            }
            catch (Exception)
            {
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Changed();
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Changed();
        }
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Changed();
        }
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            this.Changed();
        }
        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            this.Changed();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SpriteFontDialog));
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(12, 31);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(238, 23);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31",
            "32",
            "33",
            "34",
            "35",
            "36",
            "37",
            "38",
            "39",
            "40"});
            this.comboBox2.Location = new System.Drawing.Point(256, 31);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(88, 23);
            this.comboBox2.TabIndex = 1;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // comboBox3
            // 
            this.comboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Items.AddRange(new object[] {
            "Regular",
            "Italic",
            "Bold"});
            this.comboBox3.Location = new System.Drawing.Point(350, 31);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(88, 23);
            this.comboBox3.TabIndex = 2;
            this.comboBox3.SelectedIndexChanged += new System.EventHandler(this.comboBox3_SelectedIndexChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Location = new System.Drawing.Point(12, 97);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(507, 227);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(69, 64);
            this.numericUpDown1.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(58, 23);
            this.numericUpDown1.TabIndex = 5;
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(192, 64);
            this.numericUpDown2.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(58, 23);
            this.numericUpDown2.TabIndex = 6;
            this.numericUpDown2.ValueChanged += new System.EventHandler(this.numericUpDown2_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 15);
            this.label1.TabIndex = 7;
            this.label1.Text = "Kerning:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(133, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 15);
            this.label2.TabIndex = 8;
            this.label2.Text = "Spaceing:";
            // 
            // button2
            // 
            this.button2.Enabled = false;
            this.button2.Location = new System.Drawing.Point(444, 31);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 9;
            this.button2.Text = "Save";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // SpriteFontDialog
            // 
            this.ClientSize = new System.Drawing.Size(531, 336);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUpDown2);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.comboBox3);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.comboBox1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(547, 374);
            this.MinimumSize = new System.Drawing.Size(547, 374);
            this.Name = "SpriteFontDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SpriteFont Generator";
            this.Load += new System.EventHandler(this.SpriteFontDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
