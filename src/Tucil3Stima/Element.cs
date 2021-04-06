using System;
using System.Collections.Generic;
using System.IO;

namespace Tucil3Stima
{
    public class Elemen
    {
        private Simpul simpul;
        private List<Simpul> path;

        public Elemen(Simpul s) // Membuat Elemen dengan path dirinya sendiri saja
        {
            this.simpul = s;
            this.path = new List<Simpul>();
            this.path.Add(s);
        }

        public Elemen(List<Simpul> path, Simpul s) // Membuat Elemen dengan path e ditambah path dirinya sendiri
        {
            this.simpul = s;
            this.path = new List<Simpul>();
            this.path.AddRange(path);
            this.path.Add(s);
        }

        /*Mendapatkan gedung dari Elemen*/
        public Simpul getSimpul() { return this.simpul; }

        /*Mendapatkan path dari Elemen*/
        public List<Simpul> getPath() { return this.path; }

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
        public string getPathInString()
        {
            if (this.path == null)
            {
                return "Tidak ditemukan jalur";
            }
            else
            {
                string temp = "";
                for (int i = 0; i < path.Count; i++)
                {
                    temp += path[i].getNama();

                    if (i != path.Count - 1)
                    {
                        temp += "->";
                    }
                }
                return temp;
            }
        }
    }
}
