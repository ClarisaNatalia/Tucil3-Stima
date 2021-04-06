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

		public int getNbElmt() { return this.nbSimpul; }

		public Simpul getSimpulAtIdx(int idx) { return this.listSimpul[idx]; }

		public int getIdxOfSimpul(Simpul s) { return listSimpul.IndexOf(s); }

		public List<Simpul> getAllAdjSimpul(Simpul s)
		{
			List<Simpul> list = new List<Simpul>();

			for (int i = 0; i < getNbElmt(); i++)
			{
				if (matAdj[getIdxOfSimpul(s), i])
				{
					if (!list.Contains(getSimpulAtIdx(i)))
					{
						list.Add(getSimpulAtIdx(i));
					}
				}
			}
			return list;
		}

		/*Melakukan sort list dari f(n) terkecil sampai terbesar*/
		public void sortList(List<Elemen> list, Simpul tujuan)
		{
			for (var i = 0; i < list.Count; i++)
			{
				var min = i;
				for (var j = i + 1; j < list.Count; j++)
				{
					if (list[min].countFn(tujuan) > list[j].countFn(tujuan))
					{
						min = j;
					}
				}

				if (i != min)
				{
					var valRendah = list[min];
					list[min] = list[i];
					list[i] = valRendah;
				}
			}
		}

		public Elemen getAStar(Simpul asal, Simpul tujuan, List<Elemen> list)
		{
			if (list.Count == 0) // Sudah tidak ada simpul yang ingin dikunjungi
			{
				return null;
			}
			else if (list[0].getSimpul().Equals(tujuan)) // Sudah ketemu jarak terpendek
			{
				return list[0];
			}
			else
			{
				// Mendapatkan semua simpul yang bertetangga
				List<Simpul> listAdjSimpul = getAllAdjSimpul(list[0].getSimpul());

				for (int i = 0; i < listAdjSimpul.Count; i++)
				{
					List<Simpul> newPath = list[0].getPath();
					Elemen E = new Elemen(newPath, listAdjSimpul[i]);

					// Insert simpul yang bertetangga ke list, kemudian disort
					list.Add(E);
					sortList(list, tujuan);
				}

				list.Remove(list[0]);

				// Jika balik lagi ke simpul asal
				if (list[0].getSimpul().Equals(asal))
                {
					list.Remove(list[0]);
                }

				return getAStar(asal, tujuan, list);
			}
		}
	}
}
