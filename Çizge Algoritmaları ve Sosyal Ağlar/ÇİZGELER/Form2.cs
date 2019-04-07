using System;
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
    public partial class Form2 : Form
    {
        Graph birGraph;
        public Form2(Graph birGraph)
        {
            this.birGraph = birGraph;
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            //form1den gelen sosyal ağ çizge yapısını çizme işlemi gerçekleştirdik.
            string[] isimler = { "Olivia,Doktor (1)", "Celine, Müh (2)", "Winston,Politikacı (3)", "Chloe,Mimar (4)", "John,Subay (5)", "Jack,Prof (6)" };
            float a = 0, b = 0, a2, b2;
            int sayac = 0;
            Graphics g = this.CreateGraphics();
            this.Show();
            SolidBrush b1 = new SolidBrush(Color.Blue);
            Pen p1 = new Pen(Color.Black, 1);

            for (float i = 0; i < 6; i++)
            {
                a = (float)Math.Cos(i * (360 / 8));
                b = (float)Math.Sin(i * (360 / 8));
                g.DrawString(isimler[sayac], new Font("Arial Black", 10), b1, (150 * a + 300), (150 * b + 300));
                g.DrawEllipse(p1, ( 150* a + 300), (150 * b + 300), 10, 10);
                sayac++;
                for (int j = 0; j < 6; j++)
                {
                    if (birGraph.matris[(int)i, j] != 1000)
                    {
                        a2 = (float)Math.Cos(j * (360/8));
                        b2 = (float)Math.Sin(j * (360/8));
                        g.DrawLine(p1, (150* a + 305), (150 * b + 305), (150* a2 + 305), (150* b2 + 305));
                    }
                }
            }
            
        }
    }
}
