
using System;

namespace DataBase.Contract
{
    /// <summary>
    /// Операции с данными
    /// </summary>
    public interface IDataProvider : IDisposable
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
