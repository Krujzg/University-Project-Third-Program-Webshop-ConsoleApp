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
                    Console.Title = "KRUJZ WEBSHOP ( A leadott rendelések száma: " + feltetel.Count() + " db )";
                    Console.WriteLine("(1) - Rendelhetõ ruhák megtekintése");
                    Console.WriteLine("(2) - Megrendelõk megtekintése");
                    Console.WriteLine("(3) - Megrendelések megtekintése");
                    Console.WriteLine("(4) - Új ruha rendelésének leadása");
                    Console.WriteLine("(5) - Rendelés törlése");
                    Console.WriteLine("(6) - Megrendelés adatainak szerkesztése");
                    Console.WriteLine("(7) - Adott db szám feletti megrendelések keresése");
                    Console.WriteLine("(8) - Meghatározott ár alatt levõ ruhák");
                    Console.WriteLine("(9) - Megrendelõk város szerinti listázása");
                    Console.WriteLine("(10) - JAVA végpont");
                    Console.WriteLine("(esc) - kilépés");
                    be = Console.ReadLine();

                    if (be == "1")
                    {
                        Console.Title = "Ruhák listázása";
                        if (feltetel.Count() == 0)
                        {
                            Console.Clear();
                            Console.WriteLine("Nincs megjeleníthetõ adat , kérlek adj hozzá újat a (2) menüpontban");
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
                        Console.Title = "Megrendelõk listázása";
                        if (feltetel.Count() == 0)
                        {
                            Console.Clear();
                            Console.WriteLine("Nincs megjeleníthetõ adat , kérlek adj hozzá újat a (2) menüpontban");
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
                            Console.WriteLine("Nincs megjeleníthetõ adat , kérlek adj hozzá újat a (2) menüpontban");
                            Console.ReadLine();
                            Console.Clear();
                        }
                        else
                        {
                            Console.Title = "Megrendelések listázása";
                            Console.Clear();
                            logika_megrendeles.CrudSelect(new Megrendeles()).ToConsole();
                            Console.ReadLine();
                            Console.Clear();
                        }
                    } // CrudSelectMegrendeles
                    else if (be == "4")
                    {
                        Console.Title = "Új megrendelés";
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
                                Console.WriteLine("A ruhaid-je nem létezik, 1-6 ig vannak csak ruha id-k");
                                Console.ReadLine();
                                Console.Clear();
                            }
                        }
                        while (!(ruhaid > 0 && ruhaid < 7));
                        do
                        {
                            Console.Write("Vásárló id: ");
                            vasarloid = Convert.ToInt32(Console.ReadLine());

                            // if we have the vasarloid in the Megrendeles table
                            if (feltetel.Contains(vasarloid) || vasarloid < 0)
                            {
                                Console.Clear();
                                for (int i = 0; i < feltetel.Length; i++)
                                {
                                    Console.WriteLine("Ezek a létezõ id-k : " + feltetel[i]);
                                }

                                Console.ReadLine();
                                Console.Clear();
                            }
                        }
                        while (feltetel.Contains(vasarloid) || vasarloid < 0);
                        Console.Write("Név: ");
                        string nev = Console.ReadLine();
                        Console.Write("Cím: ");
                        string cim = Console.ReadLine();
                        Console.Write("Város: ");
                        string varos = Console.ReadLine();
                        Console.Write("Irányítószám: ");
                        string irszam = Console.ReadLine();
                        Console.Write("Telefonszám: ");
                        string telszam = Console.ReadLine();
                        rendelesid = vasarloid;
                        Console.Write("Mennyiség:(db) ");
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
                            Console.WriteLine("A rendelését sikeresen leadta");
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Sikertelen rendelés");
                        }

                        Console.ReadLine();
                        Console.Clear();

                        // we only want to invite this method when we change the database
                        feltetel = logika_megrendeles.CountAll(new Megrendeles());
                    } // Insert
                    else if (be == "5")
                    {
                        Console.Title = "Megrendelés törlése";

                        // Check if there is any data in the Megrendeles table
                        if (feltetel.Count() == 0)
                        {
                            Console.Clear();
                            Console.WriteLine("Nincs megjeleníthetõ adat , kérlek adj hozzá újat a (2) menüpontban");
                            Console.ReadLine();
                            Console.Clear();
                        }
                        else
                        {
                            do
                            {
                                Console.Clear();
                                Console.WriteLine("Törlés:");
                                Console.WriteLine("Írja be az RendelésId-t a törléshez");
                                input = Convert.ToInt32(Console.ReadLine());

                                // check if the Megrendeles table has got the input
                                if (!feltetel.Contains(input))
                                {
                                    Console.Clear();
                                    Console.WriteLine("Nem létezõ id");
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
                                Console.WriteLine("Sikeres törlés");
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("Sikertelen törlés");
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
                            Console.WriteLine("Nincs megjeleníthetõ adat , kérlek adj hozzá újat a (2) menüpontban");
                            Console.ReadLine();
                            Console.Clear();
                        }
                        else
                        {
                            Console.Clear();
                            Console.Title = "Megrendelõ szerkesztése";
                            do
                            {
                                Console.WriteLine("A szerkesztés megkezdõtött:");
                                Console.WriteLine("Megrendelõ adatai:");
                                Console.Write("Vásárló id: ");
                                vasarloid = Convert.ToInt32(Console.ReadLine());

                                // if we have the vasarloid in the Megrendeles table
                                if (!feltetel.Contains(vasarloid))
                                {
                                    Console.Clear();
                                    for (int i = 0; i < feltetel.Length; i++)
                                    {
                                        Console.WriteLine("Ezek a létezõ id-k : " + feltetel[i]);
                                    }

                                    Console.ReadLine();
                                    Console.Clear();
                                }
                            }
                            while (!feltetel.Contains(vasarloid) || vasarloid < 0);
                            Console.Write("Név: ");
                            string nev = Console.ReadLine();
                            Console.Write("Cím: ");
                            string cim = Console.ReadLine();
                            Console.Write("Város: ");
                            string varos = Console.ReadLine();
                            Console.Write("Irányítószám: ");
                            string irszam = Console.ReadLine();
                            Console.Write("Telefonszám: ");
                            string telszam = Console.ReadLine();
                            Console.WriteLine("\nMegrendelés adatai: ");
                            Console.Write("Darabszám: ");
                            dbszam = Convert.ToInt32(Console.ReadLine());
                            do
                            {
                                Console.Write("RuhaId:  ");
                                ruhaid = Convert.ToInt32(Console.ReadLine());

                                // ruhaid only from 1 to 6
                                if (!(ruhaid > 0 && ruhaid < 7))
                                {
                                    Console.Clear();
                                    Console.WriteLine("A ruhaid-je nem létezik, 1-6 ig vannak csak ruha id-k");
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
                                Console.WriteLine("Sikeres szerkesztés");
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
                            Console.WriteLine("Nincs megjeleníthetõ adat , kérlek adj hozzá újat a (2) menüpontban");
                            Console.ReadLine();
                            Console.Clear();
                        }
                        else
                        {
                            Console.Title = "Adott db szám feletti megrendelések keresése";
                            Console.Clear();
                            Console.Write("Kérlek írd be a db számot amire szeretnél keresni: ");
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
                            Console.WriteLine("Nincs megjeleníthetõ adat , kérlek adj hozzá újat a (2) menüpontban");
                            Console.ReadLine();
                            Console.Clear();
                        }
                        else
                        {
                            Console.Title = "Meghatározott ár alatt levõ ruhák";
                            Console.Clear();
                            Console.Write("Kérlek írd be az árat amire szeretnél keresni: ");
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
                            Console.WriteLine("Nincs megjeleníthetõ adat , kérlek adj hozzá újat a (2) menüpontban");
                            Console.ReadLine();
                            Console.Clear();
                        }
                        else
                        {
                            Console.Title = "Megrendelõk város szerinti listázása";
                            Console.Clear();
                            string[] tomb = logika_megrendelo.Cities(new Megrendelo());
                            Console.WriteLine("A megrendelõk városai: ");
                            foreach (string item in tomb)
                            {
                                Console.WriteLine(item);
                            }

                            Console.WriteLine("Kérlek írd be h melyik városra szeretnél szûrni ");
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
                        Console.Title = "JAVA végpont";
                        logika_megrendeles.JavaEndPoint();
                        Console.ReadLine();
                        Console.Clear();
                    } // JavaEndPoint
                    else if (be == "esc")
                    {
                        Console.Clear();
                        Console.WriteLine("Köszönjük ,hogy a KRUJZ WEBSHOPOT választotta");
                    } // Escape
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Hibás karakter lett beütve kérlek próbálkozz újra");
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
