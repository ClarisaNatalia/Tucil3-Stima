using System;

namespace Tucil3Stima
{
    public class Simpul
    {
        private string namaSimpul;
        private double coorX;
        private double coorY;

        public Simpul(string nama, double x, double y)
        {
            this.namaSimpul = nama;
            this.coorX = x;
            this.coorY = y;
        }

        public string getNama() { return namaSimpul; }

        public double getX() { return coorX; }

        public double getY() { return coorY; }

        /*Mendapatkan jarak lurus dari this ke G*/
        public double getStraightDistance(Simpul S)
        {
            return Math.Sqrt(Math.Pow(this.getX() - S.getX(), 2) + Math.Pow(this.getY() - S.getY(), 2));
        }
    }
}
