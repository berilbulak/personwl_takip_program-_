using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//System.OleDb.Oledb kütüphanesinin tanımlanması
using System.Data.OleDb;
//System.Text.RegularExpression (Regex) kütüphanesinin tanımlanması
using System.Text.RegularExpressions;
//Giriş çıkış işlemleri için kütüphanenin tanımlanması
using System.IO;

namespace personwl_takip_programı
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();

        }
        //Veri tabanı dosya yolu ve provider nesenlerinin belirlenmesi
        OleDbConnection baglantim = new OleDbConnection("Provider=Microsoft.Ace.Oledb.12.0;Data Source=personel.accdb");


        private void kullanicilari_goster()
        {
            try
            {
                baglantim.Open();
                OleDbDataAdapter kullanicilari_listele = new OleDbDataAdapter("select Tcno AS [Tc No],Kullaniciadi AS[Kullanıcı Adı] from kullanicilar Order By Ad ASC ", baglantim);
                DataSet dshafiza = new DataSet();
                kullanicilari_listele.Fill(dshafiza);
                dataGridView1.DataSource = dshafiza.Tables[0];
                baglantim.Close();

            }
            catch (Exception hatamsj)
            {
                MessageBox.Show(hatamsj.Message, "Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                baglantim.Close();


            }

        }

        private void personelleri_goster()
        {
            try
            {
                baglantim.Open();
                OleDbDataAdapter personelleri_listele = new OleDbDataAdapter("select Tcno AS [Tc No],Kullaniciadi AS[Kullanıcı Adı] ,Dogumtarihi AS[Doğum Tarihi],Görevyeri AS[Görev Yeri],Maas AS [Maaş] from personeller Order By Ad ASC ", baglantim);
                DataSet dshafiza = new DataSet();
                personelleri_listele.Fill(dshafiza);
                dataGridView1.DataSource = dshafiza.Tables[0];
                baglantim.Close();

            }
            catch (Exception hatamsj)
            {
                MessageBox.Show(hatamsj.Message, "SKY Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                baglantim.Close();


            }

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            //form2 ayarları
            pictureBox1.Height = 150;
            pictureBox1.Width = 150;
            //fotoğraf koymak için
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

            try
            {
                pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\kullaniciresimler\\" + Form1.Tcno + ".jpg");
            }

            catch
            {
                pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\kullniciresimler\\resimyok.jpg");

            }
            //Kullanıcı işlemleri sekmesi
            this.Text = "Yönetici İşlemleri";
            label12.ForeColor = Color.DarkRed;
            label12.Text = Form1.Ad + " " + Form1.Soyad;
            textBox1.MaxLength = 11;
            textBox4.MaxLength = 8;
            toolTip1.SetToolTip(this.textBox1, "Tc Kimlik No 11 Karakter Olmalı!");
            radioButton1.Checked = true;

            textBox2.CharacterCasing = CharacterCasing.Upper;
            textBox3.CharacterCasing = CharacterCasing.Upper;
            textBox5.MaxLength = 10;
            textBox6.MaxLength = 10;
            progressBar1.Maximum = 100;
            progressBar1.Value = 0;
            kullanicilari_goster();

            //Personel işlemleri sekmesi
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.Width = 100;
            pictureBox2.Height = 100;
            pictureBox2.BorderStyle = BorderStyle.Fixed3D;
            maskedTextBox1.Mask = "00000000000";
            maskedTextBox2.Mask = "LL????????????????????";
            maskedTextBox3.Mask = "LL????????????????????";
            maskedTextBox4.Mask = "0000";
            maskedTextBox4.Text = "0";
            maskedTextBox2.Text.ToUpper();
            maskedTextBox3.Text.ToUpper();

            comboBox1.Items.Add("İlköğretim");
            comboBox1.Items.Add("Ortaöğretim");
            comboBox1.Items.Add("Lise");
            comboBox1.Items.Add("Üniversite");

            comboBox2.Items.Add("Yönetici");
            comboBox2.Items.Add("Memur");
            comboBox2.Items.Add("Şöför");
            comboBox2.Items.Add("İşçiler");

            comboBox3.Items.Add("Arge");
            comboBox3.Items.Add("Bilgi İşlem");
            comboBox3.Items.Add("Myhasebe");
            comboBox3.Items.Add("Üretim");
            comboBox3.Items.Add("Nakliye");

            DateTime zaman = DateTime.Now;
            int yil = int.Parse(zaman.ToString("yyyy"));
            int ay = int.Parse(zaman.ToString("MM"));
            int gun = int.Parse(zaman.ToString("dd"));

            dateTimePicker1.MinDate = new DateTime(1960, 1, 1);
            dateTimePicker1.MaxDate = new DateTime(yil - 18, ay, gun);
            dateTimePicker1.Format = DateTimePickerFormat.Short;
            radioButton3.Checked = true;
            personelleri_goster(); //form yürütüldüğünde personel ve kullanıcıların aynı anda gösterilmesi sağlanıyor.

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length < 11)

                errorProvider1.SetError(textBox1, "Tc Kimlik No 11 Karakterli Olmalı!");
            else
                errorProvider1.Clear();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar >= 48 && (int)e.KeyChar <= 57 || (int)e.KeyChar == 8)
            { e.Handled = false; }
            else
                e.Handled = true;
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) == true || char.IsControl(e.KeyChar) == true || char.IsSeparator(e.KeyChar) == true)
            { e.Handled = false; }

            else
                e.Handled = true;


        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) == true || char.IsControl(e.KeyChar) == true || char.IsSeparator(e.KeyChar) == true)
            { e.Handled = false; }

            else
                e.Handled = true;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (textBox4.Text.Length != 8)
                errorProvider1.SetError(textBox4, "Kullanıcı adı 8 karakter olmalı!");

            else
                errorProvider1.Clear();

        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) == true || char.IsControl(e.KeyChar) == true || char.IsDigit(e.KeyChar) == true)
            { e.Handled = true; }
            else
                e.Handled = true;

        }
        int parola_skoru = 0;
        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            string parola_seviyesi = "";
            int kucuk_harf_skoru = 0, buyuk_harf_skoru = 0, rakam_skoru = 0, sembol_skoru = 0;
            string sifre = textBox5.Text;
            //regex kütüphanesi için sifre string ifadesindeki türkçe ifadeler ingilizceye dönüştürülmeli
            string duzeltilmis_sifre = "";
            duzeltilmis_sifre = sifre;
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('İ', 'i');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('ı', 'i');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('Ç', 'C');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('Ş', 'S');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('ş', 's');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('Ğ', 'G');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('ğ', 'g');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('Ü', 'U');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('ü', 'u');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('Ö', 'O');
            duzeltilmis_sifre = duzeltilmis_sifre.Replace('ö', 'o');

            if (sifre != duzeltilmis_sifre)
            {
                sifre = duzeltilmis_sifre;
                textBox5.Text = sifre;
                MessageBox.Show("Paroladaki türkçe karakterler ingilizceye dönüştürülmüştür.");
            }
            //1 küçük harf 10 puan 2 ve fazlası 20 puan
            int az_karakter_sayisi = sifre.Length - Regex.Replace(sifre, "[a-z]", "").Length;
            kucuk_harf_skoru = Math.Min(2, az_karakter_sayisi) * 10;

            //1 büyük harf 10 puan 2 ve fazlası 20 puan
            int AZ_karakter_sayisi = sifre.Length - Regex.Replace(sifre, "[A-Z]", "").Length;
            buyuk_harf_skoru = Math.Min(2, az_karakter_sayisi) * 10;


            //1 rakam harf 10 puan 2 ve fazlası 20 puan
            int rakam_sayisi = sifre.Length - Regex.Replace(sifre, "[0-9]", "").Length;
            rakam_skoru = Math.Min(2, rakam_sayisi) * 10;

            //1 sembol harf 10 puan 2 ve fazlası 20 puan
            int sembol_sayisi = sifre.Length - az_karakter_sayisi - AZ_karakter_sayisi - rakam_sayisi;
            sembol_skoru = Math.Min(2, sembol_sayisi) * 10;

            parola_skoru = kucuk_harf_skoru + buyuk_harf_skoru + rakam_skoru + sembol_skoru;
            if (sifre.Length == 9)
                parola_skoru += 10;
            else if (sifre.Length == 10)
                parola_skoru += 20;

            if (kucuk_harf_skoru == 0 || buyuk_harf_skoru == 0 || rakam_skoru == 0 || sembol_skoru == 0)
                label22.Text = "Büyük harf,küçük harf,rakam veya sembol mutlaka kullanılmalı!";
            if (kucuk_harf_skoru != 0 && buyuk_harf_skoru != 0 && rakam_skoru != 0 && sembol_skoru != 0)
                label22.Text = "";

            if (parola_skoru < 70)
                parola_seviyesi = "Kabul edilemez";
            else if (parola_skoru == 70 || parola_skoru == 80)
                parola_seviyesi = "Güçlü";
            else if (parola_skoru == 90 || parola_skoru == 100)
                parola_seviyesi = "Çok Güçlü";

            label9.Text = "%" + Convert.ToString(parola_skoru);
            label10.Text = parola_seviyesi;
            progressBar1.Value = parola_skoru;
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if (textBox6.Text == textBox5.Text)
                errorProvider1.SetError(textBox6, "Parola eşleşmiyor");
            else
                errorProvider1.Clear();
        }

        private void topPage1_temizle()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
        }

        private void topPage2_temizle()
        {
            pictureBox2.Image = null;
            maskedTextBox1.Clear();
            maskedTextBox4.Clear();
            comboBox1.SelectedIndex = -1; comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string yetki = "";
            bool kayitkontrol = false;

            baglantim.Open();
            OleDbCommand selectsorgu = new OleDbCommand("select * from kullanicilar where Tcno ='" + textBox1.Text + "'", baglantim);
            OleDbDataReader kayitokuma = selectsorgu.ExecuteReader();
            while (kayitokuma.Read())
            {
                kayitkontrol = true;
                break;
            }
            baglantim.Close();

            if (kayitkontrol == false)
            {
                //Tc no kontrolü 
                if (textBox1.Text.Length < 11 || textBox1.Text == "")
                    label1.ForeColor = Color.Red;
                else
                    label1.ForeColor = Color.Black;

                //Ad kontrolü
                if (textBox2.Text.Length < 2 || textBox2.Text == "")
                    label2.ForeColor = Color.Red;
                else
                    label2.ForeColor = Color.Black;

                //Soyad kontrolü
                if (textBox3.Text.Length < 2 || textBox3.Text == "")
                    label3.ForeColor = Color.Red;
                else
                    label3.ForeColor = Color.Black;

                //Kullanıcı adı kontrolü
                if (textBox4.Text.Length != 8 || textBox4.Text == "")
                    label5.ForeColor = Color.Red;
                else
                    label5.ForeColor = Color.Black;

                //Parola kontrolü
                if (textBox5.Text == "" || parola_skoru < 70)
                    label6.ForeColor = Color.Red;
                else
                    label6.ForeColor = Color.Black;

                //Parola tekrar kontrolü
                if (textBox5.Text == "" || textBox5.Text != textBox6.Text)
                    label7.ForeColor = Color.Red;
                else
                    label7.ForeColor = Color.Black;

                if (textBox1.Text.Length == 11 && textBox1.Text != "" && textBox2.Text != "" && textBox2.Text != "" && textBox2.Text.Length > 1 && textBox3.Text != "" &&
                    textBox3.Text.Length > 1 && textBox4.Text != "" && textBox5.Text != "" && textBox6.Text != "" && textBox5.Text == textBox6.Text && parola_skoru >= 70)
                {
                    if (radioButton1.Checked == true)
                        yetki = "Yönetici";
                    else if (radioButton2.Checked == true)
                        yetki = "Kullanıcı";
                    try
                    {
                        baglantim.Open();
                        OleDbCommand eklekomutu = new OleDbCommand("insert into kullanicilar values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + yetki + "','" + textBox4.Text + "','" + textBox5.Text + "')", baglantim);
                        eklekomutu.ExecuteNonQuery();
                        baglantim.Close();
                        MessageBox.Show("Yeni kullanıcı kayıdı oluşturuldu!", "Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        topPage1_temizle();
                    }
                    catch (Exception hatamsj)
                    {
                        MessageBox.Show(hatamsj.Message);
                        baglantim.Close();
                    }

                }

                else
                {
                    MessageBox.Show("Yazı rengi kırmızı olan alanlrı tekrar gözden geçirin!", "Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }

            else
            {
                MessageBox.Show("Girilen TCNo daha önceden kayıtlıdır!", "Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }
        private void button2_Click(object sender, EventArgs e)
        {
            bool kayit_arama_durumu = false;
            if (textBox1.Text.Length == 11)
            {
                baglantim.Open();
                OleDbCommand selectsorgu = new OleDbCommand("select * from kullanicilar where Tcno='" + textBox1.Text + "'", baglantim);
                OleDbDataReader kayitokuma = selectsorgu.ExecuteReader();
                while (kayitokuma.Read())
                {
                    kayit_arama_durumu = true;
                    textBox2.Text = kayitokuma.GetValue(1).ToString();
                    textBox3.Text = kayitokuma.GetValue(2).ToString();

                    if (kayitokuma.GetValue(3).ToString() == "Yönetici")
                        radioButton1.Checked = true;
                    else
                        radioButton2.Checked = true;
                    textBox4.Text = kayitokuma.GetValue(4).ToString();
                    textBox5.Text = kayitokuma.GetValue(5).ToString();
                    textBox6.Text = kayitokuma.GetValue(5).ToString();
                    break;
                }
                if (kayit_arama_durumu == false)
                {
                    MessageBox.Show("Aranan kayır bulunamadı.", "Personel Takip Uygulaması", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                baglantim.Close();
            }

            else
            {
                MessageBox.Show("11 haneli TCNo girin", "Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                topPage1_temizle();
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string yetki = "";


            //Tc no kontrolü 
            if (textBox1.Text.Length < 11 || textBox1.Text == "")
                label1.ForeColor = Color.Red;
            else
                label1.ForeColor = Color.Black;

            //Ad kontrolü
            if (textBox2.Text.Length < 2 || textBox2.Text == "")
                label2.ForeColor = Color.Red;
            else
                label2.ForeColor = Color.Black;

            //Soyad kontrolü
            if (textBox3.Text.Length < 2 || textBox3.Text == "")
                label3.ForeColor = Color.Red;
            else
                label3.ForeColor = Color.Black;

            //Kullanıcı adı kontrolü
            if (textBox4.Text.Length != 8 || textBox4.Text == "")
                label5.ForeColor = Color.Red;
            else
                label5.ForeColor = Color.Black;

            //Parola kontrolü
            if (textBox5.Text == "" || parola_skoru < 70)
                label6.ForeColor = Color.Red;
            else
                label6.ForeColor = Color.Black;

            //Parola tekrar kontrolü
            if (textBox5.Text == "" || textBox5.Text != textBox6.Text)
                label7.ForeColor = Color.Red;
            else
                label7.ForeColor = Color.Black;

            if (textBox1.Text.Length == 11 && textBox1.Text != "" && textBox2.Text != "" && textBox2.Text != "" && textBox2.Text.Length > 1 && textBox3.Text != "" &&
                textBox3.Text.Length > 1 && textBox4.Text != "" && textBox5.Text != "" && textBox6.Text != "" && textBox5.Text == textBox6.Text && parola_skoru >= 70)
            {
                if (radioButton1.Checked == true)
                    yetki = "Yönetici";
                else if (radioButton2.Checked == true)
                    yetki = "Kullanıcı";
                try
                {
                    baglantim.Open();
                    OleDbCommand guncellekomutu = new OleDbCommand("update kullanicilar set ad='" + textBox2.Text + "',soyad='" + textBox3.Text + "',yetki='" + yetki + "',kullaniciadi= '" + textBox4.Text + "',parola='" + textBox5.Text + "' where Tcno='" + textBox1.Text + "'", baglantim);
                    guncellekomutu.ExecuteNonQuery(); //güncelle komutlarının sonucunu veri tabanına işle
                    baglantim.Close();
                    MessageBox.Show("Kullanıcı bilgileri güncellendi!", "Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    kullanicilari_goster();

                }
                catch (Exception hatamsj)
                {
                    MessageBox.Show(hatamsj.Message, "Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    baglantim.Close();
                }

            }

            else
            {
                MessageBox.Show("Yazı rengi kırmızı olan alanlrı tekrar gözden geçirin!", "Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 11)
            {
                bool kayit_arama_durumu = false;
                baglantim.Open();
                OleDbCommand selectsorgu = new OleDbCommand("select * from kullanicilar where Tcno = '" + textBox1.Text + "'", baglantim);
                OleDbDataReader kayitokuma = selectsorgu.ExecuteReader();
                while (kayitokuma.Read())
                {
                    kayit_arama_durumu = true;
                    OleDbCommand deletesorgu = new OleDbCommand("delete from kullanicilar where Tcno='" + textBox1.Text + "'", baglantim);
                    deletesorgu.ExecuteNonQuery();
                    MessageBox.Show("Kullanıcı kaydı silindi.", "Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    baglantim.Close();
                    kullanicilari_goster();
                    topPage1_temizle();
                    break;
                }
                if (kayit_arama_durumu == false)
                    MessageBox.Show("Silinecek kayıt bulunamadı.", "Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                baglantim.Close();
                topPage1_temizle();

            }
            else
                MessageBox.Show("Lütfen 11 karakterden oluşan TcNo giriniz!", "Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void button5_Click(object sender, EventArgs e)
        {
            topPage1_temizle();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            OpenFileDialog resimsec = new OpenFileDialog(); //Open file dialog nesnesinin özelliklerini barındıran resimsec nesnesi oluşturuldu
            resimsec.Title = "Personel resmi seciniz.";
            resimsec.Filter = "JPG Dosyalar (*.jpg) | *.jpg";
            if (resimsec.ShowDialog() == DialogResult.OK)
            {
                this.pictureBox2.Image = new Bitmap(resimsec.OpenFile());
            }

        }
        private void button8_Click(object sender, EventArgs e)
        {
            string cinsiyet = ""; //cinsiyet alanı boş bırakıldı
            bool kayitkontrol = false;

            baglantim.Open();
            OleDbCommand selectsorgu = new OleDbCommand("select * from personeller where Tcno='" + maskedTextBox1 + "'", baglantim);
            OleDbDataReader kayitokuma = selectsorgu.ExecuteReader();

            while (kayitokuma.Read())
            {
                kayitkontrol = true;
                break;
            }
            baglantim.Close();

            if (kayitkontrol == false)
            {
                if (pictureBox2.Image == null)
                    button6.ForeColor = Color.Red;
                else
                    button6.ForeColor = Color.Black;

                if (maskedTextBox1.MaskCompleted == false)
                    label13.ForeColor = Color.Red;
                else
                    label13.ForeColor = Color.Black;

                if (maskedTextBox2.MaskCompleted == false)
                    label14.ForeColor = Color.Red;
                else
                    label14.ForeColor = Color.Black;

                if (maskedTextBox3.MaskCompleted == false)
                    label15.ForeColor = Color.Red;
                else
                    label15.ForeColor = Color.Black;

                if (comboBox1.Text == "")
                    label17.ForeColor = Color.Red; //label16 cinsiyet seçimine ilişkin olduğu için altandı
                else
                    label17.ForeColor = Color.Black;

                if (comboBox2.Text == "")
                    label19.ForeColor = Color.Red; //label18 doğum tarihi seçimine ilişkin olduğu için altandı
                else
                    label19.ForeColor = Color.Black;

                if (comboBox3.Text == "")
                    label20.ForeColor = Color.Red;
                else
                    label20.ForeColor = Color.Black;

                if (maskedTextBox4.MaskCompleted == false)
                    label21.ForeColor = Color.Red;
                else
                    label21.ForeColor = Color.Black;

                if (int.Parse(maskedTextBox4.Text) < 1000) //dört basamklı sayısal veri zorunlu tutuldu ancak 1000 den de küçük olması sağlandı
                    label21.ForeColor = Color.Red;
                else
                    label21.ForeColor = Color.Black;

                if (pictureBox2.Image != null && maskedTextBox1.MaskCompleted != false && maskedTextBox2.MaskCompleted != false && maskedTextBox3.MaskCompleted != false
                    && comboBox1.Text != "" && comboBox2.Text != "" && comboBox3.Text != "" && maskedTextBox4.MaskCompleted != false)
                {
                    if (radioButton3.Checked == true)
                        cinsiyet = "Bay";
                    else
                        cinsiyet = "Bayan";

                    try
                    {
                        baglantim.Open();
                        OleDbCommand eklekomutu = new OleDbCommand("insert into personeller values('" + maskedTextBox1.Text + "','" + maskedTextBox2.Text + "','" + maskedTextBox3.Text + "','" + cinsiyet + "','" + comboBox1.Text + "','" + dateTimePicker1.Text + "','" + comboBox2.Text + "','" + comboBox3.Text + "','" + maskedTextBox4.Text + "')", baglantim);
                        eklekomutu.ExecuteNonQuery();
                        baglantim.Close();
                        if (!Directory.Exists(Application.StartupPath + "\\personelresimler")) //Application.StartupPath bindeki debug klasörünü temsil eder
                            Directory.CreateDirectory(Application.StartupPath + "\\personelresimler"); //klasör yoksa oluşturulması sağlandı
                        pictureBox2.Image.Save(Application.StartupPath + "\\personelresimler\\" + maskedTextBox1.Text + ".jpg"); //resim farklı isimle kayıtlı olsa bile tc numarası ile kayıt edilmesi sağlandı

                        MessageBox.Show("Yeni personel kaydı oluşturuldu", "Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        personelleri_goster();
                        topPage2_temizle();
                        maskedTextBox4.Text = "0"; //temizledikten sonra 1000 den küçük olmasın dediğimiz için başlangıç değeri olmalı tekrardan 

                    }
                    catch (Exception hatamsj)
                    {
                        MessageBox.Show(hatamsj.Message, "Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        baglantim.Close();
                    }

                }
                else
                    MessageBox.Show("Yazı rengi kırmızı olan alanları gözden geçiriniz.", "Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
                MessageBox.Show("Girilen Tc No daha önceden kayıtlıdır.", "Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);


        }

        private void button7_Click(object sender, EventArgs e)
        {
            bool kayit_arama_durumu = false;
            if (maskedTextBox1.Text.Length == 11)
            {
                baglantim.Open();
                OleDbCommand selectsorgu = new OleDbCommand("select * from personeller where Tcno='" + maskedTextBox1.Text + "'", baglantim);
                OleDbDataReader kayitokuma = selectsorgu.ExecuteReader();
                while (kayitokuma.Read())
                {
                    kayit_arama_durumu = true;

                    try
                    {
                        pictureBox2.Image = Image.FromFile(Application.StartupPath + "\\personelresimler\\" + kayitokuma.GetValue(0).ToString() + ".jpg");
                    }
                    catch
                    {
                        pictureBox2.Image = Image.FromFile(Application.StartupPath + "\\personelresimler\\resimyok.jpg");

                    }
                    maskedTextBox2.Text = kayitokuma.GetValue(1).ToString();
                    maskedTextBox3.Text = kayitokuma.GetValue(1).ToString();
                    if (kayitokuma.GetValue(3).ToString() == "Bay")
                        radioButton3.Checked = true;
                    else
                        radioButton4.Checked = true;
                    comboBox1.Text = kayitokuma.GetValue(4).ToString();
                    dateTimePicker1.Text = kayitokuma.GetValue(5).ToString();
                    comboBox2.Text = kayitokuma.GetValue(6).ToString();
                    comboBox3.Text = kayitokuma.GetValue(7).ToString();
                    maskedTextBox4.Text = kayitokuma.GetValue(8).ToString();
                    break;

                }
                if (kayit_arama_durumu == false)
                    MessageBox.Show("Aranan kayır bulunamadı.", "Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                baglantim.Close();

            }
            else
            {
                MessageBox.Show("11 haneli TcNo giriniz.", "Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void button9_Click(object sender, EventArgs e)
        {
            string cinsiyet = ""; //cinsiyet alanı boş bırakıldı


            if (pictureBox2.Image == null)
                button6.ForeColor = Color.Red;
            else
                button6.ForeColor = Color.Black;

            if (maskedTextBox1.MaskCompleted == false)
                label13.ForeColor = Color.Red;
            else
                label13.ForeColor = Color.Black;

            if (maskedTextBox2.MaskCompleted == false)
                label14.ForeColor = Color.Red;
            else
                label14.ForeColor = Color.Black;

            if (maskedTextBox3.MaskCompleted == false)
                label15.ForeColor = Color.Red;
            else
                label15.ForeColor = Color.Black;

            if (comboBox1.Text == "")
                label17.ForeColor = Color.Red; //label16 cinsiyet seçimine ilişkin olduğu için altandı
            else
                label17.ForeColor = Color.Black;

            if (comboBox2.Text == "")
                label19.ForeColor = Color.Red; //label18 doğum tarihi seçimine ilişkin olduğu için altandı
            else
                label19.ForeColor = Color.Black;

            if (comboBox3.Text == "")
                label20.ForeColor = Color.Red;
            else
                label20.ForeColor = Color.Black;

            if (maskedTextBox4.MaskCompleted == false)
                label21.ForeColor = Color.Red;
            else
                label21.ForeColor = Color.Black;

            if (int.Parse(maskedTextBox4.Text) < 1000) //dört basamklı sayısal veri zorunlu tutuldu ancak 1000 den de küçük olması sağlandı
                label21.ForeColor = Color.Red;
            else
                label21.ForeColor = Color.Black;

            if (pictureBox2.Image != null && maskedTextBox1.MaskCompleted != false && maskedTextBox2.MaskCompleted != false && maskedTextBox3.MaskCompleted != false
                && comboBox1.Text != "" && comboBox2.Text != "" && comboBox3.Text != "" && maskedTextBox4.MaskCompleted != false)
            {
                if (radioButton3.Checked == true)
                    cinsiyet = "Bay";
                else
                    cinsiyet = "Bayan";

                try
                {
                    baglantim.Open();
                    OleDbCommand guncellekomutu = new OleDbCommand("update personeller set Ad='" + maskedTextBox2.Text + "',Soyad='" + maskedTextBox3.Text + "',Cinsiyet='" + cinsiyet + "',Mezuniyet='" + comboBox1.Text + "',Dogumtarihi='" + dateTimePicker1.Text + "',Gorevi='" + comboBox2.Text + "',Gorevyeri='" + comboBox3.Text + "',Maas='" + maskedTextBox4.Text + "' where Tcno='" + maskedTextBox1.Text + "'", baglantim);
                    guncellekomutu.ExecuteNonQuery();
                    baglantim.Close();

                    personelleri_goster();
                    topPage2_temizle();
                    maskedTextBox4.Text = "0"; //temizledikten sonra 1000 den küçük olmasın dediğimiz için başlangıç değeri olmalı tekrardan 


                }
                catch (Exception hatamsj)
                {
                    MessageBox.Show(hatamsj.Message, "Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    baglantim.Close();
                }

            }
           

        }

        private void button10_Click(object sender, EventArgs e)
        {
            if(maskedTextBox1.MaskCompleted ==  true)
            {
                bool kayit_arama_durumu = false;
                baglantim.Open();
                OleDbCommand aramasorgusu = new OleDbCommand("select * from personeller where Tcno='" + maskedTextBox1.Text + "'", baglantim); //girilen TCno nun kayıltı olup olmadığı sorgulandı
                OleDbDataReader kayitokuma = aramasorgusu.ExecuteReader(); //sorgu sonucu gelen kayıtların data reader nesnesine aktarılması sağlandı

                while(kayitokuma.Read())
                {
                    kayit_arama_durumu = true;
                    OleDbCommand deletesorgusu = new OleDbCommand("delete from personeller where Tcno= '" + maskedTextBox1.Text + "'",baglantim);
                    deletesorgusu.ExecuteNonQuery();
                    break;
                }
                if(kayit_arama_durumu == false) 
                    MessageBox.Show("Silinecek kayıt bulunamadı.","Personel Takip Programı",MessageBoxButtons.OK,MessageBoxIcon.Error);
                baglantim.Close();
                personelleri_goster();
                topPage2_temizle();
                maskedTextBox4.Text = "0";
            
            }
            else
            {
                MessageBox.Show("11 karakterden oluşan Tcno giriniz.", "Personel Takip Uygulaması", MessageBoxButtons.OK, MessageBoxIcon.Error);
                topPage2_temizle();
                maskedTextBox4.Text = "0";
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            topPage2_temizle();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
    

