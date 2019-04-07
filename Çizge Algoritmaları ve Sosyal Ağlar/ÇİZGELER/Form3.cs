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
    public partial class Form3 : Form
    {
        Graph ikiGraph;
        public Form3(Graph ikiGraph)
        {
            this.ikiGraph = ikiGraph;
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            //form1den gelen kruskal algoritmasıyla oluşturulan çizge yapısını çizme işlemi gerçekleştirdik.
            float a = 0, b = 0, a2, b2;
            Graphics g = this.CreateGraphics();
            this.Show();
            SolidBrush b1 = new SolidBrush(Color.Black);
            Pen p1 = new Pen(Color.Black, 1);

            for (float i = 0; i < ikiGraph.size; i++)
            {
                a = (float)Math.Cos(i * (360 / ikiGraph.size));
                b = (float)Math.Sin(i * (360 / ikiGraph.size));
                g.DrawString((i + 1).ToString(), new Font("Arial Black", 10), b1, (150 * a + 300), (150 * b + 300));

                g.DrawEllipse(p1, (150 * a + 300), (150 * b + 300), 10, 10);
                for (int j = 0; j < ikiGraph.size; j++)
                {
                    if (ikiGraph.ağaç[(int)i, j] == 1)
                    {
                        a2 = (float)Math.Cos(j * (360 / ikiGraph.size));
                        b2 = (float)Math.Sin(j * (360 / ikiGraph.size));
                        g.DrawLine(p1, (150 * a + 305), (150 * b + 305), (150 * a2 + 305), (150 * b2 + 305));
                    }
                }
            }
        }
    }
}
