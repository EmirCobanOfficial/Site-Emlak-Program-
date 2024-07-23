using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Site_Emlak_Programı
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            textBox1.KeyPress += new KeyPressEventHandler(OnlyNumbers);
            textBox2.KeyPress += new KeyPressEventHandler(OnlyNumbers);
            textBox4.KeyPress += new KeyPressEventHandler(OnlyLetters);
            textBox5.KeyPress += new KeyPressEventHandler(OnlyNumbers);
            textBox7.KeyPress += new KeyPressEventHandler(OnlyNumbers);
            InitializeComboBoxes();
        }

        SqlConnection baglan = new SqlConnection("Data Source = EMIRMONSTER\\SQLEXPRESS;Initial Catalog= city;Integrated Security = True");

        private void InitializeComboBoxes()
        {
            LoadComboBoxData(comboBox1, "ComboBox1Items");
            LoadComboBoxData(comboBox2, "ComboBox2Items");
            LoadComboBoxData(comboBox3, "ComboBox3Items");
            LoadComboBoxData(comboBox4, "ComboBox4Items");
        }

        private void LoadComboBoxData(ComboBox comboBox, string tableName)
        {
            try
            {
                baglan.Open();
                SqlCommand komut = new SqlCommand($"SELECT Item FROM {tableName}", baglan);
                SqlDataReader reader = komut.ExecuteReader();
                comboBox.Items.Clear();
                while (reader.Read())
                {
                    comboBox.Items.Add(reader["Item"].ToString());
                }
                reader.Close();
            }
            finally
            {
                baglan.Close();
            }
        }

        private void verilerigöster()
        {
            listView1.Items.Clear();
            baglan.Open();
            SqlCommand komut = new SqlCommand("Select * From sitebilgi", baglan);
            SqlDataReader oku = komut.ExecuteReader();

            while (oku.Read())
            {
                ListViewItem ekle = new ListViewItem();
                ekle.Text = oku["id"].ToString();
                ekle.SubItems.Add(oku["site"].ToString());
                ekle.SubItems.Add(oku["oda"].ToString());
                ekle.SubItems.Add(oku["metre"].ToString());
                ekle.SubItems.Add(oku["fiyat"].ToString());
                ekle.SubItems.Add(oku["blok"].ToString());
                ekle.SubItems.Add(oku["no"].ToString());
                ekle.SubItems.Add(oku["adsoyad"].ToString());
                ekle.SubItems.Add(oku["telefon"].ToString());
                ekle.SubItems.Add(oku["notlar"].ToString());
                ekle.SubItems.Add(oku["satkira"].ToString());

                listView1.Items.Add(ekle);
            }
            baglan.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Zambak")
            {
                btn_zambak.BackColor = Color.Yellow;
                btn_gül.BackColor = Color.Green;
                btn_papatya.BackColor = Color.Green;
                btn_menekse.BackColor = Color.Green;
            }
            if (comboBox1.Text == "Papatya")
            {
                btn_zambak.BackColor = Color.Green;
                btn_gül.BackColor = Color.Green;
                btn_papatya.BackColor = Color.Yellow;
                btn_menekse.BackColor = Color.Green;
            }
            if (comboBox1.Text == "Gül")
            {
                btn_zambak.BackColor = Color.Green;
                btn_gül.BackColor = Color.Yellow;
                btn_papatya.BackColor = Color.Green;
                btn_menekse.BackColor = Color.Green;
            }
            if (comboBox1.Text == "Menekşe")
            {
                btn_zambak.BackColor = Color.Green;
                btn_gül.BackColor = Color.Green;
                btn_papatya.BackColor = Color.Green;
                btn_menekse.BackColor = Color.Yellow;
            }
        }

        private void btn_görüntüle_Click(object sender, EventArgs e)
        {
            verilerigöster();
        }

        private void btn_kaydet_Click(object sender, EventArgs e)
        {
            if (ValidateInputs())
            {
                baglan.Open();
                SqlCommand komut = new SqlCommand("insert into sitebilgi (site,oda,metre,fiyat,blok,no,adsoyad,telefon,notlar,satkira) values('" + comboBox1.Text.ToString() + "','" + comboBox3.Text.ToString() + "','" + textBox1.Text.ToString() + "','" + textBox2.Text.ToString() + "','" + comboBox4.Text.ToString() + "','" + textBox7.Text.ToString() + "','" + textBox4.Text.ToString() + "','" + textBox5.Text.ToString() + "','" + textBox6.Text.ToString() + "','" + comboBox2.Text.ToString() + "')", baglan);
                komut.ExecuteNonQuery();
                baglan.Close();
                verilerigöster();
            }
            else
            {
                MessageBox.Show("Lütfen tüm alanları doğru şekilde doldurun.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        int id = 0;
        private void btn_sil_Click(object sender, EventArgs e)
        {
            baglan.Open();
            SqlCommand komut = new SqlCommand("Delete from sitebilgi where id = (" + id + ")", baglan);
            komut.ExecuteNonQuery();
            baglan.Close();
            verilerigöster();
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            id = int.Parse(listView1.SelectedItems[0].SubItems[0].Text);

            textBox8.Text = listView1.SelectedItems[0].SubItems[0].Text;
            comboBox1.Text = listView1.SelectedItems[0].SubItems[1].Text;
            comboBox3.Text = listView1.SelectedItems[0].SubItems[2].Text;
            textBox1.Text = listView1.SelectedItems[0].SubItems[3].Text;
            textBox2.Text = listView1.SelectedItems[0].SubItems[4].Text;
            comboBox4.Text = listView1.SelectedItems[0].SubItems[5].Text;
            textBox7.Text = listView1.SelectedItems[0].SubItems[6].Text;
            textBox4.Text = listView1.SelectedItems[0].SubItems[7].Text;
            textBox5.Text = listView1.SelectedItems[0].SubItems[8].Text;
            textBox6.Text = listView1.SelectedItems[0].SubItems[9].Text;
            comboBox2.Text = listView1.SelectedItems[0].SubItems[10].Text;
        }

        private void btn_düzelt_Click(object sender, EventArgs e)
        {
            if (ValidateInputs())
            {
                baglan.Open();
                SqlCommand komut = new SqlCommand("update sitebilgi set id = '" + textBox8.Text.ToString() + "',site='" + comboBox1.Text.ToString() + "',oda='" + comboBox3.Text.ToString() + "',metre='" + textBox1.Text.ToString() + "',fiyat='" + textBox2.Text.ToString() + "',blok='" + comboBox4.Text.ToString() + "',no='" + textBox7.Text.ToString() + "',adsoyad='" + textBox4.Text.ToString() + "',telefon='" + textBox5.Text.ToString() + "',notlar='" + textBox6.Text.ToString() + "',satkira='" + comboBox2.Text.ToString() + "' where id = " + id + "", baglan);
                komut.ExecuteNonQuery();
                baglan.Close();
                verilerigöster();
            }
            else
            {
                MessageBox.Show("Lütfen tüm alanları doğru şekilde doldurun.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(comboBox1.Text) || string.IsNullOrWhiteSpace(comboBox3.Text) || string.IsNullOrWhiteSpace(textBox1.Text) ||
                string.IsNullOrWhiteSpace(textBox2.Text) || string.IsNullOrWhiteSpace(comboBox4.Text) || string.IsNullOrWhiteSpace(textBox7.Text) ||
                string.IsNullOrWhiteSpace(textBox4.Text) || string.IsNullOrWhiteSpace(textBox5.Text) || string.IsNullOrWhiteSpace(textBox6.Text) ||
                string.IsNullOrWhiteSpace(comboBox2.Text))
            {
                return false;
            }
            return true;
        }

        private void OnlyNumbers(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void OnlyLetters(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }


        private List<string> SaveComboBox1Items()
        {
            List<string> comboBox1Items = comboBox1.Items.Cast<string>().ToList();
            return comboBox1Items;
        }


        private List<string> SaveComboBox2Items()
        {
            List<string> comboBox2Items = comboBox2.Items.Cast<string>().ToList();
            return comboBox2Items;
        }


        private List<string> SaveComboBox3Items()
        {
            List<string> comboBox3Items = comboBox3.Items.Cast<string>().ToList();
            return comboBox3Items;
        }
        private List<string> SaveComboBox4Items()
        {
            List<string> comboBox4Items = comboBox4.Items.Cast<string>().ToList();
            return comboBox4Items;
        }
    }
}
