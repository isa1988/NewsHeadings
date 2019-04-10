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
        void Insert(HeadingInfo heading);

        /// <summary>
        /// Редактирование рубрики
        /// </summary>
        /// <param name="id">Индефикатор</param>
        /// <param name="name">Наименование</param>
        void Edit(int id, string name);
        /// <summary>
        /// Редактирование статьи 
        /// </summary>
        void Edit(HeadingInfo heading);
        /// <summary>
        /// Удаление рубрики
        /// </summary>
        /// <param name="id">Индефикатор</param>
        void Delete(int id);

        /// <summary>
        /// Получить всех рубрик
        /// </summary>        
        List<HeadingInfo> GetAll();
        /// <summary>
        /// Получить объекта по индефикатору
        /// </summary>        
        HeadingInfo GetByID(int id);
        /// <summary>
        /// Получить объекта по пути
        /// </summary>        
        HeadingInfo GetByPathLink(string pathLink);
        
        /// <summary>
        /// Получить статей по определенной рубрики
        /// </summary>
        /// <param name="headingID">Ссылка на рубрику</param>
        List<ArticleInfo> ArticleByHeading(int headingID);
    }
}
