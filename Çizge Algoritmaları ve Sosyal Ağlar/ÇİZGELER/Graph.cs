using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ÇİZGELER
{
    public class Graph
    {   //çizgeler için gerekli matris ve ağaçlar tanımlandı.
        public int[,] matris;
        public int size;
        public int[,] ağaç;
        public int[,] ağaç2;
        public ArrayList yollar;

        //constructorda gerekli atamalar gerçekleşmiştir.
        public Graph(int size)
        {
            this.size = size;
            yollar = new ArrayList();
            matris = new int[size, size];
            ağaç = new int[size, size];
            ağaç2 = new int[size, size];

            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    matris[i, j] = 1000;
        }
        //grapha bir kenar eklemeye yarar.
        public void ekle(int a, int b, int weight)
        {
            matris[a, b] = weight;
            matris[b, a] = weight;

            vertexEdge yol = new vertexEdge(a, b, weight);
            yollar.Add(yol);
        }
        //iki köşe çifti arasındaki en kısa mesafeyi bulup geri döndürür
        public int Dijktras(int a, int b)
        {
            int[] distances = new int[size];

            Distance(distances, a);

            return distances[b];
        }
        //dijtras fonksiyonuna yardımcı olur.
        public void Distance(int[] D, int a)
        {
            int w, v, min;

            bool[] visited = new bool[size];

            D[a] = 0;
            for (v = 0; v < size; v++)
            {
                if (v != a)
                {
                    visited[v] = false;
                    D[v] = matris[a, v];
                }
            }

            for (int i = 0; i < size; ++i)
            {
                min = 1000;
                for (w = 0; w < size; w++)
                    if (!visited[w])
                        if (D[w] < min)
                        {
                            v = w;
                            min = D[w];
                        }

                visited[v] = true;

                for (w = 0; w < size; w++)
                    if (!visited[w])
                        if (min + matris[v, w] < D[w])
                            D[w] = min + matris[v, w];

            }
        }
        //Seçilen bir köşenin Genişlik öncelikli arama algoritmasıyla sıralanmasını gerçekleştirir.
        public ArrayList BFS(int a)
        {
            Queue kuyruk = new Queue();

            bool[,] visited = new bool[size, size];
            ArrayList isimSırası = new ArrayList();

            kuyruk.Enqueue(a);
            visited[0, a] = true;

            for (int k = 0; k < size; k++)
                for (int i = 0; i < size; i++)
                    if (visited[k, i] && kuyruk.Count != 0)
                    {
                        isimSırası.Add(kuyruk.Dequeue());
                        for (int j = k + 1; j < size; j++)
                            for (int n = 0; n < size; n++)
                                if (!visited[j, n] && matris[i, n] != 1000 && n != a)
                                {
                                    kuyruk.Enqueue(n);
                                    for (int y = 0; y < size; y++)
                                        visited[y, n] = true;
                                }
                    }

            return isimSırası;
        }
        //kruskal algoritmasıyla en küçük kapsayan ağacı bulur.
        public void MST()
        {
            bool[] visited = new bool[size];
            ArrayList liste = new ArrayList();

            listeEkle(0, liste, visited);
            visited[0] = true;
            for (int x = 1; x < size; x++)
            {
                vertexEdge birYol = (vertexEdge)liste[0];
                liste.RemoveAt(0);

                if (!visited[birYol.v2] && visited[birYol.v1])
                {
                    visited[birYol.v2] = true;
                    listeEkle(birYol.v2, liste, visited);
                }
                else if (!visited[birYol.v1] && visited[birYol.v2])
                {
                    visited[birYol.v1] = true;
                    listeEkle(birYol.v1, liste, visited);
                }

                ağaç[birYol.v1, birYol.v2] = 1;
            }
        }
        //mst fonksiyonuna listeye yolların eklenmesiyle yardımcı olur.
        public void listeEkle(int i, ArrayList liste, bool[] visited)
        {
            int a = 0;
            foreach (vertexEdge birYol in yollar)
                if ((birYol.v1 == i || birYol.v2 == i) && (visited[birYol.v1] == false || visited[birYol.v2] == false))
                {
                    if (liste.Count == 0)
                        liste.Add(birYol);
                    else
                    {
                        foreach (vertexEdge yol in liste)
                        {
                            if (birYol.e < yol.e)
                            {
                                liste.Insert(a, birYol);
                                break;
                            }
                            a++;
                        }
                        if (i == liste.Count)
                            liste.Add(birYol);
                    }
                }
        }

        //prim algoritmasıyla en küçük kapsayan ağacı bulur.
        public void Prim(int[,] graph, int köseSayisi)
        {
            int[] ebeveyn = new int[köseSayisi];
            int[] anahtar = new int[köseSayisi];
            bool[] mstSet = new bool[köseSayisi];

            for (int i = 0; i < köseSayisi; ++i)
            {
                anahtar[i] = int.MaxValue;
                mstSet[i] = false;
            }

            anahtar[0] = 0;
            ebeveyn[0] = -1;

            for (int sayi = 0; sayi < köseSayisi - 1; ++sayi)
            {
                int u = MinAnahtar(anahtar, mstSet, köseSayisi);
                mstSet[u] = true;

                for (int v = 0; v < köseSayisi; ++v)
                {
                    if (Convert.ToBoolean(graph[u, v]) && mstSet[v] == false && graph[u, v] < anahtar[v])
                    {
                        ebeveyn[v] = u;
                        anahtar[v] = graph[u, v];
                        
                    }
                }
            }
            AgacaEkle(ebeveyn, köseSayisi);
        }
        //minimum ağırlığın indeksini döndürerek prim metoduna yardımcı olur.
        private  int MinAnahtar(int[] anahtar, bool[] set, int köseSayisi)
        {
            int min = int.MaxValue, minIndex = 0;

            for (int v = 0; v < köseSayisi; ++v)
            {
                if (set[v] == false && anahtar[v] < min)
                {
                    min = anahtar[v];
                    minIndex = v;
                }
            }

            return minIndex;
        }
        //prim metodunda oluşturulan yolların ağaç matrisine eklenmesini gerçekleştirir.
        private void AgacaEkle(int[] ebeveyn, int köseSayisi)
        {
            for (int i = 1; i < köseSayisi; ++i)
                ağaç2[ebeveyn[i],i]= 1;
        }

    }


}
