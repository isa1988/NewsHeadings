using System.Runtime.Serialization.Formatters;
using DataBase.Contract;
using DataBase.DataModel;

namespace DataBase.Working
{
    /// <summary>
    /// Работа с данными
    /// </summary>
    public class DataProvider : IDataProvider
    {
        /// <summary>
        /// Работа с базой
        /// </summary>
        private DataContent dataContent;
        private ArticleWorker article;
        private HeadingWorker heading;

        /// <summary>
        /// Работа с данными 
        /// </summary>
        public DataProvider()
        {
            dataContent = new DataContent();
        }

        /// <summary>
        /// Статья новостей
        /// </summary>
        public IArticleRepository Article
        {
            get { return article ?? (article = new ArticleWorker(dataContent)); }
        }

        /// <summary>
        /// Рубрика
        /// </summary>
        public IHeadingRepository Heading
        {
            get { return heading ?? (heading = new HeadingWorker(dataContent)); }
        }
    }
}
