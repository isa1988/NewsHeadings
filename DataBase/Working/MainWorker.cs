using System.Runtime.Serialization.Formatters;
using DataBase.Contract;
using DataBase.DataModel;

namespace DataBase.Working
{
    public class MainWorker : IDataProvider
    {
        private MainContent mainContent;
        private ArticleWorker article;
        private HeadingWorker heading;

        public MainWorker()
        {
            mainContent = new MainContent();
        }

        public IArticleRepository Article
        {
            get { return article ?? (article = new ArticleWorker(mainContent)); }
        }

        public IHeadingRepository Heading
        {
            get { return heading ?? (heading = new HeadingWorker(mainContent)); }
        }
    }
}
