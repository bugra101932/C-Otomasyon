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
    public partial class yukseklik : Form
    {
        string gelen = "0";
  


        SqlConnection baglanti = new SqlConnection("Data Source=BUGRAA;Initial Catalog=Besyo;Integrated Security=True");
        public bool Gonder(string konu, string icerik,string eposta)
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
        }
        public yukseklik()
        {
            InitializeComponent();
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
            else {
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
        
        private void button4_Click(object sender, EventArgs e)
        {   
            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
                SqlCommand cmd2 = new SqlCommand();
                cmd2.Connection = baglanti;
                int ogrno = Convert.ToInt32(textBox2.Text);
                int geldi_mesafe = Convert.ToInt32(textBox1.Text);

                cmd2.CommandText = "UPDATE bilgiler SET Yukseklik='" + geldi_mesafe + "' WHERE Ogrenci_NO LIKE  '" + ogrno + "'  ";
                cmd2.ExecuteNonQuery();
                int yukseklık = geldi_mesafe;

                if (yukseklık > 100)
                {
                    int not = 100;
                    cmd2.CommandText = "UPDATE bilgiler SET YPuan ='" + not + "' WHERE Ogrenci_NO LIKE  '" + ogrno + "'  ";

                }
                else if (yukseklık < 100 && yukseklık > 80)
                {
                    int not = 85;
                    cmd2.CommandText = "UPDATE bilgiler SET YPuan='" + not + "' WHERE Ogrenci_NO LIKE  '" + ogrno + "'  ";

                }
                else if (yukseklık < 80 && yukseklık > 60)
                {
                    int not = 70;
                    cmd2.CommandText = "UPDATE bilgiler SET YPuan='" + not + "' WHERE Ogrenci_NO LIKE  '" + ogrno + "'  ";

                }
                else if (yukseklık < 60 && yukseklık > 40)
                {
                    int not = 55;
                    cmd2.CommandText = "UPDATE bilgiler SET YPuan='" + not + "' WHERE Ogrenci_NO LIKE  '" + ogrno + "'  ";

                }
                else if (yukseklık < 40 && yukseklık > 30)
                {
                    int not = 40;
                    cmd2.CommandText = "UPDATE bilgiler SET YPuan='" + not + "' WHERE Ogrenci_NO LIKE  '" + ogrno + "'  ";

                }
                else if (yukseklık < 30)
                {
                    int not = 0;
                    cmd2.CommandText = "UPDATE bilgiler SET YPuan='" + not + "' WHERE Ogrenci_NO LIKE  '" + ogrno + "'  ";

                }
                
                cmd2.ExecuteNonQuery();


                SqlCommand cmd3 = new SqlCommand();
                cmd2.Connection = baglanti;
                int ogrno2 = Convert.ToInt32(textBox2.Text);
                cmd2.CommandText = "SELECT Mail FROM resimli WHERE numara LIKE  '" + ogrno + "'  ";

                cmd2.ExecuteNonQuery();
                string mail = cmd2.ExecuteScalar().ToString();
                string konu = "Yukseklik Sınav Sonucunuz :";

                SqlCommand cmd4 = new SqlCommand();
                cmd2.Connection = baglanti;
                int ogrno4 = Convert.ToInt32(textBox2.Text);
                cmd2.CommandText = "SELECT YPuan FROM bilgiler WHERE Ogrenci_No LIKE  '" + ogrno + "'  ";

                cmd2.ExecuteNonQuery();
                string icerik = cmd2.ExecuteScalar().ToString();
                
                Gonder(konu,icerik,mail);
                baglanti.Close();

            }
            else {
                SqlCommand cmd2 = new SqlCommand();
                cmd2.Connection = baglanti;
                int ogrno = Convert.ToInt32(textBox2.Text);
                int geldi_mesafe = Convert.ToInt32(textBox1.Text);

                cmd2.CommandText = "UPDATE bilgiler SET Yukseklik='" + geldi_mesafe + "' WHERE Ogrenci_NO LIKE  '" + ogrno + "'  ";
                cmd2.ExecuteNonQuery();
                int yukseklık = Convert.ToInt32(textBox1.Text);

                if (yukseklık > 100)
                {
                    int not = 100 / 4;
                    cmd2.CommandText = "UPDATE bilgiler SET YPuan ='" + not + "' WHERE Ogrenci_NO LIKE  '" + ogrno + "'  ";

                }
                else if (yukseklık < 100 && yukseklık > 80)
                {
                    int not = 85 / 4;
                    cmd2.CommandText = "UPDATE bilgiler SET YPuan='" + not + "' WHERE Ogrenci_NO LIKE  '" + ogrno + "'  ";

                }
                else if (yukseklık < 80 && yukseklık > 60)
                {
                    int not = 70 / 4;
                    cmd2.CommandText = "UPDATE bilgiler SET YPuan='" + not + "' WHERE Ogrenci_NO LIKE  '" + ogrno + "'  ";

                }
                else if (yukseklık < 60 && yukseklık > 40)
                {
                    int not = 55 / 4;
                    cmd2.CommandText = "UPDATE bilgiler SET YPuan='" + not + "' WHERE Ogrenci_NO LIKE  '" + ogrno + "'  ";

                }
                else if (yukseklık < 40 && yukseklık > 30)
                {
                    int not = 40 / 4;
                    cmd2.CommandText = "UPDATE bilgiler SET YPuan='" + not + "' WHERE Ogrenci_NO LIKE  '" + ogrno + "'  ";

                }
                else if (yukseklık < 30)
                {
                    int not = 0;
                    cmd2.CommandText = "UPDATE bilgiler SET YPuan='" + not + "' WHERE Ogrenci_NO LIKE  '" + ogrno + "'  ";

                }
                cmd2.ExecuteNonQuery();
                SqlCommand cmd3 = new SqlCommand();
                cmd2.Connection = baglanti;
                int ogrno2 = Convert.ToInt32(textBox2.Text);
                cmd2.CommandText = "SELECT Mail FROM resimli WHERE numara LIKE  '" + ogrno + "'  ";

                cmd2.ExecuteNonQuery();
                string mail = cmd2.ExecuteScalar().ToString();
                string konu = "Yukseklik Sınav Sonucunuz :";

                SqlCommand cmd4 = new SqlCommand();
                cmd2.Connection = baglanti;
                int ogrno4 = Convert.ToInt32(textBox2.Text);
                cmd2.CommandText = "SELECT YPuan FROM bilgiler WHERE Ogrenci_No LIKE  '" + ogrno + "'  ";

                cmd2.ExecuteNonQuery();
                string icerik = cmd2.ExecuteScalar().ToString();

                Gonder(konu, icerik, mail);
                baglanti.Close();

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            //serialPort2.Close();
            serialPort2.PortName = "COM3";
            serialPort2.Open();
            timer1.Enabled = true;

        }

        private void yukseklik_Load(object sender, EventArgs e)
        {
            serialPort2.Close();
        }



        private void timer1_Tick(object sender, EventArgs e)
        {
            gelen = serialPort2.ReadLine();
            
            // textBox8.Text = gelen.ToString();
            
            textBox1.Text = gelen;
             
 
        }
       



        private void button2_Click(object sender, EventArgs e)
        {   
            timer1.Enabled = false;
            serialPort2.Close();
            
        }
    }
}
