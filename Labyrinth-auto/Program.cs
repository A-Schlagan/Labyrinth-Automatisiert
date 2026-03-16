using System;
using System.Text;
using System.Collections.Generic;

namespace labyrinth
{
    internal class Program
    {
        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.CursorVisible = false;

            char[,] lab =
            {
            { '#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#' },
            { '#','S','.','.','.','#','.','.','.','.','.','.','#','.','.','.','.','.','.','#' },
            { '#','#','#','#','.','#','.','#','#','#','#','.','#','.','#','#','#','#','.','#' },
            { '#','.','.','.','.','.','.','#','.','.','#','.','.','.','#','.','.','#','.','#' },
            { '#','.','#','#','#','#','.','#','.','#','#','.','#','.','#','.','#','#','.','#' },
            { '#','.','.','.','.','#','.','.','.','#','.','.','#','.','#','.','.','.','.','#' },
            { '#','#','#','#','.','#','#','#','.','#','.','#','#','.','#','#','#','#','.','#' },
            { '#','.','.','#','.','.','.','#','.','#','.','.','.','.','.','.','.','#','.','#' },
            { '#','.','#','#','#','#','.','#','.','#','#','#','#','#','#','#','.','#','.','#' },
            { '#','.','.','.','.','.','.','.','.','.','.','.','.','.','#','.','.','#','.','#' },
            { '#','#','#','#','.','#','#','#','#','#','#','#','#','.','#','Z','#','#','.','#' },
            { '#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#' }
            };

            int zeile = 0;
            int spalte = 0;
            for (int z = 0; z < lab.GetLength(0); z++)
            {
                for (int s = 0; s < lab.GetLength(1); s++)
                {
                    if (lab[z, s] == 'S')
                    {
                        zeile = z;
                        spalte = s;
                    }
                }
            }

            int schritte = 0;
            bool ziel = true;
            List<int> verlaufZeile = new List<int>();
            List<int> verlaufSpalte = new List<int>();

            while (ziel == true)
            {                
                RenderLabyrinth(lab);
                System.Threading.Thread.Sleep(150);

                int newZeile = zeile;
                int newSpalte = spalte;
                

                if (lab[zeile - 1, spalte] == '.' || lab[zeile - 1, spalte] == 'Z')
                {
                    newZeile--; 
                }
                else if (lab[zeile + 1, spalte] == '.' || lab[zeile + 1, spalte] == 'Z')
                {
                    newZeile++;
                }
                else if (lab[zeile,spalte -1] == '.' || lab[zeile, spalte -1] == 'Z')
                {
                    newSpalte--;
                }
                else if (lab[zeile, spalte + 1] == '.' || lab[zeile, spalte+1] == 'Z')
                {
                    newSpalte++;
                }

                if (newZeile !=zeile || newSpalte != spalte)
                {
                    verlaufZeile.Add(zeile);
                    verlaufSpalte.Add(spalte);
                }
                else if (verlaufZeile.Count > 0)
                {
                    newZeile = verlaufZeile[verlaufZeile.Count - 1];
                    newSpalte = verlaufSpalte[verlaufSpalte.Count - 1];

                    verlaufZeile.RemoveAt(verlaufZeile.Count - 1);
                    verlaufSpalte.RemoveAt(verlaufSpalte.Count - 1);
                }

                if (lab[newZeile, newSpalte] != '#')
                {
                    if (lab[newZeile, newSpalte] == 'Z')
                    {

                        lab[zeile, spalte] = '*';
                        zeile = newZeile;
                        spalte = newSpalte;
                        lab[zeile, spalte] = 'S';
                        RenderLabyrinth(lab);
                        Console.WriteLine("🎉GEWONNEN🎉 \nDas Krankenhaus ist errecht!");
                        ziel = false;
                    }
                    if (schritte == 150)
                    {
                        Console.WriteLine("💀VERLOREN💀 - Keine Schritte mehr übrig!");
                        ziel = false;
                    }

                    lab[zeile, spalte] = '*';
                    zeile = newZeile;
                    spalte = newSpalte;
                    lab[zeile, spalte] = 'S';

                    schritte++;
                    Console.WriteLine($"{schritte,2} gegangen. Es bleiben {150 - schritte,2}");
                }
            }
            Console.ReadLine();
        }


        static void RenderLabyrinth(char[,] lab)
        {
            Console.SetCursorPosition(0, 0);
            for (int z = 0; z < lab.GetLength(0); z++)
            {
                for (int s = 0; s < lab.GetLength(1); s++)
                {
                    if (lab[z, s] == '#')
                    {
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        Console.Write("██");
                    }
                    else if (lab[z, s] == 'Z')
                    {
                        Console.Write("🏨");
                    }
                    else if (lab[z, s] == 'S')
                    {
                        Console.Write("🦽");
                    }
                    else if (lab[z, s] == '*')
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("..");
                    }
                    else
                    {
                        Console.Write("  ");
                    }
                }
                Console.WriteLine();
            }
            Console.ResetColor();
        }

    }
}
