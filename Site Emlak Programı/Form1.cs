using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Site_Emlak_Programı
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;

            if (username == "siteadmin" && password == "123")
            {
                Form2 emlakkayıt = new Form2();
                emlakkayıt.Show();
                this.Hide();
            }
            else if (username != "siteadmin" && password != "123")
            {
                MessageBox.Show("Kullanıcı adı ve şifreyi yanlış girdiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (username != "siteadmin")
            {
                MessageBox.Show("Kullanıcı adını yanlış girdiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (password != "123")
            {
                MessageBox.Show("Şifreyi yanlış girdiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
