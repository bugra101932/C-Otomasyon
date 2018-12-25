using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication6
{
    public partial class Gecis : Form
    {
        public Gecis()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 ac = new Form2();
            ac.Show();
            Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 ac = new Form3();
            ac.Show();
            Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            yukseklik open = new yukseklik();
            open.Show();
            Visible = false;
        }
    }
}
