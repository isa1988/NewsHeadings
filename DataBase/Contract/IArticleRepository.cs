using DataBase.DataModel;
using System.Collections.Generic;

namespace DataBase.Contract
{
    /// <summary>
    /// Операции с Статьями новостей
    /// </summary>
    public interface IArticleRepository
    {
        /// <summary>
        /// Добавление новой статьи 
        /// </summary>
        /// <param name="name">Наименование</param>
        /// <param name="text">Текст</param>
        /// <param name="autor">Автор</param>
        /// <param name="headingID">Ссылка на рублику</param>
        void Insert(string name, string text, string autor, int headingID, string nameFile, byte[] file, bool isDeleteFile);
        /// <summary>
        /// Добавление новой статьи 
        /// </summary>
        void Insert(ArticleInfo article);

        /// <summary>
        /// Редактирование статьи
        /// </summary>
        /// <param name="id">Индефикатор</param>
        /// <param name="name">Наименование</param>
        /// <param name="text">Текст</param>
        /// <param name="autor">Автор</param>
        /// <param name="headingID">Ссылка на рублику</param>
        void Edit(int id, string name, string text, string autor, int headingID, string nameFile, byte[] file, bool isDeleteFile);
        /// <summary>
        /// Редактирование статьи 
        /// </summary>
        void Edit(ArticleInfo article);
        /// <summary>
        /// Удаление статьи
        /// </summary>
        /// <param name="id">Индефикатор</param>
        void Delete(int id);

        /// <summary>
        /// Получить всех статей
        /// </summary>        
        List<ArticleInfo> GetAll();
        /// <summary>
        /// Получить объекта по индефикатору
        /// </summary>        
        ArticleInfo GetByID(int id);
        /// <summary>
        /// Получить статей по определенной рублики
        /// </summary>
        /// <param name="headingID">Ссылка на рублику</param>
        List<ArticleInfo> ArticleByHeading(int headingID);
    }
}
