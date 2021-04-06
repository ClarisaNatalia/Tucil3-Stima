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

/*		public Graph(int n, List<Simpul> list, List<Tuple<string, string>> nodeBersisian)
		{
			this.nbSimpul = n;
			this.listSimpul = list;

			bool[,] mat = new bool[n, n];

			for (int i = 0; i < nodeBersisian.Count; i++)
			{
				Simpul simpul1 = getSimpulFromName(list, nodeBersisian[i].Item1);
				int idx1 = list.IndexOf(simpul1);

				Simpul simpul2 = getSimpulFromName(list, nodeBersisian[i].Item2);
				int idx2 = list.IndexOf(simpul2);

				matAdj[idx1, idx2] = true;
				matAdj[idx2, idx1] = true;
			}

			this.matAdj = mat;
		}*/

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
					/*					Console.WriteLine(s.getNama() + " bertetangga dengan " + getSimpulAtIdx(i).getNama() + i);
					*/
					if (!list.Contains(getSimpulAtIdx(i)))
					{
						list.Add(getSimpulAtIdx(i));

						/*						for (int j=0; j<list.Count; j++)
												{
													Console.WriteLine(list[j].getNama());
												}*/
					}
				}
			}
			return list;
		}

		public Simpul getSimpulFromName(List<Simpul> list, string nama)
		{
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i].getNama().Equals(nama))
				{
					return list[i];
				}
			}
			return null;
		}

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

				if (min != i)
				{
					var lowerValue = list[min];
					list[min] = list[i];
					list[i] = lowerValue;
				}
			}
		}

		public Elemen getAStar(Simpul asal, Simpul tujuan, List<Elemen> list)
		{
			if (list.Count == 0) // Sudah tidak ada simpul yang ingin dikunjungi
			{
				/*				Console.WriteLine("List kosong");*/
				return null;
			}
			else if (list[0].getSimpul().Equals(tujuan)) // Sudah ketemu jarak terpendek
			{
				/*				Console.WriteLine("Sampai tujuan");*/
				return list[0];
			}
			else
			{
				List<Simpul> listAdjSimpul = getAllAdjSimpul(list[0].getSimpul());
				/*				Console.WriteLine("Kondisi listSimpul:");
								for (int j = 0; j < listAdjSimpul.Count; j++)
								{
									Console.WriteLine(listAdjSimpul[j].getNama());
								}
								Console.WriteLine("Kondisi list sebelum nambah simpul2 yg diekspan:");
								for (int j = 0; j < list.Count; j++)
								{
									Console.WriteLine(list[j].getSimpul().getNama());
								}
								Console.WriteLine("Jumlah element list: " + list.Count);*/
				for (int i = 0; i < listAdjSimpul.Count; i++)
				{
					List<Simpul> newPath = list[0].getPath();
					Elemen E = new Elemen(newPath, listAdjSimpul[i]);
					/*					Console.WriteLine(E.getPathInString());*/
					/*INSERT ELEMENT*/
					list.Add(E);
					sortList(list, tujuan);

					/*					List<Elemen> newList = new List<Elemen>();*/
					/*insertElemen(list, E, tujuan);*/
					/*					if (list.Count == 0)
										{
											Console.WriteLine("List kosong");
											list.Add(E);
										}

										else
										{
											for (int j = 0; j < list.Count; j++)
											{
												if (E.countFn(tujuan) < list[j].countFn(tujuan))
												{
													Console.Write("Insert " + E.getSimpul().getNama() + " ke list");
													*//*list.Insert(j, E);*
					/*								List<Elemen> remainList = new List<Elemen>();
													remainList.Add(E);
													remainList.AddRange(list.GetRange(j+1))
													list.InsertRange(j, E);*/
					/*break;*//*
				}
				else if (j == list.Count-1)
				{
					list.Add(E);
				}
			}
		}*/
					/*
										Console.WriteLine("Kondisi list setelah ditambah simpul yg diekspan:");
										for (int j = 0; j < list.Count; j++)
										{
											Console.WriteLine(list[j].getSimpul().getNama());
										}*/
				}
				/*				Console.WriteLine("Masuk rekursif");
								Console.WriteLine("Kondisi list yang akan masuk rekursif:");
								for (int j = 0; j < list.Count; j++)
								{
									Console.WriteLine(list[j].getSimpul().getNama());
								}*/


				list.Remove(list[0]);

				if (list[0].getSimpul().Equals(asal))
                {
					return null;
                }

				return getAStar(asal, tujuan, list);
			}
		}
	}
}
