// <copyright file="Logic_Test.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ClothShop.Logic.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using ClothShop.Data;
    using ClothShop.Logic;
    using ClothShop.Repository;
    using Moq;
    using NUnit.Framework;

    /// <summary>
    /// Test class with mockedrepository
    /// </summary>
    [TestFixture]
    public class Logic_Test
    {
        private Logica<Megrendeles> logicMegrendeles;
        private Logica<Megrendelo> logicMegrendelo;
        private Logica<Ruha> logicRuhak;

        private Mock<IRepository<Megrendeles>> mockedMegrendeles;
        private Mock<IRepository<Megrendelo>> mockedMegrendelo;
        private Mock<IRepository<Ruha>> mockedRuha;

        /// <summary>
        /// This is the setup method for the testes, it has got the mockrepositories and logic declarations , it contains 3 lists which replaces the tables from the database , and the setups
        /// </summary>
        [OneTimeSetUp]
        public void Setup()
        {
                this.mockedMegrendelo = new Mock<IRepository<Megrendelo>>();
                this.mockedMegrendeles = new Mock<IRepository<Megrendeles>>();
                this.mockedRuha = new Mock<IRepository<Ruha>>();

                IEnumerable<Ruha> ruhak = new List<Ruha>()
                {
                new Ruha() { RuhaID = 1, Anyag = "zselatin", Ar = 100, Meret = 30, Tipus = "ételszerű ruha" },
                new Ruha() { RuhaID = 2, Anyag = "papír", Ar = 2, Meret = 30, Tipus = "nadrág" },
                new Ruha() { RuhaID = 3, Anyag = "kínai gyapjú", Ar = 1000, Meret = 300, Tipus = "teritő" }
                };
                IEnumerable<Megrendelo> megrendelok = new List<Megrendelo>()
                {
                new Megrendelo() { VasarloID = 1, Nev = "jankó", Cim = "sas", Varos = "Dunaújváros", Iranyitoszam = "2001", Telefonszam = "1342342" },
                new Megrendelo() { VasarloID = 2, Nev = "panko", Cim = "györgy", Varos = "Baracs", Iranyitoszam = "2001", Telefonszam = "06301231234567897" },
                new Megrendelo() { VasarloID = 3, Nev = "danko", Cim = "teszt", Varos = "Budapest", Iranyitoszam = "2001", Telefonszam = "06304123135627897" }
                };
                IEnumerable<Megrendeles> megrendelesek = new List<Megrendeles>()
                {
                new Megrendeles() { VasarloID = 1, Leadasi_idopont = DateTime.Now, Hatarido = Convert.ToDateTime("2018.12.10."),  DB_szam = 2, RuhaID = 1, RendelesID = 1, Megrendelo = megrendelok.First() },
                new Megrendeles() { VasarloID = 2, Leadasi_idopont = DateTime.Now, Hatarido = Convert.ToDateTime("2018.12.11."), DB_szam = 3, RuhaID = 2, RendelesID = 2, Megrendelo = megrendelok.Take(2).Last() },
                new Megrendeles() { VasarloID = 3, Leadasi_idopont = DateTime.Now, Hatarido = Convert.ToDateTime("2018.12.12."), DB_szam = 4, RuhaID = 3, RendelesID = 3, Megrendelo = megrendelok.Last() }
                };

                this.mockedMegrendelo.Setup(m => m.GetAll()).Returns(megrendelok.AsEnumerable);
                this.mockedMegrendelo.Setup(m => m.CrudSelect(It.IsAny<Megrendelo>())).Returns(megrendelok.AsEnumerable);

                this.mockedRuha.Setup(m => m.GetAll()).Returns(ruhak.AsEnumerable);
                this.mockedRuha.Setup(m => m.CrudSelect(It.IsAny<Ruha>())).Returns(ruhak.AsEnumerable);

                this.mockedMegrendeles.Setup(m => m.GetAll()).Returns(megrendelesek.AsEnumerable);
                this.mockedMegrendeles.Setup(m => m.CrudSelect(It.IsAny<Megrendeles>())).Returns(megrendelesek.AsEnumerable);
                this.mockedMegrendeles.Setup(m => m.Insert(It.IsAny<Megrendeles>())).Returns(true);
                this.mockedMegrendeles.Setup(m => m.Delete(It.IsAny<Megrendeles>())).Returns(true);
                this.mockedMegrendeles.Setup(m => m.Update(It.IsAny<Megrendeles>())).Returns(true);
                this.mockedMegrendeles.Setup(m => m.CountAll(It.IsAny<Megrendeles>())).Returns(new int[] { 1, 2, 3 });

                this.logicRuhak = new Logica<Ruha>(this.mockedRuha.Object);
                this.logicMegrendeles = new Logica<Megrendeles>(this.mockedMegrendeles.Object);
                this.logicMegrendelo = new Logica<Megrendelo>(this.mockedMegrendelo.Object);
        }

        // ------------------------------------------------------------------------ CRUD SELECT START ------------------------------------------------------------------------ //

        /// <summary>
        /// This is the crud SelectMegrendeles method test
        /// </summary>
        [Test]
        public void CrudSelect_Megrendeles()
        {
            // ARRANGE
            // ACT
            Megrendeles result = (Megrendeles)this.logicMegrendeles.CrudSelect(new Megrendeles()).First();

            // ASSERT
            Assert.That(result.VasarloID, Is.EqualTo(1));
            Assert.That(result.DB_szam, Is.EqualTo(2));
            Assert.That(result.RuhaID, Is.EqualTo(1));
            this.mockedMegrendeles.Verify(m => m.CrudSelect(It.IsAny<Megrendeles>()), Times.Once);
        }

        /// <summary>
        /// This is the crud SelectMegrendelo method test
        /// </summary>
        [Test]
        public void CrudSelect_Megrendelo()
        {
            // ARRANGE
            // ACT
            Megrendelo result = (Megrendelo)this.logicMegrendelo.CrudSelect(new Megrendelo()).Last();

            // ASSERT
            Assert.That(result.VasarloID, Is.EqualTo(3));
            Assert.That(result.Cim, Is.EqualTo("teszt"));
            Assert.That(result.Nev, Is.EqualTo("danko"));
            this.mockedMegrendelo.Verify(m => m.CrudSelect(It.IsAny<Megrendelo>()), Times.Once);
        }

        /// <summary>
        /// This is the crud SelectRuha method test
        /// </summary>
        [Test]
        public void CrudSelect_Ruha()
        {
            // ARRANGE
            // ACT
            Ruha result = (Ruha)this.logicRuhak.CrudSelect(new Ruha()).First();

            // ASSERT
            Assert.That(result.Ar, Is.EqualTo(100));
            Assert.That(result.RuhaID, Is.EqualTo(1));
            Assert.That(result.Tipus, Is.EqualTo("ételszerű ruha"));
            this.mockedRuha.Verify(m => m.CrudSelect(It.IsAny<Ruha>()), Times.Once);
        }

        // ------------------------------------------------------------------------ CRUD SELECT END ------------------------------------------------------------------------ //
        // ------------------------------------------------------------------------ NONCRUD SELECTMEGRENDELES START ------------------------------------------------------------------------ //

        /// <summary>
        /// This is the crud SelectMegrendeles method first data test
        /// </summary>
        [Test]
        public void NonCrudSelectMegrendelesFirst()
        {
            // ARRANGE
            Megrendeles megrendeles = new Megrendeles()
            {
                DB_szam = 2
            };

            // ACT
            Megrendeles result = (Megrendeles)this.logicMegrendeles.NonCrudSelect(megrendeles).First();

            // ASSERT
            Assert.That(result.RendelesID, Is.EqualTo(3));
            Assert.That(result.RuhaID, Is.EqualTo(3));
            Assert.That(result.DB_szam, Is.EqualTo(4));
        }

        /// <summary>
        /// This is the crud SelectMegrendeles method last data test
        /// </summary>
        [Test]
        public void NonCrudSelectMegrendelesLast()
        {
            // ARRANGE
            Megrendeles megrendeles = new Megrendeles()
            {
                DB_szam = 1
            };

            // ACT
            Megrendeles result = (Megrendeles)this.logicMegrendeles.NonCrudSelect(megrendeles).Last();

            // ASSERT
            Assert.That(result.Hatarido, Is.EqualTo(Convert.ToDateTime("2018.12.10.")));
            Assert.That(result.VasarloID, Is.EqualTo(1));
            Assert.That(result.DB_szam, Is.EqualTo(2));
        }

        /// <summary>
        /// This is the crud SelectMegrendeles method empty test
        /// </summary>
        [Test]
        public void NonCrudSelectMegrendelesEmpty()
        {
            // ARRANGE
            Megrendeles megrendeles = new Megrendeles()
            {
                DB_szam = 10
            };

            // ACT
            IEnumerable<Megrendeles> result = (IEnumerable<Megrendeles>)this.logicMegrendeles.NonCrudSelect(megrendeles);

            // ASSERT
            Assert.That(result, Is.Empty);
        }

        /// <summary>
        /// This is the crud SelectMegrendeles method count test
        /// </summary>
        [Test]
        public void NonCrudSelectMegrendelesCount()
        {
            // ARRANGE
            Megrendeles megrendeles = new Megrendeles()
            {
                DB_szam = 0
            };

            // ACT
            IEnumerable<Megrendeles> result = (IEnumerable<Megrendeles>)this.logicMegrendeles.NonCrudSelect(megrendeles);

            // ASSERT
            Assert.That(result.Count(), Is.EqualTo(3));
        }

        // ------------------------------------------------------------------------ NONCRUD SELECT MEGRENDELES END ------------------------------------------------------------------------ //
        // ------------------------------------------------------------------------ NONCRUD SELECT RUHA START ------------------------------------------------------------------------ //

        /// <summary>
        /// This is the crud SelectRuha method lowcost test
        /// </summary>
        [Test]
        public void NonCrudSelectRuhaFirstLowCost()
        {
            // ARRANGE
            Ruha ruha = new Ruha()
            {
                Ar = 200
            };

            // ACT
            Ruha result = (Ruha)this.logicRuhak.NonCrudSelect(ruha).First();

            // ASSERT
            Assert.That(result.Ar, Is.EqualTo(100));
            Assert.That(result.RuhaID, Is.EqualTo(1));
            Assert.That(result.Tipus, Is.EqualTo("ételszerű ruha"));
        }

        /// <summary>
        /// This is the crud SelectRuha method bigcost test
        /// </summary>
        [Test]
        public void NonCrudSelectRuhaFirstBigCost()
        {
            // ARRANGE
            Ruha ruha = new Ruha()
            {
                Ar = 100000
            };

            // ACT
            Ruha result = (Ruha)this.logicRuhak.NonCrudSelect(ruha).FirstOrDefault();

            // ASSERT
            Assert.That(result.Ar, Is.EqualTo(1000));
            Assert.That(result.RuhaID, Is.EqualTo(3));
            Assert.That(result.Tipus, Is.EqualTo("teritő"));
        }

        /// <summary>
        /// This is the crud SelectRuha method empty test
        /// </summary>
        [Test]
        public void NonCrudSelectRuhaEmpty()
        {
            // ARRANGE
            Ruha ruha = new Ruha()
            {
                Ar = 1
            };

            // ACT
            IEnumerable<Ruha> result = (IEnumerable<Ruha>)this.logicRuhak.NonCrudSelect(ruha);

            // ASSERT
            Assert.That(result, Is.Empty);
        }

        /// <summary>
        /// This is the crud SelectRuha method count test
        /// </summary>
        [Test]
        public void NonCrudSelectRuhaCount()
        {
            // ARRANGE
            Ruha ruha = new Ruha()
            {
                Ar = 10000000
            };

            // ACT
            IEnumerable<Ruha> result = (IEnumerable<Ruha>)this.logicRuhak.NonCrudSelect(ruha);

            // ASSERT
            Assert.That(result.Count(), Is.EqualTo(3));
        }

        // ------------------------------------------------------------------------ NONCRUD SELECT RUHA END ------------------------------------------------------------------------ //
        // ------------------------------------------------------------------------ NONCRUD SELECT MEGRENDELO START ------------------------------------------------------------------------ //

        /// <summary>
        /// This is the crud SelectMegrendelo method Budapest Count test
        /// </summary>
        [Test]
        public void NonCrudSelectMegrendeloBudapestCount()
        {
            // ARRANGE
            Megrendelo megrendelo = new Megrendelo()
            {
                Varos = "Budapest"
            };

            // ACT
            IEnumerable<Megrendelo> result = (IEnumerable<Megrendelo>)this.logicMegrendelo.NonCrudSelect(megrendelo);

            // ASSERT
            Assert.That(result.Count(), Is.EqualTo(1));
        }

        /// <summary>
        /// This is the crud SelectMegrendelo method Dunaújváros Count test
        /// </summary>
        [Test]
        public void NonCrudSelectMegrendeloDunaujvarosCount()
        {
            // ARRANGE
            Megrendelo megrendelo = new Megrendelo()
            {
                Varos = "Dunaújváros"
            };

            // ACT
            IEnumerable<Megrendelo> result = (IEnumerable<Megrendelo>)this.logicMegrendelo.NonCrudSelect(megrendelo);

            // ASSERT
            Assert.That(result.Count(), Is.EqualTo(1));
        }

        /// <summary>
        /// This is the crud SelectMegrendelo method zero count test
        /// </summary>
        [Test]
        public void NonCrudSelectMegrendeloZero()
        {
            // ARRANGE
            Megrendelo megrendelo = new Megrendelo();

            // ACT
            IEnumerable<Megrendelo> result = (IEnumerable<Megrendelo>)this.logicMegrendelo.NonCrudSelect(megrendelo);

            // ASSERT
            Assert.That(result.Count(), Is.EqualTo(0));
        }

        /// <summary>
        /// This is the crud SelectMegrendelo method Baracs Count test
        /// </summary>
        [Test]
        public void NonCrudSelectMegrendeloBaracs()
        {
            // ARRANGE
            Megrendelo megrendelo = new Megrendelo()
            {
                Varos = "Baracs"
            };

            // ACT
            Megrendelo result = (Megrendelo)this.logicMegrendelo.NonCrudSelect(megrendelo).First();

            // ASSERT
            Assert.That((result as Megrendelo).Cim, Does.Contain("györgy"));
        }

        // ------------------------------------------------------------------------ NONCRUD SELECT MEGRENDELO END ------------------------------------------------------------------------ //
        // ------------------------------------------------------------------------ UPDATE START ------------------------------------------------------------------------ //

        /// <summary>
        /// This is the update method test
        /// </summary>
        [Test]
        public void Update()
        {
            // ARRANGE
            int[] tomb = this.logicMegrendeles.CountAll(new Megrendeles());
            bool result = false;
            Megrendeles testupdate = new Megrendeles() { RendelesID = 2 };

            if (tomb.Contains(testupdate.RendelesID))
            {
                // ACT
                result = this.logicMegrendeles.Update(testupdate);
            }

            // ASSERT
            Assert.That(result, Is.True);
            this.mockedMegrendeles.Verify(x => x.Update(It.IsAny<Megrendeles>()), Times.Once);
            this.mockedMegrendeles.Verify(x => x.Update(null), Times.Never);
        }

        // ------------------------------------------------------------------------ UPDATE END ------------------------------------------------------------------------ //
        // ------------------------------------------------------------------------ INSERT START ------------------------------------------------------------------------ //

        /// <summary>
        /// This is the Insert method test
        /// </summary>
        [Test]
        public void Insert()
        {
            // ARRANGE
            int[] tomb = this.logicMegrendeles.CountAll(new Megrendeles());
            bool result = false;
            Megrendeles testinsert = new Megrendeles() { RendelesID = 4 };

            if (!tomb.Contains(testinsert.RendelesID))
            {
                // ACT
                result = this.logicMegrendeles.Insert(testinsert);
            }

            // ASSERT
            Assert.That(result, Is.True);
            this.mockedMegrendeles.Verify(x => x.Insert(It.IsAny<Megrendeles>()), Times.Once);
            this.mockedMegrendeles.Verify(x => x.Insert(null), Times.Never);
        }

        // ------------------------------------------------------------------------ INSERT END ------------------------------------------------------------------------ //
        // ------------------------------------------------------------------------ DELETE START ------------------------------------------------------------------------ //

        /// <summary>
        /// This is the Delete method test
        /// </summary>
        [Test]
        public void Delete()
        {
            // ARRANGE
            int[] tomb = this.logicMegrendeles.CountAll(new Megrendeles());
            bool result = false;
            Megrendeles testdelete = new Megrendeles() { RendelesID = 1 };

            if (tomb.Contains(testdelete.RendelesID))
            {
                // ACT
                result = this.logicMegrendeles.Delete(testdelete);
            }

            // ASSERT
            Assert.That(result, Is.True);
            this.mockedMegrendeles.Verify(x => x.Delete(It.IsAny<Megrendeles>()), Times.Once);
            this.mockedMegrendeles.Verify(x => x.Delete(null), Times.Never);
        }

        // ------------------------------------------------------------------------ DELETE END ------------------------------------------------------------------------ //
    }
}
