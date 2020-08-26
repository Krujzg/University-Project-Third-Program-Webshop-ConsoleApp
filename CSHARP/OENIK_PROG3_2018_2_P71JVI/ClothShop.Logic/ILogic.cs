// <copyright file="ILogic.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ClothShop.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using ClothShop.Data;
    using ClothShop.Repository;

    /// <summary>
    /// This is the interface of the Logic class
    /// </summary>
    /// <typeparam name="T">This can be Megrendeles,Megrendelo,Ruha types</typeparam>
    public interface ILogic<T>
        where T : class
    {
        /// <summary>
        /// It transfers the data from the program.cs to the Delete method in the repo.cs, and gives back the returned parameter from the repo.cs
        /// </summary>
        /// <param name="entity">This method brings the entitytype to the repo.cs</param>
        /// <returns> It returns with a boolean, to check if the Delete Method was successful </returns>
        bool Delete(T entity);

        /// <summary>
        /// It transfers the data from the program.cs to the Update method in the repo.cs, and gives back the returned parameter from the repo.cs
        /// </summary>
        /// <param name="entity">This method brings the entitytype to the repo.cs</param>
        /// <returns> It returns with a boolean, to check if the Update Method was successful </returns>
        bool Update(T entity);

        /// <summary>
        /// It transfers the data from the program.cs to the Insert method in the repo.cs, and gives back the returned parameter from the repo.cs
        /// </summary>
        /// <param name="entity">This method brings the entitytype to the repo.cs</param>
        /// <returns> It returns with a boolean, to check if the Insert Method was successful </returns>
        bool Insert(T entity);

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
        /// This method gives the selected datas from the repo.cs to the program.cs
        /// </summary>
        /// <param name="entity">The method decides if the T param is Megrendeles,Megrendelo,Ruha type</param>
        /// <returns>It returns IEnumerable object(The object is not clear, wheter it is Megrendeles, Megrendelo , Ruha), because it will give back usually more then 1 object </returns>
        IEnumerable<object> CrudSelect(T entity);

        /// <summary>
        /// This method will transfer the datas (given by the user itself) to the repo.cs and after that , it transfers the selected datas from the repo.cs to the console
        /// </summary>
        /// <param name="entity">The method decides if the T param is Megrendeles,Megrendelo,Ruha type</param>
        /// <returns>It returns IEnumerable object(The object is not clear, wheter it is Megrendeles, Megrendelo , Ruha), because it will sometimes give back more then 1 object </returns>
        IEnumerable<object> NonCrudSelect(T entity);

        /// <summary>
        /// This is the GetAll method which return an entity from the database
        /// </summary>
        /// <returns>It returns an entity from the database which can be Ruha,Megrendeles,Megrendelo</returns>
        IEnumerable<T> GetAll();
    }
}
