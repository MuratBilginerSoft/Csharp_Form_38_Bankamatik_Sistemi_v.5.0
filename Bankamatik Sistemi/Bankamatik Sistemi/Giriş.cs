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
    public partial class Giriş : Form
    {
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

        public void verial()
        {
            sorgu = "Select Adi,Soyadi,Kart_No from Kişiler";
            komut = new OleDbCommand(sorgu,bağlantı);
            kayıt = new OleDbDataAdapter(komut);
            tablo = new DataTable();

            bağlantı.Open();
            kayıt.Fill(tablo);
            bağlantı.Close();
        
        }

        public void yöneticiverial()
        {
            sorgu2 = "Select * from Yönetici";
            komut2 = new OleDbCommand(sorgu2,bağlantı);
            kayıt2 = new OleDbDataAdapter(komut2);
            tablo2 = new DataTable();

            bağlantı.Open();
            kayıt2.Fill(tablo2);
            bağlantı.Close();
           
        
        }

        #endregion

        #region Değişkenler

        string yol="Provider=Microsoft.ACE.OLEDB.12.0;Data Source=İşBankası.accdb";
        string sorgu,sorgu2;
        OleDbConnection bağlantı,bağlantı2;
        OleDbCommand komut,komut2;
        OleDbDataAdapter kayıt,kayıt2;
        DataTable tablo,tablo2;

        static public int i,j;
        static public string kartno;
        static public string ad,ad2,soyad2, soyad;
        string yöneticino;
        int b = 0;
        #endregion


        public Giriş()
        {
            InitializeComponent();
        }

        private void Giriş_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            bağlan();
            yöneticiverial();
            yöneticino = textBox2.Text;

            int c = tablo2.Rows.Count;
            int d = 0;

            for (j = 0; j < c; j++)
            {
                if (yöneticino == tablo2.Rows[j]["Yonetici_no"].ToString())
                {
                    ad2 = tablo2.Rows[j]["Adi"].ToString();
                    soyad2 = tablo2.Rows[j]["Soyadi"].ToString();
                    d++;
                    break;

                }
            }

            if (d != 0)
            {
                Yönetici yöneticifrm = new Yönetici();
                this.Hide();
                yöneticifrm.ShowDialog();
                this.Show();
            }

            else
            {
                MessageBox.Show("Bu kart numarası geçersizdir.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
          
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            kartno = textBox1.Text;
            bağlan();
            verial();

            int a = tablo.Rows.Count;
            b = 0;
            for ( i = 0; i < a; i++)
            {
                if (kartno == tablo.Rows[i]["Kart_No"].ToString())
                {
                    ad = tablo.Rows[i]["Adi"].ToString();
                    soyad = tablo.Rows[i]["Soyadi"].ToString();
                    b++;
                    break;

                }

            }

            if (b != 0)
            {
                Form1 nfrm = new Form1();
                this.Hide();
                nfrm.ShowDialog();
                this.Show();
            }

            else
            {
                MessageBox.Show("Bu kart numarası geçersizdir.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
