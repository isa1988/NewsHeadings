
namespace DataBase.Contract
{
    public interface IDataProvider
    {
        /// <summary>
        /// Статьи новостей
        /// </summary>
        IArticleRepository Article { get; }

        /// <summary>
        /// Рублики новостей
        /// </summary>
        IHeadingRepository Heading { get; }
    }
}
