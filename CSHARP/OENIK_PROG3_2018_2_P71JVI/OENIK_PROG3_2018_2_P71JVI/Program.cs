// <copyright file="Program.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ClothShop
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using ClothShop.Data;
    using ClothShop.Logic;
    using ClothShop.Repository;

    /// <summary>
    /// This is the UI
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The UI main method
        /// </summary>
        /// <param name="args">beginning parameter for the main method</param>
        public static void Main(string[] args)
        {
            Logica<Megrendeles> logika_megrendeles = new Logica<Megrendeles>(new Repo<Megrendeles>());
            Logica<Megrendelo> logika_megrendelo = new Logica<Megrendelo>(new Repo<Megrendelo>());
            Logica<Ruha> logika_ruha = new Logica<Ruha>(new Repo<Ruha>());

            // menupoint input
            string be;
            int ruhaid;
            int input;
            int vasarloid;
            int dbszam;
            int rendelesid;
            bool igaze = false;

            int[] feltetel = logika_megrendeles.CountAll(new Megrendeles());
            try
            {
                do
                {
                    // writing on the title : feltelel.Count()
                    Console.Title = "KRUJZ WEBSHOP ( A leadott rendel�sek sz�ma: " + feltetel.Count() + " db )";
                    Console.WriteLine("(1) - Rendelhet� ruh�k megtekint�se");
                    Console.WriteLine("(2) - Megrendel�k megtekint�se");
                    Console.WriteLine("(3) - Megrendel�sek megtekint�se");
                    Console.WriteLine("(4) - �j ruha rendel�s�nek lead�sa");
                    Console.WriteLine("(5) - Rendel�s t�rl�se");
                    Console.WriteLine("(6) - Megrendel�s adatainak szerkeszt�se");
                    Console.WriteLine("(7) - Adott db sz�m feletti megrendel�sek keres�se");
                    Console.WriteLine("(8) - Meghat�rozott �r alatt lev� ruh�k");
                    Console.WriteLine("(9) - Megrendel�k v�ros szerinti list�z�sa");
                    Console.WriteLine("(10) - JAVA v�gpont");
                    Console.WriteLine("(esc) - kil�p�s");
                    be = Console.ReadLine();

                    if (be == "1")
                    {
                        Console.Title = "Ruh�k list�z�sa";
                        if (feltetel.Count() == 0)
                        {
                            Console.Clear();
                            Console.WriteLine("Nincs megjelen�thet� adat , k�rlek adj hozz� �jat a (2) men�pontban");
                            Console.ReadLine();
                            Console.Clear();
                        }
                        else
                        {
                            Console.Clear();
                            logika_ruha.CrudSelect(new Ruha()).ToConsole();
                            Console.ReadLine();
                            Console.Clear();
                        }
                    } // CrudSelectRuha
                    else if (be == "2")
                    {
                        Console.Title = "Megrendel�k list�z�sa";
                        if (feltetel.Count() == 0)
                        {
                            Console.Clear();
                            Console.WriteLine("Nincs megjelen�thet� adat , k�rlek adj hozz� �jat a (2) men�pontban");
                            Console.ReadLine();
                            Console.Clear();
                        }
                        else
                        {
                            Console.Clear();
                            logika_megrendelo.CrudSelect(new Megrendelo()).ToConsole();
                            Console.ReadLine();
                            Console.Clear();
                        }
                    } // CrudSelectMegrendelo
                    else if (be == "3")
                    {
                        // check if there is any data in the Megrendeles table
                        if (feltetel.Count() == 0)
                        {
                            Console.Clear();
                            Console.WriteLine("Nincs megjelen�thet� adat , k�rlek adj hozz� �jat a (2) men�pontban");
                            Console.ReadLine();
                            Console.Clear();
                        }
                        else
                        {
                            Console.Title = "Megrendel�sek list�z�sa";
                            Console.Clear();
                            logika_megrendeles.CrudSelect(new Megrendeles()).ToConsole();
                            Console.ReadLine();
                            Console.Clear();
                        }
                    } // CrudSelectMegrendeles
                    else if (be == "4")
                    {
                        Console.Title = "�j megrendel�s";
                        do
                        {
                            Console.Clear();
                            Console.Write("Ruha id: ");
                            ruhaid = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine();

                            // ruhaid only from 1 to 6
                            if (!(ruhaid > 0 && ruhaid < 7))
                            {
                                Console.Clear();
                                Console.WriteLine("A ruhaid-je nem l�tezik, 1-6 ig vannak csak ruha id-k");
                                Console.ReadLine();
                                Console.Clear();
                            }
                        }
                        while (!(ruhaid > 0 && ruhaid < 7));
                        do
                        {
                            Console.Write("V�s�rl� id: ");
                            vasarloid = Convert.ToInt32(Console.ReadLine());

                            // if we have the vasarloid in the Megrendeles table
                            if (feltetel.Contains(vasarloid) || vasarloid < 0)
                            {
                                Console.Clear();
                                for (int i = 0; i < feltetel.Length; i++)
                                {
                                    Console.WriteLine("Ezek a l�tez� id-k : " + feltetel[i]);
                                }

                                Console.ReadLine();
                                Console.Clear();
                            }
                        }
                        while (feltetel.Contains(vasarloid) || vasarloid < 0);
                        Console.Write("N�v: ");
                        string nev = Console.ReadLine();
                        Console.Write("C�m: ");
                        string cim = Console.ReadLine();
                        Console.Write("V�ros: ");
                        string varos = Console.ReadLine();
                        Console.Write("Ir�ny�t�sz�m: ");
                        string irszam = Console.ReadLine();
                        Console.Write("Telefonsz�m: ");
                        string telszam = Console.ReadLine();
                        rendelesid = vasarloid;
                        Console.Write("Mennyis�g:(db) ");
                        int db = Convert.ToInt32(Console.ReadLine());

                        Megrendelo ujvasarlo = new Megrendelo()
                        {
                            VasarloID = vasarloid,
                            Nev = nev,
                            Cim = cim,
                            Varos = varos,
                            Iranyitoszam = irszam,
                            Telefonszam = telszam
                        };

                        Megrendeles ujmegrendeles = new Megrendeles()
                        {
                            VasarloID = vasarloid,
                            Leadasi_idopont = DateTime.Now,
                            Hatarido = DateTime.Now.AddDays(30),
                            DB_szam = db,
                            RuhaID = ruhaid,
                            RendelesID = rendelesid,
                            Megrendelo = ujvasarlo
                        };

                        igaze = logika_megrendeles.Insert(ujmegrendeles);

                        if (igaze)
                        {
                            Console.Clear();
                            Console.WriteLine("A rendel�s�t sikeresen leadta");
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Sikertelen rendel�s");
                        }

                        Console.ReadLine();
                        Console.Clear();

                        // we only want to invite this method when we change the database
                        feltetel = logika_megrendeles.CountAll(new Megrendeles());
                    } // Insert
                    else if (be == "5")
                    {
                        Console.Title = "Megrendel�s t�rl�se";

                        // Check if there is any data in the Megrendeles table
                        if (feltetel.Count() == 0)
                        {
                            Console.Clear();
                            Console.WriteLine("Nincs megjelen�thet� adat , k�rlek adj hozz� �jat a (2) men�pontban");
                            Console.ReadLine();
                            Console.Clear();
                        }
                        else
                        {
                            do
                            {
                                Console.Clear();
                                Console.WriteLine("T�rl�s:");
                                Console.WriteLine("�rja be az Rendel�sId-t a t�rl�shez");
                                input = Convert.ToInt32(Console.ReadLine());

                                // check if the Megrendeles table has got the input
                                if (!feltetel.Contains(input))
                                {
                                    Console.Clear();
                                    Console.WriteLine("Nem l�tez� id");
                                    Console.ReadLine();
                                    Console.Clear();
                                }
                            }
                            while (!feltetel.Contains(input));
                            Megrendeles megrendeles = new Megrendeles()
                            {
                                RendelesID = input
                            };
                            igaze = logika_megrendeles.Delete(megrendeles);
                            if (igaze)
                            {
                                Console.Clear();
                                Console.WriteLine("Sikeres t�rl�s");
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("Sikertelen t�rl�s");
                            }

                            Console.ReadLine();
                            Console.Clear();

                            // we only want to invite this method when the database is changed
                            feltetel = logika_megrendeles.CountAll(new Megrendeles());
                        }
                    } // Delete
                    else if (be == "6")
                    {
                        if (feltetel.Count() == 0)
                        {
                            Console.Clear();
                            Console.WriteLine("Nincs megjelen�thet� adat , k�rlek adj hozz� �jat a (2) men�pontban");
                            Console.ReadLine();
                            Console.Clear();
                        }
                        else
                        {
                            Console.Clear();
                            Console.Title = "Megrendel� szerkeszt�se";
                            do
                            {
                                Console.WriteLine("A szerkeszt�s megkezd�t�tt:");
                                Console.WriteLine("Megrendel� adatai:");
                                Console.Write("V�s�rl� id: ");
                                vasarloid = Convert.ToInt32(Console.ReadLine());

                                // if we have the vasarloid in the Megrendeles table
                                if (!feltetel.Contains(vasarloid))
                                {
                                    Console.Clear();
                                    for (int i = 0; i < feltetel.Length; i++)
                                    {
                                        Console.WriteLine("Ezek a l�tez� id-k : " + feltetel[i]);
                                    }

                                    Console.ReadLine();
                                    Console.Clear();
                                }
                            }
                            while (!feltetel.Contains(vasarloid) || vasarloid < 0);
                            Console.Write("N�v: ");
                            string nev = Console.ReadLine();
                            Console.Write("C�m: ");
                            string cim = Console.ReadLine();
                            Console.Write("V�ros: ");
                            string varos = Console.ReadLine();
                            Console.Write("Ir�ny�t�sz�m: ");
                            string irszam = Console.ReadLine();
                            Console.Write("Telefonsz�m: ");
                            string telszam = Console.ReadLine();
                            Console.WriteLine("\nMegrendel�s adatai: ");
                            Console.Write("Darabsz�m: ");
                            dbszam = Convert.ToInt32(Console.ReadLine());
                            do
                            {
                                Console.Write("RuhaId:  ");
                                ruhaid = Convert.ToInt32(Console.ReadLine());

                                // ruhaid only from 1 to 6
                                if (!(ruhaid > 0 && ruhaid < 7))
                                {
                                    Console.Clear();
                                    Console.WriteLine("A ruhaid-je nem l�tezik, 1-6 ig vannak csak ruha id-k");
                                    Console.ReadLine();
                                    Console.Clear();
                                }
                            }
                            while (!(ruhaid > 0 && ruhaid < 7));

                            Megrendelo megrendelo = new Megrendelo()
                            {
                                VasarloID = vasarloid,
                                Nev = nev,
                                Cim = cim,
                                Iranyitoszam = irszam,
                                Telefonszam = telszam,
                                Varos = varos
                            };
                            Megrendeles megrendeles = new Megrendeles()
                            {
                                DB_szam = dbszam,
                                RuhaID = 1,
                                RendelesID = vasarloid,
                                VasarloID = vasarloid,
                                Leadasi_idopont = DateTime.Now,
                                Hatarido = DateTime.Now.AddDays(30),
                                Megrendelo = megrendelo
                            };

                            igaze = logika_megrendeles.Update(megrendeles);
                            Console.Clear();
                            if (igaze)
                            {
                                Console.Clear();
                                Console.WriteLine("Sikeres szerkeszt�s");
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("Sikertelen szerkesztes");
                            }

                            Console.ReadLine();
                            Console.Clear();
                        }
                    } // Update
                    else if (be == "7")
                    {
                        if (feltetel.Count() == 0)
                        {
                            Console.Clear();
                            Console.WriteLine("Nincs megjelen�thet� adat , k�rlek adj hozz� �jat a (2) men�pontban");
                            Console.ReadLine();
                            Console.Clear();
                        }
                        else
                        {
                            Console.Title = "Adott db sz�m feletti megrendel�sek keres�se";
                            Console.Clear();
                            Console.Write("K�rlek �rd be a db sz�mot amire szeretn�l keresni: ");
                            dbszam = Convert.ToInt32(Console.ReadLine());
                            Console.Clear();
                            Megrendeles megrendeles = new Megrendeles()
                            {
                                DB_szam = dbszam
                            };
                            logika_megrendeles.NonCrudSelect(megrendeles).ToConsole();
                            Console.ReadLine();
                            Console.Clear();
                        }
                    } // NonCrudSelectMegrendeles
                    else if (be == "8")
                    {
                        if (feltetel.Count() == 0)
                        {
                            Console.Clear();
                            Console.WriteLine("Nincs megjelen�thet� adat , k�rlek adj hozz� �jat a (2) men�pontban");
                            Console.ReadLine();
                            Console.Clear();
                        }
                        else
                        {
                            Console.Title = "Meghat�rozott �r alatt lev� ruh�k";
                            Console.Clear();
                            Console.Write("K�rlek �rd be az �rat amire szeretn�l keresni: ");
                            int ar = Convert.ToInt32(Console.ReadLine());
                            Console.Clear();
                            Ruha ruhak = new Ruha()
                            {
                                Ar = ar
                            };
                            logika_ruha.NonCrudSelect(ruhak).ToConsole();
                            Console.ReadLine();
                            Console.Clear();
                        }
                    } // NonCrudSelectRuha
                    else if (be == "9")
                    {
                        if (feltetel.Count() == 0)
                        {
                            Console.Clear();
                            Console.WriteLine("Nincs megjelen�thet� adat , k�rlek adj hozz� �jat a (2) men�pontban");
                            Console.ReadLine();
                            Console.Clear();
                        }
                        else
                        {
                            Console.Title = "Megrendel�k v�ros szerinti list�z�sa";
                            Console.Clear();
                            string[] tomb = logika_megrendelo.Cities(new Megrendelo());
                            Console.WriteLine("A megrendel�k v�rosai: ");
                            foreach (string item in tomb)
                            {
                                Console.WriteLine(item);
                            }

                            Console.WriteLine("K�rlek �rd be h melyik v�rosra szeretn�l sz�rni ");
                            string varos = Console.ReadLine();
                            Console.Clear();
                            Megrendelo megrendelo = new Megrendelo()
                            {
                                Varos = varos
                            };
                            logika_megrendelo.NonCrudSelect(megrendelo).ToConsole();
                            Console.ReadLine();
                            Console.Clear();
                        }
                    } // NonCrudSelectMegrendelo
                    else if (be == "10")
                    {
                        Console.Clear();
                        Console.Title = "JAVA v�gpont";
                        logika_megrendeles.JavaEndPoint();
                        Console.ReadLine();
                        Console.Clear();
                    } // JavaEndPoint
                    else if (be == "esc")
                    {
                        Console.Clear();
                        Console.WriteLine("K�sz�nj�k ,hogy a KRUJZ WEBSHOPOT v�lasztotta");
                    } // Escape
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Hib�s karakter lett be�tve k�rlek pr�b�lkozz �jra");
                        Console.ReadLine();
                        Console.Clear();
                    } // mistyped characters
                }
                while (be != "esc"); // Escape
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
