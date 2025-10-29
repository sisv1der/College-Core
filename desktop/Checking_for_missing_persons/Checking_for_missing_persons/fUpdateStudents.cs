using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Cerberus
{
    public partial class fUpdateStudents : Form
    {
        int k = 0;
        int i = -1;
        public fUpdateStudents()
        {
            InitializeComponent();
            try
            {
                using (StreamReader reader = new StreamReader("ListOfStudents.txt"))
                {
                    if (File.ReadAllText("ListOfStudents.txt").ToString() == "")
                    {
                        MessageBox.Show($"Фаил списка пуст!", "Внимание",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        for (int i = 0; i < File.ReadAllLines("ListOfStudents.txt").Length; i++)
                        {
                            listBox1.Items.Add($"{reader.ReadLine()}\n");
                        }
                    }
                }
            }
            catch
            {
                using (StreamWriter writer = new StreamWriter("ListOfStudents.txt", true, System.Text.Encoding.UTF8))
                {
                    writer.Write("");
                }
            }
        }


        private void button2_Click(object sender, EventArgs e)
        { 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (StreamWriter writer = new StreamWriter("ListOfStudents.txt", false, System.Text.Encoding.UTF8))
            {
            }
            using (StreamWriter writer = new StreamWriter("ListOfStudents.txt", true, System.Text.Encoding.UTF8))
            {
                for (int j = 0; j < listBox1.Items.Count; j++)
                {
                    writer.Write(listBox1.Items[j].ToString());
                }
            }
            Close();
            k = 0;
            label3.Text = $"Всего: {listBox1.Items.Count}";

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox1.Text != " " && !textBox1.Text.Contains("\\n") && !textBox1.Text.Contains("|"))
            {
                listBox1.Items.Add($"{textBox1.Text}\n");
                textBox1.Text = "";
                k = 1;
            }
            else
            {
                MessageBox.Show($"Имя студента отсутствует или \nимеет недопустимые символы", "Внимание",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            label3.Text = $"Всего: {listBox1.Items.Count}";

        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (i >= 0)
            {
                listBox1.Items.RemoveAt(i);
                k = 1;
            }
            label3.Text = $"Всего: {listBox1.Items.Count}";

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            i = listBox1.SelectedIndex;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show($"Очистить список?", "Внимание!",
              MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                listBox1.Items.Clear();
                k = 1;
            }
            label3.Text = $"Всего: {listBox1.Items.Count}";
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (k == 1)
            {
                if (MessageBox.Show($"Изменения не сохранены!\nПродолжить?", "Внимание!",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    Close();
                }
            }
            else 
            {
                Close();
            }
        }

        private void fUpdateStudents_Load(object sender, EventArgs e)
        {
            this.BackColor = ColorM.colorStyle;
            label3.Text = $"Всего: {listBox1.Items.Count}";
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

