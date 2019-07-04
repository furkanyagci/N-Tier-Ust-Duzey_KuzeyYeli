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
    public partial class UrunlerForm : Form
    {
        public UrunlerForm()
        {
            InitializeComponent();
        }
        UrunlerORM orm = new UrunlerORM();
        private void UrunlerForm_Load(object sender, EventArgs e)
        {

            dataGridView1.DataSource = orm.Listele();
            TedarikcilerORM tOrm = new TedarikcilerORM();
            cmbTedarikci.DataSource = tOrm.Listele();
            cmbTedarikci.DisplayMember = "SirketAdi";
            cmbTedarikci.ValueMember = "TedarikciID";

            KategoriORM kOrm = new KategoriORM();
            cmbKategoriler.DataSource = kOrm.Listele();
            cmbKategoriler.DisplayMember = "KategoriAdi";
            cmbKategoriler.ValueMember = "KategoriID";

            timer1.Interval = 2000;
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            Urunler u = new Urunler();
            u.UrunAdi = txtUrunAdi.Text;
            u.Fiyat = nudFiyat.Value;
            u.Stok = Convert.ToInt16(nudStok.Value);
            u.KategoriID = (int)cmbKategoriler.SelectedValue;
            u.TedarikciID = (int)cmbTedarikci.SelectedValue;
            u.BirimdekiMiktar = "";

            bool sonuc = orm.Ekle(u);
            EkleSilSonucu(sonuc, txtUrunAdi.Text,"eklendi");
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            bool sonuc = orm.Sil(Convert.ToInt32((dataGridView1.CurrentRow.Cells[0].Value)));
            EkleSilSonucu(sonuc, dataGridView1.CurrentRow.Cells[1].Value.ToString(),"silindi");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblMesaj.Visible = false;
            timer1.Stop();
        }

        void EkleSilSonucu(bool sonuc, string secilen, string islem)
        {
            if (sonuc)
            {
                lblMesaj.Visible = true;
                lblMesaj.Text = string.Format("{0} isimli ürün {1}", secilen, islem);
                lblMesaj.BackColor = Color.Green;
                timer1.Start();
                dataGridView1.DataSource = orm.Listele();
            }
            else
            {
                lblMesaj.Visible = true;
                lblMesaj.Text = secilen + " İşlem gerçekleşmedi!!!";
                lblMesaj.BackColor = Color.Red;
                timer1.Start();
            }
        }
    }
}
