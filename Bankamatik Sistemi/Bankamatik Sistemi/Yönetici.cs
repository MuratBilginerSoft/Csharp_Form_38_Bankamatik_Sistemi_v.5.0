using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Bankamatik_Sistemi
{
    public partial class Yönetici : Form
    {

        #region Değişkenler

        string yol = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=İşBankası.accdb";
        string sorgu, sorgu2;

        OleDbConnection bağlantı, bağlantı2;
        OleDbCommand komut, komut2;
        OleDbDataAdapter kayıt, kayıt2;
        DataTable tablo, tablo2;
        static public string kartno;
        static public string ad, ad2, soyad2, soyad;
        string yöneticino;
        long toplam = 0;
        int beş, on, yirmi, elli, yüz, ikiyüz, beş2, on2, yirmi2, elli2, yüz2, ikiyüz2;

        #endregion

        #region Metodlar

        public void bağlan()
        {
            try
            {
                bağlantı = new OleDbConnection(yol);
            }
            catch (OleDbException ex)
            {

                MessageBox.Show(" " + ex, "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public void paradurumu()
        {
            sorgu = "Select * from Para";
            komut = new OleDbCommand(sorgu,bağlantı);
            kayıt = new OleDbDataAdapter(komut);

            tablo = new DataTable();

            bağlantı.Open();
            kayıt.Fill(tablo);
            bağlantı.Close();

            beş = Convert.ToInt32(tablo.Rows[0]["beşlik"]) * 5;
            on = Convert.ToInt32(tablo.Rows[0]["onluk"]) * 10;
            yirmi = Convert.ToInt32(tablo.Rows[0]["yirmilik"]) * 20;
            elli = Convert.ToInt32(tablo.Rows[0]["ellilik"]) * 50;
            yüz = Convert.ToInt32(tablo.Rows[0]["yüzlük"]) * 100;
            ikiyüz = Convert.ToInt32(tablo.Rows[0]["ikiyüzlük"]) * 200;

            beş2 = Convert.ToInt32(tablo.Rows[0]["beşlik"]) ;
            on2 = Convert.ToInt32(tablo.Rows[0]["onluk"]) ;
            yirmi2 = Convert.ToInt32(tablo.Rows[0]["yirmilik"]) ;
            elli2 = Convert.ToInt32(tablo.Rows[0]["ellilik"]) ;
            yüz2 = Convert.ToInt32(tablo.Rows[0]["yüzlük"]) ;
            ikiyüz2 = Convert.ToInt32(tablo.Rows[0]["ikiyüzlük"]);
            
        }

        public void paraekle()
        {
            int kimlik = 1;
            

            beş2=Convert.ToInt32(maskedTextBox1.Text)+beş2;
            on2=Convert.ToInt32(maskedTextBox2.Text)+on2;
            yirmi2=Convert.ToInt32(maskedTextBox3.Text)+yirmi2;
            elli2=Convert.ToInt32(maskedTextBox4.Text)+elli2;
            yüz2=Convert.ToInt32(maskedTextBox5.Text)+yüz2;
            ikiyüz2=Convert.ToInt32(maskedTextBox6.Text)+ikiyüz2;


            sorgu2 = "update Para set ikiyüzlük='" + ikiyüz2 + "', yüzlük='" + yüz2 + "', ellilik='" + elli2 + "', yirmilik='" + yirmi2 + "', onluk='" + on2 + "', beşlik='" + beş2 + "' where Kimlik=" + kimlik + " ";
            komut2 = new OleDbCommand(sorgu2, bağlantı);
            bağlantı.Open();
            komut2.ExecuteNonQuery();
            bağlantı.Close();
            MessageBox.Show("Başarılı Bir Şekilde İşlem Tamamlandı.","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
        
        }

        public void maskedsıfırla()

        {
            maskedTextBox1.Text = "0";
            maskedTextBox2.Text = "0";
            maskedTextBox3.Text = "0";
            maskedTextBox4.Text = "0";
            maskedTextBox5.Text = "0";
            maskedTextBox6.Text = "0";
          
        
        }

        #endregion


      

        #region Tanımlamalar

        #endregion


        public Yönetici()
        {
            InitializeComponent();
        }

        private void Yönetici_Load(object sender, EventArgs e)
        {
            
        }

        private void tabControl1_Layout(object sender, LayoutEventArgs e)
        {
            bağlan();
            paradurumu();
            label2.Text = tablo.Rows[0]["beşlik"].ToString() + " Adet";
            label3.Text = tablo.Rows[0]["onluk"].ToString() + " Adet";
            label4.Text = tablo.Rows[0]["yirmilik"].ToString() + " Adet";
            label5.Text = tablo.Rows[0]["ellilik"].ToString() + " Adet";
            label6.Text = tablo.Rows[0]["yüzlük"].ToString() + " Adet";
            label7.Text = tablo.Rows[0]["ikiyüzlük"].ToString() + " Adet";

           

            toplam = beş + on + yirmi + elli + yüz + ikiyüz;
            label9.Text = toplam.ToString()+" TL";

        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                if (maskedTextBox1.Text=="0" && maskedTextBox2.Text=="0" && maskedTextBox3.Text=="0" && maskedTextBox4.Text=="0" && maskedTextBox5.Text=="0" && maskedTextBox6.Text=="0")
                {
                    MessageBox.Show("Hiç Bir Değişiklik Yapmadınız Para Adetlerinde","Uyarı",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }

                else
                {
                    bağlan();
                    paradurumu();
                    paraekle();
                    maskedsıfırla();
                }
                 
            }
            catch (Exception)
            {
                MessageBox.Show("Lütfen Değerleri Kontrol Edip Tekrar Giriniz","Hata",MessageBoxButtons.OK,MessageBoxIcon.Error);
                
            }
           



        }
    }
}
