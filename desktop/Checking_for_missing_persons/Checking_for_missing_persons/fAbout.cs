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
    public partial class fAbout : Form
    {
        public fAbout()
        {
            InitializeComponent();
        }

        private void fAbout_Load(object sender, EventArgs e)
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
