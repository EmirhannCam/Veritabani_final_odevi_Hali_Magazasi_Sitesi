﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Halı_Mağazası_Sitesi.BL;
using Halı_Mağazası_Sitesi.UI;

namespace Halı_Mağazası_Sitesi
{
    public partial class Anaform : Form
    {
        public Anaform()
        {
            InitializeComponent();
        }

        private void Anaform_Load(object sender, EventArgs e)
        {
            DataSet ds1 = BLogic.SatisDetay();
            if (ds1 != null)
                dataGridView1.DataSource = ds1.Tables[0];

            DataSet ds2 = BLogic.OdemeDetay();
            if (ds2 != null)
                dataGridView2.DataSource = ds2.Tables[0];
        }

        Müşteriler mf = new Müşteriler();
        Ürünler uf = new Ürünler();
        private void btnMüşteriler_Click(object sender, EventArgs e)
        {
            mf.ShowDialog();
        }

       

        private void btnÜrünler_Click(object sender, EventArgs e)
        {
            uf.ShowDialog();

        }

        private void btnSatışYap_Click(object sender, EventArgs e)
        {
            frmSatis frm = new frmSatis()
            {
                Text = "Satış Yap",
                Satis = new Satis()
                {
                    ID = Guid.NewGuid()
                }
            };

        tekrar:
            var sonuc = frm.ShowDialog();
            if (sonuc == DialogResult.OK)
            {
                bool b = BLogic.SatisEkle(frm.Satis);

                if (b)
                {
                DataSet ds1 = BLogic.SatisDetay();
                if (ds1 != null)
                    dataGridView1.DataSource = ds1.Tables[0];
                }
                else
                    goto tekrar;

             }
        }

    

        private void btnSatışDuzenle_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dataGridView1.SelectedRows[0];

            frmSatis frm = new frmSatis()
            {
                Text = "Satış Güncelle",
                Güncelleme = true,
                Satis = new Satis()
                {
                    ID = Guid.Parse(row.Cells[0].Value.ToString()),
                    MusteriID = Guid.Parse(row.Cells[1].Value.ToString()),
                    UrunID = Guid.Parse(row.Cells[2].Value.ToString()),
                    Fiyat = double.Parse(row.Cells[7].Value.ToString()),
                    Tarih = DateTime.Parse(row.Cells[8].Value.ToString()),
                },
            };

            var sonuc = frm.ShowDialog();
            if (sonuc == DialogResult.OK)
            {
                bool b = BLogic.SatisGüncelle(frm.Satis);
                if (b)
                {
                    row.Cells[1].Value = frm.Satis.MusteriID;
                    row.Cells[2].Value = frm.Satis.UrunID;
                    row.Cells[7].Value = frm.Satis.Fiyat;
                    row.Cells[8].Value = frm.Satis.Tarih;

                }


            }
        }

        private void btnSatışSil_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dataGridView1.SelectedRows[0];
            var ID = row.Cells[0].Value.ToString();



            var sonuc = MessageBox.Show("Seçili kayıt silinsin mi?", "Silmeyi onayla",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Question);


            if (sonuc == DialogResult.OK)
            {
                bool b = BLogic.SatisSil(ID);

                if (b)
                {
                    DataSet ds = BLogic.SatisDetay();
                    if (ds != null)
                        dataGridView1.DataSource = ds.Tables[0];

                }


            }
        }

        private void btnÖdemeYap_Click(object sender, EventArgs e)
        {
            frmOdeme frm = new frmOdeme()
            {
                Text = "Ödeme Yap",
                Odeme = new Odeme()
                {
                    ID = Guid.NewGuid()
                }
            };

        tekrar:
            var sonuc = frm.ShowDialog();
            if (sonuc == DialogResult.OK)
            {
                bool b = BLogic.OdemeEkle(frm.Odeme);

                if (b)
                {
                    DataSet ds2 = BLogic.OdemeDetay();
                    if (ds2 != null)
                        dataGridView2.DataSource = ds2.Tables[0];
                }
                else
                    goto tekrar;

            }
        }

        private void btnÖdemeDüzenle_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dataGridView2.SelectedRows[0];

            frmOdeme frm = new frmOdeme()
            {
                Text = "Ödeme Güncelle",
                Güncelleme = true,
                Odeme = new Odeme()
                {
                    ID = Guid.Parse(row.Cells[0].Value.ToString()),
                    MusteriID = Guid.Parse(row.Cells[1].Value.ToString()),
                    Tarih = DateTime.Parse(row.Cells[3].Value.ToString()),
                    Tutar = double.Parse(row.Cells[4].Value.ToString()),
                    Tur = row.Cells[5].Value.ToString(),
                    Açıklama = row.Cells[6].Value.ToString(),
                },
            };

            var sonuc = frm.ShowDialog();
            if (sonuc == DialogResult.OK)
            {
                bool b = BLogic.OdemeGüncelle(frm.Odeme);
                if (b)
                {
                    row.Cells[1].Value = frm.Odeme.MusteriID;
                    row.Cells[3].Value = frm.Odeme.Tarih;
                    row.Cells[4].Value = frm.Odeme.Tutar;
                    row.Cells[5].Value = frm.Odeme.Tur;
                    row.Cells[6].Value = frm.Odeme.Açıklama;

                }


            }
        }

        private void btnÖdemeSil_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dataGridView2.SelectedRows[0];
            var ID = row.Cells[0].Value.ToString();



            var sonuc = MessageBox.Show("Seçili kayıt silinsin mi?", "Silmeyi onayla",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Question);


            if (sonuc == DialogResult.OK)
            {
                bool b = BLogic.OdemeSil(ID);

                if (b)
                {
                    DataSet ds = BLogic.OdemeDetay();
                    if (ds != null)
                        dataGridView2.DataSource = ds.Tables[0];

                }


            }
        }
    }
}
