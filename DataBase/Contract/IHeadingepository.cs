using DataBase.DataModel;
using System.Collections.Generic;

namespace DataBase.Contract
{
    /// <summary>
    /// Операции с рубликами новостей
    /// </summary>
    public interface IHeadingRepository
    {
        /// <summary>
        /// Добавление новой рублики
        /// </summary>
        /// <param name="name">Наименование</param>
        void Insert(string name);
        /// <summary>
        /// Добавление новой статьи 
        /// </summary>
        void Insert(HeadingInfo heading);

        /// <summary>
        /// Редактирование рублики
        /// </summary>
        /// <param name="id">Индефикатор</param>
        /// <param name="name">Наименование</param>
        void Edit(int id, string name);
        /// <summary>
        /// Редактирование статьи 
        /// </summary>
        void Edit(HeadingInfo heading);
        /// <summary>
        /// Удаление рублики
        /// </summary>
        /// <param name="id">Индефикатор</param>
        void Delete(int id);

        /// <summary>
        /// Получить всех рублик
        /// </summary>        
        List<HeadingInfo> GetAll();
        /// <summary>
        /// Получить статей по определенной рублики
        /// </summary>
        /// <param name="headingID">Ссылка на рублику</param>
        List<ArticleInfo> ArticleByHeading(int headingID);
    }
}
