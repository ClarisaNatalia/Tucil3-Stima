using System;
using System.Collections.Generic;
using System.IO;

namespace Tucil3Stima
{
    public class Element
    {
        private Simpul simpul;
        private List<Simpul> path;

        public Element(Simpul s) // Membuat Element dengan path dirinya sendiri saja
        {
            this.simpul = s;
            this.path = new List<Simpul>();
            this.path.Add(s);
        }

        public Element(Element e, Simpul s) // Membuat Element dengan path e ditambah path dirinya sendiri
        {
            this.simpul = s;
            this.path = new List<Simpul>();
            this.path.AddRange(e.path);
            this.path.Add(s);
        }

        /*Mendapatkan gedung dari Element*/
        public Simpul getGedung() { return this.simpul; }

        /*Mendapatkan jarak sejauh path*/
        public double getPathDistance()
        {
            // Inisialisasi dist yang akan menyimpan jarak sejauh path
            double dist = 0;

            // Lakukan perulangan untuk menghitung jarak pathnya
            for (int i = 0; i < path.Count - 1; i++)
            {
                dist += path[i].getStraightDistance(path[i + 1]);
            }

            return dist;
        }

        /*Menghitung f(n)*/
        public double countFn(Simpul tujuan)
        {
            /*Menghitung g(n) + h(n)*/
            return getPathDistance() + simpul.getStraightDistance(tujuan);
        }

        /*Print out path*/
        public void printPath()
        {
            for (int i = 0; i < path.Count; i++)
            {
                Console.Write(path[i].getNama());

                if (i != path.Count - 1)
                {
                    Console.Write("->");
                }
                else
                {
                    Console.WriteLine();
                }
            }
        }
    }
}
