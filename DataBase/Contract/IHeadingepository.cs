using DataBase.DataModel;
using System.Collections.Generic;

namespace DataBase.Contract
{
    /// <summary>
    /// Операции с рубриками новостей
    /// </summary>
    public interface IHeadingRepository
    {
        /// <summary>
        /// Добавление новой рубрики
        /// </summary>
        /// <param name="name">Наименование</param>
        void Insert(string name);
        /// <summary>
        /// Добавление новой статьи 
        /// </summary>
        /// <param name="heading">Модель рубрики</param>
        void Insert(HeadingInfo heading);

        /// <summary>
        /// Редактирование рубрики
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="name">Наименование</param>
        void Edit(int id, string name);
        /// <summary>
        /// Редактирование статьи 
        /// </summary>
        /// <param name="heading">Модель рубрики</param>
        void Edit(HeadingInfo heading);
        /// <summary>
        /// Удаление рубрики
        /// </summary>
        /// <param name="id">Идентификатор</param>
        void Delete(int id);

        /// <summary>
        /// Получить все рубрики
        /// </summary>        
        List<HeadingInfo> GetAll();
        /// <summary>
        /// Получить объект по Идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        HeadingInfo GetByID(int id);
        /// <summary>
        /// Получить объект по пути
        /// </summary>
        /// <param name="pathLink">Путь</param>
        HeadingInfo GetByPathLink(string pathLink);
        
        /// <summary>
        /// Получить статей по определенной рубрики
        /// </summary>
        /// <param name="headingID">Ссылка на рубрику</param>
        List<ArticleInfo> ArticleByHeading(int headingID);
    }
}
