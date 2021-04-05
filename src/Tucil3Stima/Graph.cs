using System;
using System.Collections.Generic;
using System.IO;

namespace Tucil3Stima
{
	public class Graph
	{
		private int nbSimpul;
		private List<Simpul> listSimpul;
		private bool[,] matAdj;

		public Graph(int n, List<Simpul> list, bool[,] mat)
		{
			this.nbSimpul = n;
			this.listSimpul = list;
			this.matAdj = mat;
		}

		public Graph(int n, List<Simpul> list, List<Tuple<string, string>> nodeBersisian)
		{
			this.nbSimpul = n;
			this.listSimpul = list;

			bool[,] mat = new bool[n, n];

			for (int i=0; i<nodeBersisian.Count; i++)
            {
				Simpul simpul1 = getSimpulFromName(list, nodeBersisian[i].Item1);
				int idx1 = list.IndexOf(simpul1);

				Simpul simpul2 = getSimpulFromName(list, nodeBersisian[i].Item2);
				int idx2 = list.IndexOf(simpul2);

				matAdj[idx1, idx2] = true;
				matAdj[idx2, idx1] = true;
			}

			this.matAdj = mat;
		}

		public int getNbElmt() { return this.nbSimpul; }

		public Simpul getSimpulAtIdx(int idx) { return this.listSimpul[idx]; }

		public int getIdxOfSimpul(Simpul s) { return listSimpul.IndexOf(s); }

		public List<Simpul> getAllAdjSimpul(Simpul s)
        {
			List<Simpul> list = new List<Simpul>();

			for (int i=0; i<getNbElmt(); i++)
            {
				if (matAdj[getIdxOfSimpul(s), i])
                {
					list.Add(getSimpulAtIdx(i));
                }
            }

			return list;
        }
		
		public Simpul getSimpulFromName(List<Simpul> list, string nama)
        {
			for (int i=0; i<list.Count; i++)
            {
				if (list[i].getNama().Equals(nama))
				{
					return list[i];
				}
            }
        }

		public static void insertElement(List<Element> list, Element e, Simpul tujuan)
		{
			if (list.Count == 0)
			{
				list.Add(e);
			}

			else
			{
				for (int i = 0; i < list.Count; i++)
				{
					if (e.countFn(tujuan) < list[i].countFn(tujuan))
					{
						list.Insert(i, e);
						break;
					}
				}
			}
		}
/*		public Simpul getAStar(Simpul asal, Simpul tujuan)
        {

        }*/
	}
}
