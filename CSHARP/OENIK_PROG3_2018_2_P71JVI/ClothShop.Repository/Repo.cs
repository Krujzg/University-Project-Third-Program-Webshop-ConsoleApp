// <copyright file="Repo.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ClothShop.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using ClothShop.Data;

    /// <summary>
    /// This is the Repository class
    /// </summary>
    /// <typeparam name="T">This can be Megrendeles,Megrendelo,Ruha types</typeparam>
    public class Repo<T> : IRepository<T>
        where T : class
    {
        private readonly DBEntities entity;

        /// <summary>
        /// Initializes a new instance of the <see cref="Repo{T}"/> class.
        /// This is the constructor of the Repo.cs
        /// </summary>
        public Repo()
        {
            // System.Diagnostics.Debugger.NotifyOfCrossThreadDependency();
            this.entity = new DBEntities();
        }

        /// <summary>
        /// This is the GetAll method which return an entity from the database
        /// </summary>
        /// <returns>It returns an entity from the database which can be Ruha,Megrendeles,Megrendelo</returns>
        public IEnumerable<T> GetAll()
        {
            return this.entity.Set<T>();
        }

        /// <summary>
        /// This is the Insert method
        /// </summary>
        /// <param name="entity">The method decides if the T param is Megrendeles,Megrendelo,Ruha type</param>
        /// <returns>It returns a boolean value, to check if the Insert Method was successful</returns>
        public bool Insert(T entity)
        {
            if (entity is Megrendeles)
            {
                Megrendeles ujmegrendeles = entity as Megrendeles;
                Megrendelo ujmegrendelo = ujmegrendeles.Megrendelo;

                this.entity.Megrendelo.Add(ujmegrendelo);
                this.entity.Megrendeles.Add(ujmegrendeles);
                this.entity.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// This is the Delete Method
        /// </summary>
        /// <param name="entity">The method decides if the T param is Megrendeles,Megrendelo,Ruha type</param>
        /// <returns>It returns a boolean value, to check if the Delete Method was successful</returns>
        public bool Delete(T entity)
        {
            if (entity is Megrendeles)
            {
                int id = (entity as Megrendeles).RendelesID;
                Megrendelo valaki = this.entity.Megrendelo.Single(x => x.VasarloID == id);
                Megrendeles rendeles = this.entity.Megrendeles.Single(x => x.VasarloID == id);
                this.entity.Megrendelo.Remove(valaki);
                this.entity.Megrendeles.Remove(rendeles);
                this.entity.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// This is the Update Method
        /// </summary>
        /// <param name="entity">The method decides if the T param is Megrendeles,Megrendelo,Ruha type</param>
        /// <returns>It returns a boolean value, to check if the Update Method was successful</returns>
        public bool Update(T entity)
        {
            if (entity is Megrendeles)
            {
                int id = (entity as Megrendeles).VasarloID;
                Megrendelo deletablemegrendelo = this.entity.Megrendelo.Single(x => x.VasarloID == id);
                Megrendeles deletablemegrendeles = this.entity.Megrendeles.Single(x => x.VasarloID == id);
                this.entity.Megrendelo.Remove(deletablemegrendelo);
                this.entity.Megrendeles.Remove(deletablemegrendeles);
                this.entity.Megrendelo.Add((entity as Megrendeles).Megrendelo);
                this.entity.Megrendeles.Add(entity as Megrendeles);
                this.entity.SaveChanges();
                return true;
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
            if (entity is Megrendeles)
            {
                int[] tomb = new int[(this.GetAll() as IEnumerable<Megrendeles>).Count()];
                int i = 0;
                foreach (var item in this.GetAll() as IEnumerable<Megrendeles>)
                {
                    tomb[i] = item.RendelesID;
                    i++;
                }

                return tomb;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// This is the crud Select method, it selects everything from the T type table(Ruha,Megrendeles,Megrendelo)
        /// </summary>
        /// <param name="entity">The method decides if the T param is Megrendeles,Megrendelo,Ruha type</param>
        /// <returns>It returns IEnumerable object(The object is not clear, wheter it is Megrendeles, Megrendelo , Ruha), because it will give back usually more then 1 object </returns>
        public IEnumerable<object> CrudSelect(T entity)
        {
            if (entity is Ruha)
            {
                IEnumerable<Ruha> q = from i in this.GetAll() as IEnumerable<Ruha>
                                      select i;
                return q;
            }
            else if (entity is Megrendeles)
            {
                IEnumerable<Megrendeles> q = from i in this.GetAll() as IEnumerable<Megrendeles>
                                             select i;
                return q;
            }
            else if (entity is Megrendelo)
            {
                IEnumerable<Megrendelo> q = from i in this.GetAll() as IEnumerable<Megrendelo>
                                            select i;
                return q;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// This method is getting all the cities from the database
        /// </summary>
        /// <param name="entity">The method decides if the T param is Megrendeles,Megrendelo,Ruha type</param>
        /// /// <returns>All the city names in the database</returns>
        public string[] Cities(T entity)
        {
            if (entity is Megrendelo)
            {
                string[] tomb = new string[(this.GetAll() as IEnumerable<Megrendelo>).Count()];
                int i = 0;
                foreach (var item in this.GetAll() as IEnumerable<Megrendelo>)
                {
                    if (!tomb.Contains(item.Varos))
                    {
                        tomb[i] = item.Varos;
                    }

                    i++;
                }

                return tomb;
            }
            else
            {
                return null;
            }
        }
    }
}
