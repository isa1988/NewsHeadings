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
        /// <param name="name">Наимование</param>
        /// <param name="text">Тест статьи</param>
        /// <param name="author">Автор</param>
        /// <param name="headingID">Ссылка на рубрику</param>
        /// <param name="nameFile">Наимование файла</param>
        /// <param name="file">Файл</param>
        /// <param name="isDeleteFile">Удаление файла при редактирование</param>
        int Insert(string name, string text, string author, int headingID, string nameFile, byte[] file, bool isDeleteFile);
        
        /// <summary>
        /// Добавление новой статьи 
        /// </summary>
        /// <param name="article">Модель статьи</param>
        int Insert(ArticleInfo article);

        /// <summary>
        /// Редактирование статьи
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="name">Наимование</param>
        /// <param name="text">Тест статьи</param>
        /// <param name="author">Автор</param>
        /// <param name="headingID">Ссылка на рубрику</param>
        /// <param name="nameFile">Наимование файла</param>
        /// <param name="file">Файл</param>
        /// <param name="isDeleteFile">Удаление файла при редактирование</param>
        void Edit(int id, string name, string text, string author, int headingID, string nameFile, byte[] file, bool isDeleteFile);

        /// <summary>
        /// Редактирование статьи 
        /// </summary>
        /// <param name="article">Модель статьи</param>
        void Edit(ArticleInfo article);
        
        /// <summary>
        /// Удаление статьи
        /// </summary>
        /// <param name="id">Идентификатор</param>
        void Delete(int id);

        /// <summary>
        /// Получить все статьи
        /// </summary>        
        List<ArticleInfo> GetAll();
        /// <summary>
        /// Получить объект по Идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        ArticleInfo GetByID(int id);
        /// <summary>
        /// Получить статей по определенной рубрики
        /// </summary>
        /// <param name="headingID">Ссылка на рубрику</param>
        List<ArticleInfo> ArticleByHeading(int headingID);
    }
}
