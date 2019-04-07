using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ÇİZGELER
{
    class vertexEdge
    { //eklenen kenarların bilgileri tutulur.
        public int v1;
        public int v2;
        public int e;

        public vertexEdge(int v1, int v2, int e)
        {
            this.v1 = v1;
            this.v2 = v2;
            this.e = e;
        }
    }
}
