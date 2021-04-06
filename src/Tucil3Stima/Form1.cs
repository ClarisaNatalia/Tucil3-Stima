using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tucil3Stima
{
    public partial class Form1 : Form
    {
        private int nbSimpulForm;
        private List<Simpul> listSimpulForm;
        private bool[,] matAdjForm;
        private string asal;
        private string tujuan;

        public Form1()
        {
            InitializeComponent();
        }

        OpenFileDialog ofd = new OpenFileDialog();
        OpenFileDialog ofd2 = new OpenFileDialog();

        private void button1_Click(object sender, EventArgs e)
        {
            ofd.Filter = "Text Documents|*.txt";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                label1.Text = ofd.FileName;
                string readfile = System.IO.File.ReadAllText(ofd.FileName);
                string[] cobasekiankali = readfile.Split(new[] { "\r\n", "\r", "\n", "," }, StringSplitOptions.None);

                label2.Text = cobasekiankali[13];

                //testing ubah dari string ke double
                //double a = double.Parse(cobasekiankali[9], System.Globalization.CultureInfo.InvariantCulture);
                //double.Parse(cobasekiankali[9], System.Globalization.CultureInfo.InvariantCulture);
                //a = a * 2.00;
                //string b = a.ToString("F2");
                //label2.Text = b; //tes cobasekaiankali udah bisa hasilin array dari file txt
                //dah bisa keluar semua as string 

/*Menampilkan Graph*/
                int banyakgedung = Int16.Parse(cobasekiankali[0]);//insialisasi banyak gedung
                this.nbSimpulForm = banyakgedung;
                //label3.Text = banyakgedung.ToString();

                //int i = 1;
                //double a = double.Parse(cobasekiankali[i = i +2], System.Globalization.CultureInfo.InvariantCulture);
                //a = a * 2.00;
                //string b = a.ToString("F2");
                //label4.Text = b;

                //bagian bikin list nama gedung dan list koordinat gedung
                List<string> listgedung = new List<string>();
                List <Tuple<double,double>> listkoordinatgedung = new List<Tuple<double,double>> ();
                //listgedung[0] = new string("hehe");
                int isekarang = 0;
                //int indexgedung = 0;
                for (int i = 1; i < banyakgedung*3.00; i++)
                {
                    listgedung.Add(cobasekiankali[i]);
                    double koordx = double.Parse(cobasekiankali[i = i + 1], System.Globalization.CultureInfo.InvariantCulture);
                    double koordy = double.Parse(cobasekiankali[i = i + 1], System.Globalization.CultureInfo.InvariantCulture);
                    listkoordinatgedung.Add(Tuple.Create(koordx, koordy));
                    isekarang = i;
                    //indexgedung++;

                }

                //label3.Text = listgedung[0];
                //string b = listkoordinatgedung[0].Item1.ToString("F2");
                string b = isekarang.ToString();
                label3.Text = b;


                //list node yang terhubung, buat bikin grafnya 
                int banyakelemenbacafile = cobasekiankali.Length;
                List<Tuple<string, string>> nodegraf = new List<Tuple<string, string>>();

                for (int mulai = isekarang+1; mulai < banyakelemenbacafile-1; mulai++)
                {
                    nodegraf.Add(Tuple.Create(cobasekiankali[mulai], cobasekiankali[mulai = mulai + 1]));
                }
                //string c = banyakelemenbacafile.ToString();
                //label4.Text = nodegraf[1].Item1;


                //membuat graf
                Microsoft.Msagl.Drawing.Graph graph = new Microsoft.Msagl.Drawing.Graph("graph");

                for (int qwe = 0; qwe < nodegraf.Count; qwe++)
                {
                    graph.AddEdge(nodegraf[qwe].Item1, nodegraf[qwe].Item2);
                }

                foreach (var edge in graph.Edges)
                {
                    edge.Attr.ArrowheadAtTarget = Microsoft.Msagl.Drawing.ArrowStyle.None;
                }

                List<string> distinctelement = new List<string>();
                int banyakelemenunik = 0;
                for (int i = isekarang+1; i < banyakelemenbacafile; i++)
                {
                    bool isDuplicate = false;
                    for (int j = isekarang + 1; j < i; j++)
                    {
                        if (cobasekiankali[i] == cobasekiankali[j])
                        {
                            isDuplicate = true;
                            break;
                        }
                    }

                    if (!isDuplicate)
                    {
                        Console.WriteLine(cobasekiankali[i]);
                        distinctelement.Add(cobasekiankali[i]);
                        banyakelemenunik++;

                    }
                }

                distinctelement.ToArray();

                for (int ter = 0; ter < banyakelemenunik; ter++)
                {
                    Microsoft.Msagl.Drawing.Node nodeehe = graph.FindNode(distinctelement[ter]);
                    nodeehe.Attr.Shape = Microsoft.Msagl.Drawing.Shape.Circle;
                }

                //tampilin graf di formnya
                gViewer1.Graph = graph;

/*MEMBUAT OBJEK GRAPH*/
                /*Membuat listSimpul dari Graph*/
                List<Simpul> listSimpul = new List<Simpul>();

                for (int i=0; i<banyakgedung; i++)
                {
                    Simpul tempSimpul = new Simpul(listgedung[i], listkoordinatgedung[i].Item1, listkoordinatgedung[i].Item2);
                    listSimpul.Add(tempSimpul);
                }

                this.listSimpulForm = listSimpul;

                /*Membuat matriks of adjacency, inisialisasi semua elemennya false*//*
                bool[,] matAdj = new bool[banyakgedung, banyakgedung];
                for (int i=0; i<banyakgedung; i++)
                {
                    for (int j=0; j<banyakgedung; j++)
                    {
                        matAdj[i, j] = false;
                    }
                }

                *//*Membuat objek graph*//*
                Graph G = new Graph(banyakgedung, listSimpul, nodegraf);*/


                //input pilihan ke combo box
                for(int combos = 0; combos<banyakelemenunik; combos++)
                {
                    comboBox1.Items.Add(distinctelement[combos]);
                    comboBox2.Items.Add(distinctelement[combos]);
                }

            }

            

        }
        /*  0 1 1 0 
            0 0 1 1
            0 0 0 1
            0 0 0 0 */

        private void button2_Click(object sender, EventArgs e)
        {
            ofd2.Filter = "Text Documents|*.txt";

            if (ofd2.ShowDialog() == DialogResult.OK)
            {

                //label7.Text = ofd2.FileName;
                string readfile2 = System.IO.File.ReadAllText(ofd2.FileName);
                string[] cobasekiankali2 = readfile2.Split(new[] { "\r\n", "\r", "\n", " "}, StringSplitOptions.None);

                double ukuranmatriksdalamdouble = Math.Sqrt(cobasekiankali2.Length);
                string ukuranmatriksstring = ukuranmatriksdalamdouble.ToString();
                int ukuranmatriksint = Int16.Parse(ukuranmatriksstring);

                //string ukuranmatriksstring = (cobasekiankali2.Length).ToString();
                //label7.Text = ukuranmatriksstring;

                //matriks boolean 
                bool[,] matriksketerhubungan = new bool[ukuranmatriksint,ukuranmatriksint];

                int matriksbacafile = 0;
                for(int r = 0; r<ukuranmatriksint; r++)
                {
                    for(int co = 0; co<ukuranmatriksint; co++)
                    {
                        if(cobasekiankali2[matriksbacafile] == "1")
                        {
                            matriksketerhubungan[r, (matriksbacafile % ukuranmatriksint)] = true;
                        }

                        //if((matriksbacafile % 4 == 0) & (cobasekiankali2[matriksbacafile] == "1"))
                        //{
                        //    matriksketerhubungan[r, 0] = true;
                        //}
                        //else if((matriksbacafile % 4 == 1) & (cobasekiankali2[matriksbacafile] == "1"))
                        //{
                        //    matriksketerhubungan[r, 1] = true;
                        //}
                        //else if ((matriksbacafile % 4 == 2) & (cobasekiankali2[matriksbacafile] == "1"))
                        //{
                        //    matriksketerhubungan[r, 2] = true;
                        //}
                        //else if ((matriksbacafile % 4 == 3) & (cobasekiankali2[matriksbacafile] == "1"))
                        //{
                        //    matriksketerhubungan[r, 3] = true;
                        //}

                        matriksbacafile++;
                    }
                }

                this.matAdjForm = matriksketerhubungan;

                //bool coba = false;
                string tester = matriksketerhubungan[0,1].ToString();

                label7.Text = tester;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.asal = comboBox1.Text;
            this.tujuan = comboBox2.Text;

            /*Mencari simpul dengan nama asal*/
            Simpul simpulAsal = new Simpul("X", 0, 0);
            for (int i = 0; i < this.nbSimpulForm; i++)
            {
                if (this.listSimpulForm[i].getNama().Equals(asal))
                {
                    simpulAsal = listSimpulForm[i];
                    break;
                }
            }

            /*Mencari simpul dengan nama tujuan*/
            Simpul simpulTujuan = new Simpul("X",0,0);
            for (int i=0; i<this.nbSimpulForm; i++)
            {
                if (this.listSimpulForm[i].getNama().Equals(tujuan))
                {
                    simpulTujuan = listSimpulForm[i];
                    break;
                }
            }

            Graph G = new Graph(this.nbSimpulForm, this.listSimpulForm, this.matAdjForm);

            List<Elemen> listElemen = new List<Elemen>();
            listElemen.Add(new Elemen(simpulAsal));

            Elemen E = G.getAStar(simpulAsal, simpulTujuan, listElemen);

            if (E == null)
            {
                label8.Text = "Tidak ada jalur tersedia";
            }
            else
            {
                label8.Text = "Jarak yang ditempuh:" + E.getPathDistance().ToString();
            }
            

            string readfile = System.IO.File.ReadAllText(ofd.FileName);
            string[] cobasekiankali = readfile.Split(new[] { "\r\n", "\r", "\n", "," }, StringSplitOptions.None);

            //testing ubah dari string ke double
            //double a = double.Parse(cobasekiankali[9], System.Globalization.CultureInfo.InvariantCulture);
            //double.Parse(cobasekiankali[9], System.Globalization.CultureInfo.InvariantCulture);
            //a = a * 2.00;
            //string b = a.ToString("F2");
            //label2.Text = b; //tes cobasekaiankali udah bisa hasilin array dari file txt
            //dah bisa keluar semua as string 

            /*Menampilkan Graph*/
            int banyakgedung = Int16.Parse(cobasekiankali[0]);//insialisasi banyak gedung
            this.nbSimpulForm = banyakgedung;
            //label3.Text = banyakgedung.ToString();

            //int i = 1;
            //double a = double.Parse(cobasekiankali[i = i +2], System.Globalization.CultureInfo.InvariantCulture);
            //a = a * 2.00;
            //string b = a.ToString("F2");
            //label4.Text = b;

            //bagian bikin list nama gedung dan list koordinat gedung
            List<string> listgedung = new List<string>();
            List<Tuple<double, double>> listkoordinatgedung = new List<Tuple<double, double>>();
            //listgedung[0] = new string("hehe");
            int isekarang = 0;
            //int indexgedung = 0;
            for (int i = 1; i < banyakgedung * 3.00; i++)
            {
                listgedung.Add(cobasekiankali[i]);
                double koordx = double.Parse(cobasekiankali[i = i + 1], System.Globalization.CultureInfo.InvariantCulture);
                double koordy = double.Parse(cobasekiankali[i = i + 1], System.Globalization.CultureInfo.InvariantCulture);
                listkoordinatgedung.Add(Tuple.Create(koordx, koordy));
                isekarang = i;
                //indexgedung++;

            }

            //label3.Text = listgedung[0];
            //string b = listkoordinatgedung[0].Item1.ToString("F2");
            string b = isekarang.ToString();
            label3.Text = b;


            //list node yang terhubung, buat bikin grafnya 
            int banyakelemenbacafile = cobasekiankali.Length;
            List<Tuple<string, string>> nodegraf = new List<Tuple<string, string>>();

            for (int mulai = isekarang + 1; mulai < banyakelemenbacafile - 1; mulai++)
            {
                nodegraf.Add(Tuple.Create(cobasekiankali[mulai], cobasekiankali[mulai = mulai + 1]));
            }
            //string c = banyakelemenbacafile.ToString();
            //label4.Text = nodegraf[1].Item1;


            //membuat graf
            Microsoft.Msagl.Drawing.Graph graph = new Microsoft.Msagl.Drawing.Graph("graph");

            for (int qwe = 0; qwe < nodegraf.Count; qwe++)
            {
                var edge = graph.AddEdge(nodegraf[qwe].Item1, nodegraf[qwe].Item2);

                if (E != null)
                {
                    for (int i = 0; i < E.getPath().Count - 1; i++)
                    {
                        //* graph.FindNode(E.getPath()[i].getNama(), E.getPath()[i + 1].getNama());
                        /*graph.AddEdge(E.getPath()[i].getNama(), E.getPath()[i + 1].getNama()).Attr.Color = Microsoft.Msagl.Drawing.Color.Coral;*//**/
                        bool conditional1 = (nodegraf[qwe].Item1.Equals(E.getPath()[i].getNama()) && nodegraf[qwe].Item2.Equals(E.getPath()[i + 1].getNama()));
                        bool conditional2 = (nodegraf[qwe].Item2.Equals(E.getPath()[i].getNama()) && nodegraf[qwe].Item1.Equals(E.getPath()[i + 1].getNama()));
                        if (conditional1 || conditional2)
                        {
                            edge.Attr.Color = Microsoft.Msagl.Drawing.Color.ForestGreen;
                        }
                        /*                    else
                                            {
                                                graph.AddEdge(nodegraf[qwe].Item1, nodegraf[qwe].Item2);
                                            }*/

                    }
                }
            }

            foreach (var edge in graph.Edges)
            {
                edge.Attr.ArrowheadAtTarget = Microsoft.Msagl.Drawing.ArrowStyle.None;
            }

            List<string> distinctelement = new List<string>();
            int banyakelemenunik = 0;
            for (int i = isekarang + 1; i < banyakelemenbacafile; i++)
            {
                bool isDuplicate = false;
                for (int j = isekarang + 1; j < i; j++)
                {
                    if (cobasekiankali[i] == cobasekiankali[j])
                    {
                        isDuplicate = true;
                        break;
                    }
                }

                if (!isDuplicate)
                {
                    Console.WriteLine(cobasekiankali[i]);
                    distinctelement.Add(cobasekiankali[i]);
                    banyakelemenunik++;

                }
            }

            distinctelement.ToArray();

            for (int ter = 0; ter < banyakelemenunik; ter++)
            {
                Microsoft.Msagl.Drawing.Node nodeehe = graph.FindNode(distinctelement[ter]);
                nodeehe.Attr.Shape = Microsoft.Msagl.Drawing.Shape.Circle;
            }

            //tampilin graf di formnya
            gViewer1.Graph = graph;

            if (E != null)
            {
                for (int k = 0; k < this.nbSimpulForm; k++)
                {
                    for (int j = 0; j < E.getPath().Count; j++)
                    {
                        if (listSimpulForm[k].getNama().Equals(E.getPath()[j].getNama()))
                        {
                            graph.FindNode(listSimpulForm[k].getNama()).Attr.FillColor = Microsoft.Msagl.Drawing.Color.LimeGreen;
                        }
                    }
                }
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
}
