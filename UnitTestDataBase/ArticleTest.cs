using DataBase.DataModel;
using DataBase.Working;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace UnitTestDataBase
{
    /// <summary>
    /// Тестирование блока статей рубрик
    /// </summary>
    [TestClass]
    public class ArticleTest 
    {

        /// <summary>
        /// Тест на проверку наличия в базе статей
        /// </summary>
        [TestMethod]
        public void GetAllTest()
        {
            using (var dataProvider = new DataProvider())
            {
                // запрос несуществующего заголовка возвращает null
                Assert.IsNull(dataProvider.Article.GetByID(-1));
                Assert.IsTrue(dataProvider.Article.GetAll().Count > 0, "В базе нет данных");
            }
        }

        /// <summary>
        /// Тест для возрата статьи по Идентификатору и проверка наименования и рубрики
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="headingID">Ссылка на рубрику</param>
        /// <param name="checkName">Наименвание</param>
        private void GetIDAnCheckNameTest(int id, int headingID, string checkName)
        {
            using (var dataProvider = new DataProvider())
            {
                ArticleInfo articleInfo = dataProvider.Article.GetByID(id);
                Assert.IsNotNull(articleInfo);
                Assert.AreEqual(checkName, articleInfo.Name);
                Assert.AreEqual(headingID, articleInfo.HeadingID);
            }
        }

        /// <summary>
        /// Тест для ректирования данных
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="name">Наимование</param>
        /// <param name="text">Тест статьи</param>
        /// <param name="author">Автор</param>
        /// <param name="headingID">Ссылка на рубрику</param>
        private void EditTest(int id, string name, string text, string author, int headingID)
        {
            using (var dataProvider = new DataProvider())
            {
                dataProvider.Article.Edit(id, name, text, author, headingID, string.Empty, null, false);
                GetIDAnCheckNameTest(id, headingID, name);
            }
        }

        /// <summary>
        /// Тест удаления данных
        /// </summary>
        /// <param name="id">Идентификатор</param>
        private void DeleteTest(int id)
        {
            using (var dataProvider = new DataProvider())
            {
                dataProvider.Article.Delete(id);
                ArticleInfo articleInfo = dataProvider.Article.GetByID(id);
                Assert.IsNull(articleInfo);
            }
        }

        /// <summary>
        /// Тест для проверки общей логики Создание, Получение рубрики по идентификатору, Редактирование и удаление
        /// </summary>
        [TestMethod]
        public void InsertAndGetIDAndGetPathAndEditAndDeleteTest()
        {
            using (var dataProvider = new DataProvider())
            {
                var all = dataProvider.Article.GetAll(); //получение всех статей
                int totalItems = all.Count;
                Assert.IsNotNull(all);
                Assert.IsTrue(all.Count > 0);

                using (var tran = new TransactionScope())
                {
                    //все транзакции выполняются без комита в БД
                    totalItems++;
                    string name = "Тест", text = "Тестовый текст", author = "Админ";
                    int headingID = dataProvider.Heading.Insert("Рубрика"); //Создание рурики в БД
                    //Создание статьи
                    int id = dataProvider.Article.Insert(name, text, author, headingID, string.Empty, null, false);

                    all = dataProvider.Article.GetAll();
                    Assert.IsNotNull(all);
                    Assert.AreEqual(totalItems, all.Count);

                    GetIDAnCheckNameTest(id, headingID, name); //Проверка создалась запись в БД

                    name = "ТестПравка";
                    EditTest(id, name, text, author, headingID); // Редакттирование записи в БД

                    DeleteTest(id); // Удаление записи из БД
                }

                totalItems--;
                all = dataProvider.Article.GetAll();
                Assert.IsNotNull(all);
                Assert.AreEqual(totalItems, all.Count);
            }
        }

        /// <summary>
        /// Тест на невозможность редактирования пустой записи
        /// </summary>
        [TestMethod]
        //[ExpectedException(typeof(ArgumentException))]
        public void EditTestThrowsExceptionIfEmpty()
        {
            using (var dataProvider = new DataProvider())
            {
                ArticleInfo articleInfo = dataProvider.Article.GetByID(15);
                Assert.IsNotNull(articleInfo);
                Assert.ThrowsException<ArgumentException>(() =>
                    dataProvider.Article.Edit(1, "", "", "", 0, "", null, false));
            }
        }
    }
}
