using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;


namespace Cerberus
{
    public partial class Form1 : Form
    {

        static DateTime date = DateTime.Now;
        string path = "Week.txt";
        List<Propusk> lst = new List<Propusk>();
        int c = 1;
        public static string[] mass = null;
        int i = -1;
        int l = 0;
        public Form1()
        {
            InitializeComponent();

            label3.Text = $"{date.Date.ToString("dddd")}, {date.Date.ToString("D")}";

            listBox1.Items.Clear();
            try
            {
                using (StreamReader reader = new StreamReader("ListOfStudents.txt"))
                {
                    if (File.ReadAllText("ListOfStudents.txt").ToString() == "")
                    {
                        listBox1.Items.Clear();
                        listBox1.Items.Add($"Нет студентов в списке");
                        listBox1.Enabled = false;
                    }
                    else
                    {
                        for (int i = 0; i < File.ReadAllLines("ListOfStudents.txt").Length; i++)
                        {
                            listBox1.Items.Add($"{reader.ReadLine()}");

                        }
                        listBox1.Enabled = true;
                    }
                }
            }
            catch
            {
                using (StreamWriter writer = new StreamWriter("ListOfStudents.txt", true, System.Text.Encoding.UTF8))
                {
                    writer.Write("");
                    listBox1.Items.Clear();
                    listBox1.Items.Add($"Нет студентов в списке");
                    listBox1.Enabled = false;

                }

            }

        }
        private void button1_Click(object sender, System.EventArgs e)
        {
            if (l == 1 && listBox2.Items.Count != 0)
            {
                if (MessageBox.Show($"Добавленные студенты не сохранены в списке недели.\n Перейти к формированию рапорта без сохранения?", "Внимание!",
              MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    string f = "";
                    try
                    {
                        using (StreamReader reader = new StreamReader("Week.txt"))
                        {
                            f = reader.ReadToEnd();
                        }
                        if (f == "")
                        {
                            if (MessageBox.Show($"Фаил списка недели пуст!\nПродолжить?", "Внимание!",
                                    MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                            {
                                Form fShow = new fShow(f);
                                fShow.ShowDialog();
                            }
                            else
                            {
                                Form fShow = new fShow(f);
                                fShow.ShowDialog();
                            }
                        }
                        else
                        {
                            Form fShow = new fShow(f);
                            fShow.ShowDialog();
                        }


                    }
                    catch
                    {
                        using (StreamWriter writer = new StreamWriter(path, true, System.Text.Encoding.UTF8))
                        {
                        }
                        if (MessageBox.Show($"Фаил списка недели пуст!\nПродолжить?", "Внимание!",
                                        MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                        {
                            Form fShow = new fShow(f);
                            fShow.ShowDialog();
                        }
                    }
                    //Form fShow = new fShow(f);
                    //fShow.ShowDialog();
                }
            }
            else
            {
                string f = "";
                try
                {
                    using (StreamReader reader = new StreamReader("Week.txt"))
                    {
                        f = reader.ReadToEnd();
                    }
                    if (f == "")
                    {
                        if (MessageBox.Show($"Фаил списка недели пуст!\nПродолжить?", "Внимание!",
                                MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                        {
                            Form fShow = new fShow(f);
                            fShow.ShowDialog();
                        }
                    }
                    else
                    {
                        Form fShow = new fShow(f);
                        fShow.ShowDialog();
                    }
                }
                catch
                {
                    using (StreamWriter writer = new StreamWriter(path, true, System.Text.Encoding.UTF8))
                    {
                    }
                    if (MessageBox.Show($"Фаил списка недели пуст!\nПродолжить?", "Внимание!",
                                        MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                    {
                        Form fShow = new fShow(f);
                        fShow.ShowDialog();
                    }
                }
                //Form fShow = new fShow(f);
                //fShow.ShowDialog();

            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                if (cbxCnt.Text != "")
                {
                    if (cbxZnach.Text != "")
                    {

                        lst.Add(new Propusk()
                        {
                            NameStudents = $"{listBox1.SelectedItems[0].ToString()}",
                            DatePropuska = $"{cbxData.Text}",
                            CntChasov = int.Parse(cbxCnt.Text),
                            OboznacheniePropuska = $"{cbxZnach.Text}",
                            OpisaniePropuska = $"{tbxPrich.Text}",
                        });
                        listBox2.Items.Add($"{c.ToString()}. {listBox1.SelectedItems[0].ToString()}|{cbxData.Text}|{cbxCnt.Text}|{cbxZnach.Text}|{tbxPrich.Text}|{Environment.NewLine}");
                        c++;
                        listBox1.SelectedItems.Clear();
                        cbxCnt.Text = "";
                        cbxZnach.Text = "";
                        tbxPrich.Text = "";
                        l = 1;
                        listBox1.Select();
                    }
                }
            }
            listBox1.Select();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string f = "";
            if (checkBox1.Checked == true && listBox2.Items.Count != 0)
            {
                using (StreamWriter writer = new StreamWriter(path, false, System.Text.Encoding.UTF8))
                {
                    writer.Write(f);
                }
                checkBox1.Checked = false;
            }
            lst = lst.OrderBy(t => t.NameStudents).ToList();
            foreach (Propusk propusk in lst)
            {
                f = f + $"{propusk.NameStudents}|{propusk.DatePropuska}|" +
                   $"{propusk.CntChasov}|{propusk.OboznacheniePropuska}|{propusk.OpisaniePropuska}|\n";
            }
            using (StreamWriter writer = new StreamWriter(path, true, System.Text.Encoding.UTF8))
            {
                writer.Write(f);
            }
            listBox2.Items.Clear();
            lst.Clear();
            c = 1;
            l = 0;
            button1.Select();
        }
        private void ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form fUpdateStudents = new fUpdateStudents();
            fUpdateStudents.ShowDialog();
            listBox1.Items.Clear();
            try
            {
                using (StreamReader reader = new StreamReader("ListOfStudents.txt"))
                {
                    if (File.ReadAllText("ListOfStudents.txt").ToString() == "")
                    {
                        listBox1.Items.Clear();
                        listBox1.Items.Add($"Нет студентов в списке");
                        listBox1.Enabled = false;
                    }
                    else
                    {
                        for (int i = 0; i < File.ReadAllLines("ListOfStudents.txt").Length; i++)
                        {
                            listBox1.Items.Add($"{reader.ReadLine()}");

                        }
                        listBox1.Enabled = true;
                    }
                }
            }
            catch
            {
                using (StreamWriter writer = new StreamWriter("ListOfStudents.txt", true, System.Text.Encoding.UTF8))
                {
                    writer.Write("");
                    listBox1.Items.Clear();
                    listBox1.Items.Add($"Нет студентов в списке");
                    listBox1.Enabled = false;
                }
            }
        }
        private void cbxCnt_TextUpdate(object sender, EventArgs e)
        {
            try
            {
                int b = int.Parse(cbxCnt.Text);
                if (b > 0 && b < 8)
                {

                }
                else
                {
                    cbxCnt.Text = string.Empty;
                }
            }
            catch
            {
                cbxCnt.Text = string.Empty;
            }
        }
        private void cbxZnach_TextUpdate(object sender, EventArgs e)
        {
            if (cbxZnach.Text.Length > 4)
            {
                cbxZnach.Text = "";
            }
            if (cbxZnach.Text == "1")
            {
                cbxZnach.Text = "б";
            }
            if (cbxZnach.Text == "2")
            {
                cbxZnach.Text = "в";
            }
            if (cbxZnach.Text == "3")
            {
                cbxZnach.Text = "с";
            }
            if (cbxZnach.Text == "4")
            {
                cbxZnach.Text = "з";
            }
            if (cbxZnach.Text == "5")
            {
                cbxZnach.Text = "б/у";
            }
            if (cbxZnach.Text == "6")
            {
                cbxZnach.Text = "д";
            }
            if (cbxZnach.Text == "7")
            {
                cbxZnach.Text = "н/б";
            }


        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Form fLogin = new fLogin();
            fLogin.ShowDialog();
            if (!AuthLog.IsAuthenticated) Close();
            listBox1.Select();
            try
            {
                using (StreamReader reader = new StreamReader("ColorStyle.txt"))
                {
                    ColorM.colorStyle = Color.FromArgb(int.Parse(reader.ReadLine()));
                    this.BackColor = ColorM.colorStyle;
                    menuStrip1.BackColor = ColorM.colorStyle;
                }
            }
            catch
            {
                ColorM.colorStyle = Color.White;
            }
        }
        private void listBox2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //listBox2.Items.Remove(i);
            if (i >= 0)
            {
                lst.RemoveAt(i);
                c = 1;
                listBox2.Items.Clear();
                foreach (Propusk propusk in lst)
                {
                    listBox2.Items.Add($"{c.ToString()}. {propusk.NameStudents}|{propusk.DatePropuska}|" +
                       $"{propusk.CntChasov}|{propusk.OboznacheniePropuska}|{propusk.OpisaniePropuska}|\n");
                    c++;
                }
            }
            i = -1;
        }
        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            i = listBox2.SelectedIndex;
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Form fAbout = new fAbout();
            fAbout.ShowDialog();
        }

        private void cbxZnach_Leave(object sender, EventArgs e)
        {
            string g = cbxZnach.Text;
            g = g.ToLower();
            if (g != "б")
            {
                if (g != "в")
                {
                    if (g != "с")
                    {
                        if (g != "з")
                        {
                            if (g != "б/у")
                            {
                                if (g != "д")
                                {
                                    if (g != "н/б")
                                    {
                                        cbxZnach.Text = "";
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void tbxPrich_TextChanged(object sender, EventArgs e)
        {
            label8.Text = $"{tbxPrich.Text.Length}/100";
            if (!tbxPrich.Text.Contains("\\n") && !tbxPrich.Text.Contains("|")) 
            {

            }
        }

        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cbxZnach.Select();
            }
        }

        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {
            cbxZnach.Select();
        }

        private void cbxZnach_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cbxCnt.Select();
            }
        }

        private void cbxCnt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cbxData.Select();
            }
        }

        private void cbxData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button3.Select();
            }
        }
        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
                Form fCalendary = new fCalendary();
                fCalendary.Show();
        }

        private void фаилСтудентовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(@"ListOfStudents.txt");
            }
            catch 
            {
                MessageBox.Show($"Фаил списка студентов не существует!", "Внимание!",
                                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void фаилНеделиToolStripMenuItem_Click(object sender, EventArgs e)
        {

            try
            {
                Process.Start(@"Week.txt");
            }
            catch
            {
                MessageBox.Show($"Фаил недели не существует!", "Внимание!",
                                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void фаилИсторииToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(@"Histori.txt");
            }
            catch
            {
                MessageBox.Show($"Фаил истории студентов не существует!", "Внимание!",
                                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void файлЦветаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(@"ColorStyle.txt");
            }
            catch
            {
                MessageBox.Show($"Фаил стиля не существует!", "Внимание!",
                                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e) //голубой
        {
            using (StreamWriter writer = new StreamWriter("ColorStyle.txt", false, System.Text.Encoding.UTF8))
            {
                writer.WriteLine(Color.LightSteelBlue.ToArgb());
            }
            this.BackColor = Color.LightSteelBlue;
            menuStrip1.BackColor = Color.LightSteelBlue;
            ColorM.colorStyle = Color.LightSteelBlue;
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)//зеленый
        {
            using (StreamWriter writer = new StreamWriter("ColorStyle.txt", false, System.Text.Encoding.UTF8))
            {
                writer.WriteLine(Color.OliveDrab.ToArgb());
            }
            this.BackColor = Color.OliveDrab;
            menuStrip1.BackColor = Color.OliveDrab;
            ColorM.colorStyle = Color.OliveDrab;
        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)//светло-зеленый
        {
            using (StreamWriter writer = new StreamWriter("ColorStyle.txt", false, System.Text.Encoding.UTF8))
            {
                writer.WriteLine(Color.DarkSeaGreen.ToArgb());
            }
            this.BackColor = Color.DarkSeaGreen;
            menuStrip1.BackColor = Color.DarkSeaGreen;
            ColorM.colorStyle = Color.DarkSeaGreen;
        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)//красный 
        {
            using (StreamWriter writer = new StreamWriter("ColorStyle.txt", false, System.Text.Encoding.UTF8))
            {
                writer.WriteLine(Color.Firebrick.ToArgb());
            }
            this.BackColor = Color.Firebrick;
            menuStrip1.BackColor = Color.Firebrick;
            ColorM.colorStyle = Color.Firebrick;
        }

        private void toolStripMenuItem10_Click(object sender, EventArgs e)// розовый
        {
            using (StreamWriter writer = new StreamWriter("ColorStyle.txt", false, System.Text.Encoding.UTF8))
            {
                writer.WriteLine(Color.Pink.ToArgb());
            }
            this.BackColor = Color.Pink;
            menuStrip1.BackColor = Color.Pink;
            ColorM.colorStyle = Color.Pink;
        }
        private void toolStripMenuItem12_Click(object sender, EventArgs e)//белый
        {
            using (StreamWriter writer = new StreamWriter("ColorStyle.txt", false, System.Text.Encoding.UTF8))
            {
                writer.WriteLine(Color.White.ToArgb());
            }
            this.BackColor = Color.White;
            menuStrip1.BackColor = Color.White;
            ColorM.colorStyle = Color.White;
        }
        private void panel4_MouseDown(object sender, MouseEventArgs e)
        {
            panel4.Capture = false;
            Message m = Message.Create(base.Handle, 0xa1, new IntPtr(2), IntPtr.Zero);
            WndProc(ref m);
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void свойЦветToolStripMenuItem_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            ColorM.colorStyle = colorDialog1.Color;

            using (StreamWriter writer = new StreamWriter("ColorStyle.txt", false, System.Text.Encoding.UTF8))
            {
                writer.WriteLine(ColorM.colorStyle.ToArgb());
            }
            this.BackColor = ColorM.colorStyle;
            menuStrip1.BackColor = ColorM.colorStyle;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void pictureBox3_MouseMove(object sender, MouseEventArgs e)
        {
            pictureBox3.Image = Cerberus.Properties.Resources.collapseB;
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            pictureBox3.Image = Cerberus.Properties.Resources.collapseBlack;
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



