using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cerberus
{
    public partial class fLogin : Form
    {
        public fLogin()
        {
            InitializeComponent();
            AuthLog.AddUser("Log", "Pas");
        }

        private void fLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AuthLog.AuthenticateUser(textBox1.Text,textBox2.Text);
            if (AuthLog.IsAuthenticated)
            {
                Close();
            }
            else
            {
                MessageBox.Show($"Неверный логин или пароль!", "Внимание!",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void fLogin_Load(object sender, EventArgs e)
        {
            try
            {
                using (StreamReader reader = new StreamReader("ColorStyle.txt"))
                {
                    ColorM.colorStyle = Color.FromArgb(int.Parse(reader.ReadLine()));
                    this.BackColor = ColorM.colorStyle;
                }
            }
            catch
            {
                ColorM.colorStyle = Color.White;
                this.BackColor = Color.White;
            }
            if (textBox1.Text == "")
            {
                textBox1.Select();
            }
            else 
            {
                if (textBox2.Text == "")
                {
                    textBox2.Select();
                }
                else 
                {
                    button1.Select();
                }
            }
        }
        private void button2_MouseDown(object sender, MouseEventArgs e)
        {
            textBox2.PasswordChar = '\0';
            button2.BackgroundImage = Cerberus.Properties.Resources._11111111111111111111;
        }

        private void button2_MouseUp(object sender, MouseEventArgs e)
        {
            textBox2.PasswordChar = '*';
            button2.BackgroundImage = Cerberus.Properties.Resources.free_icon_hide_2767146;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            panel1.Capture = false;
            Message m = Message.Create(base.Handle, 0xa1, new IntPtr(2), IntPtr.Zero);
            WndProc(ref m);
        }
    }
}
