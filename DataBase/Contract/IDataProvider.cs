
namespace DataBase.Contract
{
    /// <summary>
    /// Операции с данными
    /// </summary>
    public interface IDataProvider
    {
        /// <summary>
        /// Статьи новостей
        /// </summary>
        IArticleRepository Article { get; }

        /// <summary>
        /// Рубрики новостей
        /// </summary>
        IHeadingRepository Heading { get; }
    }
}
