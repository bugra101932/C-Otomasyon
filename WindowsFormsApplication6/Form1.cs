using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.Data.SqlClient;

namespace WindowsFormsApplication6
{
    public partial class Form1 : Form
    {
        SqlConnection baglan= new SqlConnection("Data Source=BUGRAA;Initial Catalog=Besyo;Integrated Security=True");

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try {
                baglan.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = baglan;
                int no = Convert.ToInt32(textBox1.Text);
                cmd.CommandText = "SELECT Sifre FROM Giris WHERE No = '" + no + "' ";
                string sifre = cmd.ExecuteScalar().ToString();

                if (sifre == textBox2.Text)
                {
                    Gecis ac = new Gecis();
                    ac.Show();

                    Visible = false;
                }
                else {
                    textBox3.Visible = true;
                }
            }
            catch {
                textBox3.Visible = true;


            }
            baglan.Close();


            


            


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox3.Visible = false;
        }
    }
}
