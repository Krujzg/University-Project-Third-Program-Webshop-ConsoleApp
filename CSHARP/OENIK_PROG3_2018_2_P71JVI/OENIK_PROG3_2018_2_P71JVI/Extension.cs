// <copyright file="Extension.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ClothShop
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ClothShop.Data;

    /// <summary>
    /// This is the Extension class, it has been created there , because the method within this class is important in the program.cs
    /// </summary>
    public static class Extension
    {
        /// <summary>
        /// This method writes the Select methods values to the console
        /// </summary>
        /// <typeparam name="T">It can be Megrendeles,Megrendelo,Ruha types</typeparam>
        /// <param name="input">This is the input which is given by the Select methods</param>
        public static void ToConsole<T>(this IEnumerable<T> input)
        {
            if (input is IEnumerable<Ruha>)
            {
                Console.WriteLine("A ruhák listája: \n");
                foreach (Ruha item in input as IEnumerable<Ruha>)
                {
                    Console.WriteLine("Ruha id: " + item.RuhaID + "\nTípusa: " + item.Tipus + "\nMérete: " + item.Meret + "\nÁra: " + item.Ar + " FT");
                    Console.WriteLine();
                }
            }
            else if (input is IEnumerable<Megrendelo>)
            {
                Console.WriteLine("A Megrendelők adatai:\n");
                foreach (Megrendelo item in input as IEnumerable<Megrendelo>)
                {
                    Console.WriteLine("A Megrendelő id-je: " + item.VasarloID + "\nNeve: " + item.Nev + "\nVárosa: " + item.Varos + "\nCíme: " + item.Cim + "\nÍrányítószáma: " + item.Iranyitoszam + "\nTelefonszáma: " + item.Telefonszam);
                    Console.WriteLine();
                }
            }
            else if (input is IEnumerable<Megrendeles>)
            {
                Console.WriteLine("A Megrendelők adatai:\n");
                foreach (Megrendeles item in input as IEnumerable<Megrendeles>)
                {
                    Console.WriteLine("A Megrendelő id-je: " + item.VasarloID + "\nRuha idje: " + item.RuhaID + "\nRendelés dbszáma: " + item.DB_szam + "\nRendelésidje: " + item.RendelesID + "\nLeadási időpont: " + item.Leadasi_idopont + "\nHatáridő: " + item.Hatarido);
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("Sajnos nem találtunk eredményt");
            }
        }
    }
}
