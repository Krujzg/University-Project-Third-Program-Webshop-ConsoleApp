// <copyright file="IRepository.cs" company="PlaceholderCompany">
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
    /// This is the inteface of the repo.cs
    /// </summary>
    /// <typeparam name="T">It can be Megrendeles,Megrendelo,Ruha types</typeparam>
    public interface IRepository<T>
        where T : class
    {
        /// <summary>
        /// This is the crud Select method, it selects everything from the T type table(Ruha,Megrendeles,Megrendelo)
        /// </summary>
        /// <param name="entity">The method decides if the T param is Megrendeles,Megrendelo,Ruha type</param>
        /// <returns>It returns IEnumerable object(The object is not clear, wheter it is Megrendeles, Megrendelo , Ruha), because it will give back usually more then 1 object </returns>
        IEnumerable<object> CrudSelect(T entity);

        /// <summary>
        /// This is the Insert method
        /// </summary>
        /// <param name="entity">The method decides if the T param is Megrendeles,Megrendelo,Ruha type</param>
        /// <returns>It returns a boolean value, to check if the Insert Method was successful</returns>
        bool Insert(T entity);

        /// <summary>
        /// This is the Delete Method
        /// </summary>
        /// <param name="entity">The method decides if the T param is Megrendeles,Megrendelo,Ruha type</param>
        /// <returns>It returns a boolean value, to check if the Delete Method was successful</returns>
        bool Delete(T entity);

        /// <summary>
        /// This is the Update Method
        /// </summary>
        /// <param name="entity">The method decides if the T param is Megrendeles,Megrendelo,Ruha type</param>
        /// <returns>It returns a boolean value, to check if the Update Method was successful</returns>
        bool Update(T entity);

        /// <summary>
        /// This method is getting all the RendelesId from the database
        /// </summary>
        /// <param name="entity">This method brings the entitytype to the repo.cs</param>
        /// <returns>It returns all the RendelesId from the Megrendeles table</returns>
        int[] CountAll(T entity);

        /// <summary>
        /// This method is getting all the cities from the database
        /// </summary>
        /// <param name="entity">The method decides if the T param is Megrendeles,Megrendelo,Ruha type</param>
        /// /// <returns>All the city names in the database</returns>
        string[] Cities(T entity);

        /// <summary>
        /// This is the GetAll method which return an entity from the database
        /// </summary>
        /// <returns>It returns an entity from the database which can be Ruha,Megrendeles,Megrendelo</returns>
        IEnumerable<T> GetAll();
    }
}
