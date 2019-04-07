using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ÇİZGELER
{
    public partial class Form1 : Form
    {
        //sosyal ağ için bir graphı oluşturduk.
        //kullanıcı tarafından oluşturulacak graph için ikigraphı oluşturduk.
        public Graph birGraph;
        public Graph ikiGraph;
        bool olustumu = false;
        int size;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //sosyal ağ çizge yapısının bilgilerini girerek oluşturduk.
            birGraph = new Graph(6);
            birGraph.ekle(0, 1, 5);
            birGraph.ekle(0, 5, 8);
            birGraph.ekle(0, 4, 12);
            birGraph.ekle(1, 5, 6);
            birGraph.ekle(1, 2, 10);
            birGraph.ekle(2, 3, 15);
            birGraph.ekle(2, 5, 16);
            birGraph.ekle(3, 4, 9);
            birGraph.ekle(3, 5, 4);
            birGraph.ekle(4, 5, 7);

        }

        private void button2_Click(object sender, EventArgs e)
        {   
            int a, b, uzaklık;
            //textboxın boş geçilmeme kontrolünü yaptık.
            if(textBox2.Text =="" || textBox3.Text =="")
            {
                MessageBox.Show("Boş Geçilemez..!");
            }
            else
            {
                a = Convert.ToInt32(textBox2.Text);
                b = Convert.ToInt32(textBox3.Text);
                //graph oluştuysa dijktras fonksiyonu ile girilen iki köşe arasındaki en kısa yolu ekrana yazdırdık.
                if (olustumu==true && Convert.ToInt32(textBox2.Text) <= ikiGraph.size && Convert.ToInt32(textBox3.Text) <= ikiGraph.size)
                {
                    uzaklık = ikiGraph.Dijktras(a - 1, b - 1);
                    if (uzaklık != 1000)
                        MessageBox.Show("Köşe çifti arasındaki en kısa uzaklık: " + uzaklık);
                    else
                        MessageBox.Show("Köşe çifti arasında bir yol yok!! ");
                }
                else
                    MessageBox.Show("Girilen Köşeler Bulunamadı..!");

                textBox3.Clear();
                textBox2.Clear();

            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            int x;
            ArrayList isimSırası = new ArrayList();

            listBox1.Items.Clear();
            //graph oluştuysa genişlik öncelikli arama algoritmasıyla tüm düğümleri gezindi ve listeletildi.
            if( textBox1.Text !="" && olustumu==true && Convert.ToInt32(textBox1.Text) <= ikiGraph.size)
            {
                x = Convert.ToInt32(textBox1.Text) - 1;
                isimSırası = ikiGraph.BFS(x);
                listBox1.Visible = true;

                foreach (int i in isimSırası)
                    listBox1.Items.Add(i + 1);
            }
            else
            MessageBox.Show("Girilen Köşe Bulunamadı veya Boş Giriş Yapıldı..!");
            textBox1.Clear();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //graph oluştuysa kruskal algoritmasıyla oluşan ağacı form3e gönderdik.
            if(olustumu==true)
            {
                ikiGraph.MST();
                groupBox1.Visible = false;
                groupBox2.Visible = false;
                groupBox3.Visible = false;
                groupBox6.Visible = false;
                Form3 frm3 = new Form3(ikiGraph);
                frm3.Show();
            }
            else
                MessageBox.Show("Önce Çizge Yapısını Oluşturmalısınız..!");


        }

        private void button5_Click(object sender, EventArgs e)
        {
            //Sosyal ağ çizge yapısını çizdirmek için form2 ye gönderdik.
            groupBox1.Visible = false;
            groupBox2.Visible = false;
            groupBox3.Visible = false;
            groupBox6.Visible = false;
            Form2 frm2 = new Form2(birGraph);
            frm2.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //dijkstra işlemlerini açarak diğerlerini kapama işlemi yapılmıştır.
            groupBox1.Visible = true;
            groupBox2.Visible = false;
            groupBox3.Visible = false;
            groupBox6.Visible = false;

        }

        private void button7_Click(object sender, EventArgs e)
        {
            //genişlik öncelikli arama işlemlerini açarak diğerlerini kapama işlemi yapılmıştır.
            groupBox1.Visible = false;
            groupBox2.Visible = true;
            groupBox3.Visible = false;
            groupBox6.Visible = false;

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //graph oluştuysa prim algoritmasını kullanarak çizdirmek üzere form4e gönderdik.
            if(olustumu==true)
            {
                groupBox1.Visible = false;
                groupBox2.Visible = false;
                groupBox3.Visible = false;
                groupBox6.Visible = false;
                ikiGraph.Prim(ikiGraph.matris, ikiGraph.size);
                Form4 frm4 = new Form4(ikiGraph);
                frm4.Show();
            }
            else
                MessageBox.Show("Önce Çizge Yapısını Oluşturmalısınız..!");

        }

        private void button10_Click(object sender, EventArgs e)
        {
            //yeni çizge oluşturma işlemlerini açarak diğerlerini kapama işlemi gerçekleştiriyor.
            groupBox1.Visible = false;
            groupBox2.Visible = false;
            groupBox3.Visible = true;
            button9.Enabled = true;
            groupBox4.Visible = false;
            groupBox6.Visible = false;
            textBox7.Clear();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //yeni çizge oluşturmaya yarar.
            if(textBox7.Text !="")
            {
                size = int.Parse(textBox7.Text);
                ikiGraph = new Graph(size);
                groupBox4.Visible = true;
                button9.Enabled = false;
                olustumu = true;
            }
            else
                MessageBox.Show("Boş Geçilemez..!");

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            //oluşan çizgiye kenar ekleme işlemlerini gerçekleştirdik.
            if(textBox4.Text !="" && textBox5.Text !="" && textBox6.Text != "")
            {
                if (Convert.ToInt32(textBox4.Text) <= ikiGraph.size && Convert.ToInt32(textBox5.Text) <= ikiGraph.size)
                {
                    ikiGraph.ekle(int.Parse(textBox4.Text) - 1, int.Parse(textBox5.Text) - 1, int.Parse(textBox6.Text));
                }
                else
                    MessageBox.Show("Girilen Köşeler Bulunamadı..!");

                textBox4.Clear();
                textBox5.Clear();
                textBox6.Clear();
            }
            else
                MessageBox.Show("Boş Geçilemez..!");

        }

        private void button11_Click(object sender, EventArgs e)
        {
            //bir çizge oluştuysa çizge yapısı çizmek için form5e gönderir.
            if(olustumu==true)
            {
                groupBox1.Visible = false;
                groupBox2.Visible = false;
                groupBox3.Visible = false;
                groupBox6.Visible = false;
                Form5 frm5 = new Form5(ikiGraph);
                frm5.Show();
            }
            else
                MessageBox.Show("Önce Çizge Yapısını Oluşturmalısınız..!");

        }

        private void button12_Click(object sender, EventArgs e)
        {
            //doktora ulaşma işlemlerini gerçekleştirme işlemleri açar diğerlerini kapatır.
            groupBox1.Visible = false;
            groupBox2.Visible = false;
            groupBox3.Visible = false;
            groupBox6.Visible = true;
            comboBox1.SelectedItem = 0;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            //sosyal ağdaki seçilen bir kişiden doktora ulaşmak için yapılacak en az arama sayısını ekrana yazdırır.
            int a, b, uzaklık;
            a = 1;
            b = comboBox1.SelectedIndex + 2;
            uzaklık = birGraph.Dijktras(a - 1, b - 1);
            if (uzaklık != 1000)
                MessageBox.Show("Doktora Ulaşmak için Yapılacak En az Arama Sayısı : " + uzaklık);
            else
                MessageBox.Show("Doktora ulaşılamaz..!");
            
        }
    }
}
