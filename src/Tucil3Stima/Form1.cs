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
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //inisialisasi
            Microsoft.Msagl.Drawing.Graph graph = new Microsoft.Msagl.Drawing.Graph("graph");

            //create the graph content
            //nikin grafnya  //masih kode tubes 2 stima
            for (int qwe = 0; qwe < ukurangraf; qwe++)
            {
                graph.AddEdge(graf[qwe].Item1, graf[qwe].Item2);
            }

            foreach (var edge in graph.Edges)
            {
                edge.Attr.ArrowheadAtTarget = Microsoft.Msagl.Drawing.ArrowStyle.None;
            }


            //cari elemen unik dari soal
            List<string> distinctelement = new List<string>();
            int banyakelemenunik = 0;
            for (int i = 1; i <= ukurangraf * 2; i++)
            {
                bool isDuplicate = false;
                for (int j = 0; j < i; j++)
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
            //bikin graf bentuknya bulet di simpul 
            for (int ter = 0; ter < banyakelemenunik; ter++)
            {
                Microsoft.Msagl.Drawing.Node nodeehe = graph.FindNode(distinctelement[ter]);
                nodeehe.Attr.Shape = Microsoft.Msagl.Drawing.Shape.Circle;
            }

            //tampilin graf di formnya
            gViewer1.Graph = graph;
        }
    }
}
