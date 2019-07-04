using NTier.UstDuzey.Entity;
using NTier.UstDuzey.ORM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NTier.UstDuzey.UI.WinForm
{
    public partial class KategorilerForm : Form
    {
        public KategorilerForm()
        {
            InitializeComponent();
        }

        KategoriORM orm = new KategoriORM();
        private void KategorilerForm_Load(object sender, EventArgs e)
        {
            //orm.Sil(18);
            dataGridView1.DataSource = orm.Listele();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            Kategoriler k = new Kategoriler();
            k.KategoriAdi = txtAdi.Text;
            k.Tanimi = txtTanimi.Text;
            bool sonuc = orm.Ekle(k);
            if (sonuc)
            {
                MessageBox.Show("Kayıt eklenmiştir");
                dataGridView1.DataSource = orm.Listele();
            }
            else
            {
                MessageBox.Show("hata oluştu");
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            int secilenSatirID = Convert.ToInt32((dataGridView1.CurrentRow.Cells[0].Value));
            orm.Sil(secilenSatirID);
            dataGridView1.DataSource = orm.Listele();
        }
    }
}
