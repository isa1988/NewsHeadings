using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Transactions;
using DataBase.DataModel;
using DataBase.Media;
using DataBase.Working;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.Logging;

namespace UnitTestDataBase
{
    /// <summary>
    /// Тестирование блока рубрик
    /// </summary>
    [TestClass]
    public class HeadingTest
    {
        /// <summary>
        /// Строка подключения
        /// </summary>
        string connectionString;

        /// <summary>
        /// Метод используется в качестве конструктора
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            // вставляю в конфигурационный файл абсолютный путь
            // так как база у нас для облегчения тестирования не разворачивается в SQL Server
            // мы должны как то построить путь до нее
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            string conn = config.ConnectionStrings.ConnectionStrings["DataContent"].ConnectionString;
            if (conn.Contains("|DataDirectory|"))
            {
                conn = conn.Replace("|DataDirectory|",
                    Path.Combine(
                        Path.GetDirectoryName(typeof(HeadingTest).Assembly.Location),
                        "App_Data"));
            }

            connectionString = conn;
            config.ConnectionStrings.ConnectionStrings["DataContent"].ConnectionString = conn;
            config.Save(ConfigurationSaveMode.Modified, true);
            ConfigurationManager.RefreshSection("connectionStrings");
        }

        /// <summary>
        /// Тест на проверку наличия в базе рубрик
        /// </summary>
        [TestMethod]
        public void GetAllTest()
        {
            var dataProvider = new DataProvider();
            // запрос несуществующего заголовка возвращает null
            Assert.IsNull(dataProvider.Heading.GetByID(-1));
            Assert.IsTrue(dataProvider.Heading.GetAll().Count > 0, "В базе нет данных");
        }

        /// <summary>
        /// Тест для возрата рубрики по Идентификатору и проверка наименования
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="checkName">Наименвание</param>
        private void GetIDAnCheckNameTest(int id, string checkName)
        {
            var dataProvider = new DataProvider();
            HeadingInfo headingInfo = dataProvider.Heading.GetByID(id);
            Assert.IsNotNull(headingInfo);
            Assert.AreEqual(checkName, headingInfo.Name);
        }

        /// <summary>
        /// Тест для возрата рубрики по Идентификатору и проверка наименования
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="path">Путь строка в линке</param>
        /// <param name="checkName">Наименвание</param>
        private void GetPathLinkAnCheckNameTest(int id, string path, string checkName)
        {
            var dataProvider = new DataProvider();
            HeadingInfo headingInfo = dataProvider.Heading.GetByPathLink(path);
            Assert.IsNotNull(headingInfo);
            Assert.AreEqual(id, headingInfo.ID);
            Assert.AreEqual(checkName, headingInfo.Name);
        }

        /// <summary>
        /// Тест для ректирования данных
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="name">Наименование</param>
        private void EditTest(int id, string name)
        {
            var dataProvider = new DataProvider();
            dataProvider.Heading.Edit(id, name);
            GetIDAnCheckNameTest(id, name);
        }

        /// <summary>
        /// Тест удаления данных
        /// </summary>
        /// <param name="id">Идентификатор</param>
        private void DeleteTest(int id)
        {
            var dataProvider = new DataProvider();
            dataProvider.Heading.Delete(id);
            HeadingInfo headingInfo = dataProvider.Heading.GetByID(id);
            Assert.IsNull(headingInfo);
        }

        /// <summary>
        /// Тест для проверки общей логики Создание, Получение рубрики по идентификатору, Редактирование и удаление
        /// </summary>
        [TestMethod]
        public void InsertAndGetIDAndGetPathAndEditAndDeleteTest()
        {
            var dataProvider = new DataProvider();
            var all = dataProvider.Heading.GetAll();//получение всех рубрик
            int totalItems = all.Count;
            Assert.IsNotNull(all);
            Assert.IsTrue(all.Count > 0);

            using (var tran = new TransactionScope())
            {//все транзакции выполняются без комита в БД
                totalItems++;
                string name = "Тест";
                int id = dataProvider.Heading.Insert(name); //Создание записи в БД
                all = dataProvider.Heading.GetAll();
                Assert.IsNotNull(all);
                Assert.AreEqual(totalItems, all.Count);

                GetIDAnCheckNameTest(id, name);//Проверка создалась запись в БД

                GetPathLinkAnCheckNameTest(id, "Test", name); //Проверка создалась запись по линку

                name = "ТестПравка";
                EditTest(id, name); // Редакттирование записи в БД

                DeleteTest(id); // Удаление записи из БД 
            }

            totalItems--;
            all = dataProvider.Heading.GetAll();
            Assert.IsNotNull(all);
            Assert.AreEqual(totalItems, all.Count);
        }
        
        /// <summary>
        /// Тест на невозможность редактирования пустой записи
        /// </summary>
        [TestMethod]
        //[ExpectedException(typeof(ArgumentException))]
        public void EditTestThrowsExceptionIfEmpty()
        {
            var dataProvider = new DataProvider();
            HeadingInfo headingInfo = dataProvider.Heading.GetByID(1);
            Assert.IsNotNull(headingInfo);
            Assert.ThrowsException<ArgumentException>(() => dataProvider.Heading.Edit(1, ""));
        }
    }
}
