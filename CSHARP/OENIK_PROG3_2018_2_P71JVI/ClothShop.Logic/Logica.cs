// <copyright file="Logica.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ClothShop.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Linq;
    using ClothShop.Data;
    using ClothShop.Repository;

    /// <summary>
    /// This is the Logica class(logic)
    /// </summary>
    /// <typeparam name="T">This can be Megrendeles,Megrendelo,Ruha types</typeparam>
    public class Logica<T> : ILogic<T>
        where T : class
    {
        private readonly IRepository<T> repo;

        /// <summary>
        /// Initializes a new instance of the <see cref="Logica{T}"/> class.
        /// This is the constructor of the Logica class
        /// </summary>
        /// <param name="repository">Dependency injection</param>
        public Logica(IRepository<T> repository)
        {
            this.repo = repository;
        }

        /// <summary>
        /// It transfers the data from the program.cs to the Delete method in the repo.cs, and gives back the returned parameter from the repo.cs
        /// </summary>
        /// <param name="entity">This method brings the entitytype to the repo.cs</param>
        /// <returns> It returns with a boolean, to check if the Delete Method was successful </returns>
        public bool Delete(T entity)
        {
            if (entity is Megrendeles)
            {
                return this.repo.Delete(entity);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// It transfers the data from the program.cs to the Insert method in the repo.cs, and gives back the returned parameter from the repo.cs
        /// </summary>
        /// <param name="entity">This method brings the entitytype to the repo.cs</param>
        /// <returns> It returns with a boolean, to check if the Insert Method was successful </returns>
        public bool Insert(T entity)
        {
            if (entity is Megrendeles)
            {
                return this.repo.Insert(entity);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// It transfers the data from the program.cs to the Update method in the repo.cs, and gives back the returned parameter from the repo.cs
        /// </summary>
        /// <param name="entity">This method brings the entitytype to the repo.cs</param>
        /// <returns> It returns with a boolean, to check if the Update Method was successful </returns>
        public bool Update(T entity)
        {
            if (entity is Megrendeles)
            {
                return this.repo.Update(entity);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// This method is getting all the RendelesId from the database
        /// </summary>
        /// <param name="entity">This method brings the entitytype to the repo.cs</param>
        /// <returns>It returns all the RendelesId from the Megrendeles table</returns>
        public int[] CountAll(T entity)
        {
            return this.repo.CountAll(entity);
        }

        /// <summary>
        /// This method gives the selected datas from the repo.cs to the program.cs
        /// </summary>
        /// <param name="entity">The method decides if the T param is Megrendeles,Megrendelo,Ruha type</param>
        /// <returns>It returns IEnumerable object(The object is not clear, wheter it is Megrendeles, Megrendelo , Ruha), because it will give back usually more then 1 object </returns>
        public IEnumerable<object> CrudSelect(T entity)
        {
            return this.repo.CrudSelect(entity);
        }

        /// <summary>
        /// This method will transfer the datas (given by the user itself) to the repo.cs and after that , it transfers the selected datas from the repo.cs to the console
        /// </summary>
        /// <param name="entity">The method decides if the T param is Megrendeles,Megrendelo,Ruha type</param>
        /// <returns>It returns IEnumerable object(The object is not clear, wheter it is Megrendeles, Megrendelo , Ruha), because it will sometimes give back more then 1 object </returns>
        public IEnumerable<object> NonCrudSelect(T entity)
        {
            if (entity is Megrendeles)
            {
                IEnumerable<Megrendeles> q = from i in this.repo.GetAll() as IEnumerable<Megrendeles>
                                             where i.DB_szam >= (int)(entity as Megrendeles).DB_szam
                                             orderby i.DB_szam descending
                                             select new Megrendeles
                                             {
                                                 DB_szam = i.DB_szam,
                                                 Hatarido = i.Hatarido,
                                                 Leadasi_idopont = i.Leadasi_idopont,
                                                 Megrendelo = i.Megrendelo,
                                                 RendelesID = i.RendelesID,
                                                 RuhaID = i.RuhaID,
                                                 VasarloID = i.VasarloID
                                             };
                return q;
            }
            else if (entity is Ruha)
            {
                int? ar = (entity as Ruha).Ar;
                IEnumerable<Ruha> q = from i in this.repo.GetAll() as IEnumerable<Ruha>
                                    where i.Ar < ar
                                    orderby i.Ar descending
                                    select new Ruha
                                    {
                                        Anyag = i.Anyag,
                                        Ar = i.Ar,
                                        Meret = i.Meret,
                                        RuhaID = i.RuhaID,
                                        Tipus = i.Tipus
                                    };
                return q;
            }
            else if (entity is Megrendelo)
            {
                IEnumerable<Megrendelo> q = from i in this.repo.GetAll() as IEnumerable<Megrendelo>
                                            where i.Varos.Equals((entity as Megrendelo).Varos)
                                            orderby i.VasarloID descending
                                            select new Megrendelo
                                            {
                                                Cim = i.Cim,
                                                Iranyitoszam = i.Iranyitoszam,
                                                Megrendeles = i.Megrendeles,
                                                VasarloID = i.VasarloID,
                                                Telefonszam = i.Telefonszam,
                                                Nev = i.Nev,
                                                Varos = i.Varos
                                            };
                return q;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// This is the JavaEndPoint method , which is writing xml elements to the console from the java project
        /// </summary>
        public void JavaEndPoint()
        {
            XDocument d = XDocument.Load("http://localhost:8080/ClothShop.Java/AjanloServlet?Cloth=oltony%28fekete%29&vasarlonev=Feher+Jeno&Ar=100000");

            var collection = from x in d.Descendants("details")
                             select new
                             {
                                 Ar = Convert.ToInt32(x.Element("Ar").Value),
                                 Anyag = x.Element("cloth").Value,
                                 VasarloNev = x.Element("vasarlonev").Value
                             };

            foreach (var item in collection)
            {
                Console.WriteLine(item.ToString());
            }
        }

        /// <summary>
        /// This method is getting all the cities from the database
        /// </summary>
        /// <param name="entity">The method decides if the T param is Megrendeles,Megrendelo,Ruha type</param>
        /// /// <returns>All the city names in the database</returns>
        public string[] Cities(T entity)
        {
            return this.repo.Cities(entity);
        }

        /// <summary>
        /// This is the GetAll method which return an entity from the database
        /// </summary>
        /// <returns>It returns an entity from the database which can be Ruha,Megrendeles,Megrendelo</returns>
        public IEnumerable<T> GetAll()
        {
            return this.repo.GetAll();
        }
    }
}
