using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace Cerberus
{
    public partial class fShow : Form
    {
        string path = "Week.txt";
        List<Propusk> lst;
        public fShow(string t)
        {
            InitializeComponent();
            richTextBox1.Text = t;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string f = richTextBox1.Text;
            using (StreamWriter writer = new StreamWriter(path, false, System.Text.Encoding.UTF8))
            {
                writer.Write(f);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show($"Очистить список?", "Внимание!",
              MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                richTextBox1.Text = "";
                Close();
                using (StreamWriter writer = new StreamWriter(path, false, System.Text.Encoding.UTF8))
                {
                    writer.Write(richTextBox1.Text);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string f = richTextBox1.Text;
            using (StreamWriter writer = new StreamWriter(path, false, System.Text.Encoding.UTF8))
            {
                writer.Write(f);
            }
            Form fPrint = new fPrint(f);
            fPrint.ShowDialog();
            Close();

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                richTextBox1.Enabled = true;
                MessageBox.Show($"Отображаемое форматирование является техническим.\r\nПри редактировании соблюдать структуру:\r\nИмя студента|Дата пропуска|Кол-во часов(от 1 до 7)|\nПричина|Комментарий|\r\nПри несоблюдении структуры неизбежен сбой в работе программы\r\n", "Внимание!",
              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                button1.Enabled = true;
                button1.Visible = true;
                label2.Visible = true;
            }
            if (checkBox1.Checked == false) 
            {
                richTextBox1.Enabled = false;
                button1.Enabled = false;
                button1.Visible = false;
                label2.Visible = false;
            }
        }

        private void fShow_Load(object sender, EventArgs e)
        {
            this.BackColor = ColorM.colorStyle;
        }

        private void panel4_MouseDown(object sender, MouseEventArgs e)
        {
            panel4.Capture = false;
            Message m = Message.Create(base.Handle, 0xa1, new IntPtr(2), IntPtr.Zero);
            WndProc(ref m);
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void pictureBox2_MouseMove(object sender, MouseEventArgs e)
        {
            pictureBox2.Image = Cerberus.Properties.Resources.CloseR;
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.Image = Cerberus.Properties.Resources.CloseB;
        }
    }
}
