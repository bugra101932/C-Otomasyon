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
using System.Net.Mail;

namespace WindowsFormsApplication6
{
    public partial class Form2 : Form
    {
        string gelen = "0";
        SqlConnection baglanti = new SqlConnection("Data Source=BUGRAA;Initial Catalog=Besyo;Integrated Security=True");
        public Form2()
        {
            InitializeComponent();
        }
       /* public bool Gonder(string konu, string icerik, string eposta)
        {
            MailMessage ePosta = new MailMessage();
            ePosta.From = new MailAddress("blg.bugra@hotmail.com");
            //
            ePosta.To.Add(eposta);

            // 
            //
            ePosta.Subject = konu;
            //
            ePosta.Body = icerik;
            //
            SmtpClient smtp = new SmtpClient();
            //
            smtp.Credentials = new System.Net.NetworkCredential("blg.bugra@hotmail.com", "23734bugra2");
            smtp.Port = 587;
            smtp.Host = "smtp.hotmail.com";
            smtp.EnableSsl = true;
            object userState = ePosta;
            bool kontrol = true;
            try
            {
                smtp.SendAsync(ePosta, (object)ePosta);
            }
            catch (SmtpException ex)
            {
                kontrol = false;
                System.Windows.Forms.MessageBox.Show(ex.Message, "Mail Gönderme Hatasi");
            }
            return kontrol;
        }*/
        private void Form2_Load(object sender, EventArgs e)
        {
            serialPort1.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = baglanti;
                int ogrno = Convert.ToInt32(textBox2.Text);
                cmd.CommandText = "SELECT Ad FROM bilgiler WHERE Ogrenci_NO = '" + ogrno + "' ";
                string adi = cmd.ExecuteScalar().ToString();
                textBox3.Text = adi;
                cmd.CommandText = "SELECT Soyad FROM bilgiler WHERE Ogrenci_NO = '" + ogrno + "' ";
                string soyadi = cmd.ExecuteScalar().ToString();
                textBox4.Text = soyadi;
                cmd.CommandText = "SELECT Boy FROM bilgiler WHERE Ogrenci_NO = '" + ogrno + "' ";
                string boy = cmd.ExecuteScalar().ToString();
                textBox5.Text = boy;
                cmd.CommandText = "SELECT Kilo FROM bilgiler WHERE Ogrenci_NO = '" + ogrno + "' ";
                string kilo = cmd.ExecuteScalar().ToString();
                textBox6.Text = kilo;
                cmd.CommandText = "SELECT Cinsiyet FROM bilgiler WHERE Ogrenci_NO = '" + ogrno + "' ";
                string cinsiyet = cmd.ExecuteScalar().ToString();
                textBox7.Text = cinsiyet;

                baglanti.Close();



            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            serialPort1.Close();
            serialPort1.PortName = "COM3";
            serialPort1.Open();
            gelen = serialPort1.ReadLine();
            textBox8.Text = gelen.ToString();
           // string[] kulvar = gelen.Split('*');
            // textBox16.Text = kulvar[0];
            textBox16.Text =textBox8.Text;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            serialPort1.Close();
            serialPort1.PortName = "COM3";
            serialPort1.Open();
            gelen = serialPort1.ReadLine();
            
            textBox8.Text = gelen.ToString();
           
            textBox1.Text = textBox8.Text;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
                SqlCommand cmd2 = new SqlCommand();
                cmd2.Connection = baglanti;
                int ogrno = Convert.ToInt32(textBox2.Text);
                int geldi_sure = Convert.ToInt32(textBox1.Text);
                cmd2.CommandText = "UPDATE bilgiler SET Sure='" + geldi_sure + "' WHERE Ogrenci_NO LIKE  '" + ogrno + "'  ";
                cmd2.ExecuteNonQuery();
                int süre = Convert.ToInt32(textBox1.Text);
                süre = süre / 1000;
                if (süre < 20)
                {
                    int not = 100 / 4;
                    cmd2.CommandText = "UPDATE bilgiler SET KPuan='" + not + "' WHERE Ogrenci_NO LIKE  '" + ogrno + "'  ";

                }
                else if (süre>20 && süre<50) {
                    int not = 85 / 4;
                    cmd2.CommandText = "UPDATE bilgiler SET KPuan='" + not + "' WHERE Ogrenci_NO LIKE  '" + ogrno + "'  ";

                }
                else if (süre > 50 && süre < 70)
                {
                    int not = 70 / 4;
                    cmd2.CommandText = "UPDATE bilgiler SET KPuan='" + not + "' WHERE Ogrenci_NO LIKE  '" + ogrno + "'  ";

                }
                else if (süre > 70 && süre < 80)
                {
                    int not = 55 / 4;
                    cmd2.CommandText = "UPDATE bilgiler SET KPuan='" + not + "' WHERE Ogrenci_NO LIKE  '" + ogrno + "'  ";

                }
                else if (süre > 80 && süre < 100)
                {
                    int not = 40 / 4;
                    cmd2.CommandText = "UPDATE bilgiler SET KPuan='" + not + "' WHERE Ogrenci_NO LIKE  '" + ogrno + "'  ";

                }
                else if (süre > 100)
                {
                    
                    cmd2.CommandText = "UPDATE bilgiler SET KPuan='" + 0 + "' WHERE Ogrenci_NO LIKE  '" + ogrno + "'  ";

                }
                cmd2.ExecuteNonQuery();

                SqlCommand cmd3 = new SqlCommand();
                cmd2.Connection = baglanti;
                int ogrno2 = Convert.ToInt32(textBox2.Text);
                cmd2.CommandText = "SELECT Mail FROM resimli WHERE numara LIKE  '" + ogrno + "'  ";

                cmd2.ExecuteNonQuery();
                string mail = cmd2.ExecuteScalar().ToString();
                string konu = "Koşu Sınav Sonucunuz :";

                SqlCommand cmd4 = new SqlCommand();
                cmd2.Connection = baglanti;
                int ogrno4 = Convert.ToInt32(textBox2.Text);
                cmd2.CommandText = "SELECT KPuan FROM bilgiler WHERE Ogrenci_No LIKE  '" + ogrno + "'  ";

                cmd2.ExecuteNonQuery();
                string icerik = cmd2.ExecuteScalar().ToString();

                //Gonder(konu, icerik, mail);

                baglanti.Close();

            }
            else
            {
                SqlCommand cmd2 = new SqlCommand();
                cmd2.Connection = baglanti;
                int ogrno = Convert.ToInt32(textBox2.Text);
                int geldi_sure = Convert.ToInt32(textBox1.Text);
                cmd2.CommandText = "UPDATE bilgiler SET Sure='" + geldi_sure + "' WHERE Ogrenci_NO LIKE  '" + ogrno + "'  ";
                cmd2.ExecuteNonQuery();
                int süre = Convert.ToInt32(textBox1.Text);
                if (süre < 20)
                {
                    int not = 100 / 4;
                    cmd2.CommandText = "UPDATE bilgiler SET KPuan='" + not + "' WHERE Ogrenci_NO LIKE  '" + ogrno + "'  ";

                }
                else if (süre > 20 && süre < 50)
                {
                     int not = 85 / 4;
                    cmd2.CommandText = "UPDATE bilgiler SET KPuan='" + not + "' WHERE Ogrenci_NO LIKE  '" + ogrno + "'  ";

                }
                else if (süre > 50 && süre < 70)
                {
                    int not = 70 / 4;
                    cmd2.CommandText = "UPDATE bilgiler SET KPuan='" + not + "' WHERE Ogrenci_NO LIKE  '" + ogrno + "'  ";

                }
                else if (süre > 70 && süre < 80)
                {
                    int not = 55 / 4;
                    cmd2.CommandText = "UPDATE bilgiler SET KPuan='" + not + "' WHERE Ogrenci_NO LIKE  '" + ogrno + "'  ";

                }
                else if (süre > 80 && süre < 100)
                {
                    int not = 40 / 4;
                    cmd2.CommandText = "UPDATE bilgiler SET KPuan='" + not + "' WHERE Ogrenci_NO LIKE  '" + ogrno + "'  ";

                }
                else if (süre > 100)
                {

                    cmd2.CommandText = "UPDATE bilgiler SET KPuan='" + 0 + "' WHERE Ogrenci_NO LIKE  '" + ogrno + "'  ";

                }
                cmd2.ExecuteNonQuery();

                SqlCommand cmd3 = new SqlCommand();
                cmd2.Connection = baglanti;
                int ogrno2 = Convert.ToInt32(textBox2.Text);
                cmd2.CommandText = "SELECT Mail FROM resimli WHERE numara LIKE  '" + ogrno + "'  ";

                cmd2.ExecuteNonQuery();
                string mail = cmd2.ExecuteScalar().ToString();
                string konu = "Koşu Sınav Sonucunuz :";

                SqlCommand cmd4 = new SqlCommand();
                cmd2.Connection = baglanti;
                int ogrno4 = Convert.ToInt32(textBox2.Text);
                cmd2.CommandText = "SELECT KPuan FROM bilgiler WHERE Ogrenci_No LIKE  '" + ogrno + "'  ";

                cmd2.ExecuteNonQuery();
                string icerik = cmd2.ExecuteScalar().ToString();

               // Gonder(konu, icerik, mail);

                baglanti.Close();

            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = baglanti;
                int ogrno = Convert.ToInt32(textBox15.Text);
                cmd.CommandText = "SELECT Ad FROM bilgiler WHERE Ogrenci_NO = '" + ogrno + "' ";
                string adi = cmd.ExecuteScalar().ToString();
                textBox14.Text = adi;
                cmd.CommandText = "SELECT Soyad FROM bilgiler WHERE Ogrenci_NO = '" + ogrno + "' ";
                string soyadi = cmd.ExecuteScalar().ToString();
                textBox13.Text = soyadi;
                cmd.CommandText = "SELECT Boy FROM bilgiler WHERE Ogrenci_NO = '" + ogrno + "' ";
                string boy = cmd.ExecuteScalar().ToString();
                textBox12.Text = boy;
                cmd.CommandText = "SELECT Kilo FROM bilgiler WHERE Ogrenci_NO = '" + ogrno + "' ";
                string kilo = cmd.ExecuteScalar().ToString();
                textBox11.Text = kilo;
                cmd.CommandText = "SELECT Cinsiyet FROM bilgiler WHERE Ogrenci_NO = '" + ogrno + "' ";
                string cinsiyet = cmd.ExecuteScalar().ToString();
                textBox10.Text = cinsiyet;
                baglanti.Close();



            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
                SqlCommand cmd2 = new SqlCommand();
                cmd2.Connection = baglanti;
                int ogrno = Convert.ToInt32(textBox15.Text);
                cmd2.CommandText = "UPDATE bilgiler SET Sure='" + Convert.ToInt32(textBox16.Text) + "' WHERE Ogrenci_NO LIKE  '" + ogrno + "'  ";

                cmd2.ExecuteNonQuery();
                int süre = Convert.ToInt32(textBox1.Text);
                süre = süre / 1000;
                if (süre < 20)
                {
                    int not = 100 ;
                    cmd2.CommandText = "UPDATE bilgiler SET KPuan='" + not + "' WHERE Ogrenci_NO LIKE  '" + ogrno + "'  ";

                }
                else if (süre > 20 && süre < 50)
                {
                    int not = 85;
                    cmd2.CommandText = "UPDATE bilgiler SET KPuan='" + not + "' WHERE Ogrenci_NO LIKE  '" + ogrno + "'  ";

                }
                else if (süre > 50 && süre < 70)
                {
                    int not = 70;
                    cmd2.CommandText = "UPDATE bilgiler SET KPuan='" + not + "' WHERE Ogrenci_NO LIKE  '" + ogrno + "'  ";

                }
                else if (süre > 70 && süre < 80)
                {
                    int not = 55;
                    cmd2.CommandText = "UPDATE bilgiler SET KPuan='" + not + "' WHERE Ogrenci_NO LIKE  '" + ogrno + "'  ";

                }
                else if (süre > 80 && süre < 100)
                {
                    int not = 40;
                    cmd2.CommandText = "UPDATE bilgiler SET KPuan='" + not + "' WHERE Ogrenci_NO LIKE  '" + ogrno + "'  ";

                }
                else if (süre > 100)
                {

                    cmd2.CommandText = "UPDATE bilgiler SET KPuan='" + 0 + "' WHERE Ogrenci_NO LIKE  '" + ogrno + "'  ";

                }
                cmd2.ExecuteNonQuery();

                SqlCommand cmd3 = new SqlCommand();
                cmd2.Connection = baglanti;
                int ogrno2 = Convert.ToInt32(textBox2.Text);
                cmd2.CommandText = "SELECT Mail FROM resimli WHERE numara LIKE  '" + ogrno + "'  ";

                cmd2.ExecuteNonQuery();
                string mail = cmd2.ExecuteScalar().ToString();
                string konu = "Koşu Sınav Sonucunuz :";

                SqlCommand cmd4 = new SqlCommand();
                cmd2.Connection = baglanti;
                int ogrno4 = Convert.ToInt32(textBox2.Text);
                cmd2.CommandText = "SELECT KPuan FROM bilgiler WHERE Ogrenci_No LIKE  '" + ogrno + "'  ";

                cmd2.ExecuteNonQuery();
                string icerik = cmd2.ExecuteScalar().ToString();

                //Gonder(konu, icerik, mail);
                baglanti.Close();

            }
            else
            {
                SqlCommand cmd2 = new SqlCommand();
                cmd2.Connection = baglanti;
                int ogrno = Convert.ToInt32(textBox15.Text);
                cmd2.CommandText = "UPDATE bilgiler SET Sure='" + Convert.ToInt32(textBox16.Text) + "' WHERE Ogrenci_NO LIKE  '" + ogrno + "'  ";
                cmd2.ExecuteNonQuery();
                int süre = Convert.ToInt32(textBox1.Text);
                süre = süre / 1000;
                if (süre < 20)
                {
                    int not = 100;
                    cmd2.CommandText = "UPDATE bilgiler SET KPuan='" + not + "' WHERE Ogrenci_NO LIKE  '" + ogrno + "'  ";

                }
                else if (süre > 20 && süre < 50)
                {
                    int not = 85;
                    cmd2.CommandText = "UPDATE bilgiler SET KPuan='" + not + "' WHERE Ogrenci_NO LIKE  '" + ogrno + "'  ";

                }
                else if (süre > 50 && süre < 70)
                {
                    int not = 70;
                    cmd2.CommandText = "UPDATE bilgiler SET KPuan='" + not + "' WHERE Ogrenci_NO LIKE  '" + ogrno + "'  ";

                }
                else if (süre > 70 && süre < 80)
                {
                    int not = 55;
                    cmd2.CommandText = "UPDATE bilgiler SET KPuan='" + not + "' WHERE Ogrenci_NO LIKE  '" + ogrno + "'  ";

                }
                else if (süre > 80 && süre < 100)
                {
                    int not = 40;
                    cmd2.CommandText = "UPDATE bilgiler SET KPuan='" + not + "' WHERE Ogrenci_NO LIKE  '" + ogrno + "'  ";

                }
                else if (süre > 100)
                {

                    cmd2.CommandText = "UPDATE bilgiler SET KPuan='" + 0 + "' WHERE Ogrenci_NO LIKE  '" + ogrno + "'  ";

                }
                cmd2.ExecuteNonQuery();
                SqlCommand cmd3 = new SqlCommand();
                cmd2.Connection = baglanti;
                int ogrno2 = Convert.ToInt32(textBox2.Text);
                cmd2.CommandText = "SELECT Mail FROM resimli WHERE numara LIKE  '" + ogrno + "'  ";

                cmd2.ExecuteNonQuery();
                string mail = cmd2.ExecuteScalar().ToString();
                string konu = "Koşu Sınav Sonucunuz :";

                SqlCommand cmd4 = new SqlCommand();
                cmd2.Connection = baglanti;
                int ogrno4 = Convert.ToInt32(textBox2.Text);
                cmd2.CommandText = "SELECT KPuan FROM bilgiler WHERE Ogrenci_No LIKE  '" + ogrno + "'  ";

                cmd2.ExecuteNonQuery();
                string icerik = cmd2.ExecuteScalar().ToString();

               // Gonder(konu, icerik, mail);

                baglanti.Close();

            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
