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
    public partial class Form3 : Form
    {


        public Form3()
        {
            InitializeComponent();
        }
        public bool Gonder(string konu, string icerik, string eposta)
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
        SqlConnection con;
        SqlDataAdapter da;
        DataSet ds;
        SqlCommandBuilder cmdb;

        private void Form3_Load(object sender, EventArgs e)
        {
            //SqlConnection baglanti = new SqlConnection("Data Source=BUGRAA;Initial Catalog=Besyo;Integrated Security=True");
            con = new SqlConnection("Data Source=BUGRAA;Initial Catalog=Besyo;Integrated Security=True");
            con.Open();
            da = new SqlDataAdapter("Select * from bilgiler", con);
            ds = new DataSet();
            da.Fill(ds, "bilgiler");
            dataGridView1.DataSource = ds.Tables[0];
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int kayitsayisi;
            kayitsayisi = dataGridView1.RowCount;
            cmdb = new SqlCommandBuilder(da);
            da.Update(ds, "bilgiler");
            MessageBox.Show("Kayıt güncellendi");
            SqlCommand cmd2 = new SqlCommand();
            for (int i = 0; i < kayitsayisi; i++)
            {

                

                int sinav = Convert.ToInt32(dataGridView1.Rows[i].Cells[7].Value);
                int sozlu = Convert.ToInt32(dataGridView1.Rows[i].Cells[9].Value);
                int yazili = Convert.ToInt32(dataGridView1.Rows[i].Cells[10].Value);
                int yuksek = Convert.ToInt32(dataGridView1.Rows[i].Cells[8].Value);
                int donem_notu = (sinav / 4) + (sozlu / 4) + (yazili / 4) + (yuksek/4);

                dataGridView1.Rows[i].Cells[12].Value = donem_notu.ToString();
                da.Update(ds, "bilgiler");


                SqlCommand cmd3 = new SqlCommand();
                cmd2.Connection = con;
                int ogrno2 = Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value);
                cmd2.CommandText = "SELECT Mail FROM resimli WHERE numara LIKE  '" + ogrno2 + "'  ";

                cmd2.ExecuteNonQuery();
                string mail = cmd2.ExecuteScalar().ToString();
                string konu = "Sozlu Sınav Sonucunuz :";

                SqlCommand cmd4 = new SqlCommand();
                cmd2.Connection = con;
                int ogrno4 = Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value);
                cmd2.CommandText = "SELECT Sozlu FROM bilgiler WHERE Ogrenci_No LIKE  '" + ogrno4 + "'  ";

                cmd2.ExecuteNonQuery();
                string icerik = cmd2.ExecuteScalar().ToString();

                Gonder(konu, icerik, mail);

                SqlCommand cmd33 = new SqlCommand();
                cmd2.Connection = con;
                int ogrno23 = Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value);
                cmd2.CommandText = "SELECT Mail FROM resimli WHERE numara LIKE  '" + ogrno2 + "'  ";

                cmd2.ExecuteNonQuery();
                string mail3 = cmd2.ExecuteScalar().ToString();
                string konu3 = "Yazılı Sınav Sonucunuz :";

                SqlCommand cmd43 = new SqlCommand();
                cmd2.Connection = con;
                int ogrno43 = Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value);
                cmd2.CommandText = "SELECT Yazili FROM bilgiler WHERE Ogrenci_No LIKE  '" + ogrno43 + "'  ";

                cmd2.ExecuteNonQuery();
                string icerik3 = cmd2.ExecuteScalar().ToString();

                Gonder(konu3, icerik3, mail3);

            }
        }
    }
}
