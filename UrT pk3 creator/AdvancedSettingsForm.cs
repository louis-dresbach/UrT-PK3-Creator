using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace UrT_pk3_creator
{
    public partial class AdvancedSettingsForm : Form
    {
        public AdvancedSettingsForm()
        {
            InitializeComponent();
            LoadSettings();
        }

        private void LoadSettings()
        {
            // GENERAL
            textBox7.Text = Properties.Settings.Default.CustomPK3Name;
            checkBox1.Checked = Properties.Settings.Default.CustomBackgroundEnabled;
            textBox1.Text = Properties.Settings.Default.CustomBackground;

            // EXTRA PAGE
            checkBox2.Checked = Properties.Settings.Default.ExtraPageEnabled;
            textBox2.Text = Properties.Settings.Default.ExtraPageButton;
            textBox3.Text = Properties.Settings.Default.ExtraPageLeft;
            textBox4.Text = Properties.Settings.Default.ExtraPageTopRight;
            textBox5.Text = Properties.Settings.Default.ExtraPageBottomRight;
            textBox6.Text = Properties.Settings.Default.ExtraPageTitle;

            // HELP
            Font fnt = new Font("Verdana", 12F, FontStyle.Bold, GraphicsUnit.Point);
            richTextBox1.SelectionFont = fnt;
            richTextBox1.SelectionColor = Color.Red;
            richTextBox1.SelectedText = "^1: RED";
            richTextBox1.SelectionColor = Color.Green;
            richTextBox1.SelectedText = Environment.NewLine + "^2: GREEN";
            richTextBox1.SelectionColor = Color.Yellow;
            richTextBox1.SelectedText = Environment.NewLine + "^3: YELLOW";
            richTextBox1.SelectionColor = Color.Blue;
            richTextBox1.SelectedText = Environment.NewLine + "^4: BLUE";
            richTextBox1.SelectionColor = Color.Cyan;
            richTextBox1.SelectedText = Environment.NewLine + "^5: CYAN";
            richTextBox1.SelectionColor = Color.Violet;
            richTextBox1.SelectedText = Environment.NewLine + "^6: VIOLET";
            richTextBox1.SelectionColor = Color.Gray;
            richTextBox1.SelectedText = Environment.NewLine + "^7: WHITE";
            richTextBox1.SelectionColor = Color.Black;
            richTextBox1.SelectedText = Environment.NewLine + "^8: BLACK";
        }

        private void SaveSettings()
        {
            // GENERAL
            Properties.Settings.Default.CustomPK3Name = textBox7.Text;
            Properties.Settings.Default.CustomBackground = textBox1.Text;
            Properties.Settings.Default.CustomBackgroundEnabled = checkBox1.Checked;

            // EXTRA PAGE
            Properties.Settings.Default.ExtraPageEnabled = checkBox2.Checked;
            Properties.Settings.Default.ExtraPageButton = textBox2.Text;
            Properties.Settings.Default.ExtraPageLeft = textBox3.Text;
            Properties.Settings.Default.ExtraPageTopRight = textBox4.Text;
            Properties.Settings.Default.ExtraPageBottomRight = textBox5.Text;
            Properties.Settings.Default.ExtraPageTitle = textBox6.Text;
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            checkBox1.Checked = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveSettings();
            this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            checkBox1.Checked = true;

            OpenFileDialog of = new OpenFileDialog();
            of.CheckFileExists = true;
            of.Multiselect = false;
            of.Title = "Select custom background image";
            of.Filter = "All Images |*.bmp;*.tiff;*.jpg;*.jpeg;*.bmp;*.gif;*.png|All Files |*.*";

            if (of.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = of.FileName;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            customTabControl1.SelectedIndex = 0;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            customTabControl1.SelectedIndex = 1;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            customTabControl1.SelectedIndex = 2;
        }
    }
}
