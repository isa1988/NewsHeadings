
using System;
using System.Collections.Generic;
using System.Linq;
using DataBase.Contract;
using DataBase.DataModel;

namespace DataBase.Working
{
    /// <summary>
    /// работа с статьями 
    /// </summary>
    public class ArticleWorker : IArticleRepository
    {
        private MainContent mainContent;
        private Article article;
        public ArticleWorker(object mainContent)
        {
            if (mainContent is MainContent) this.mainContent = (MainContent)mainContent;
        }
        public ArticleWorker()
        {
            mainContent = new MainContent();
        }
        /// <summary>
        /// Вернуть все статьи по рублики
        /// </summary>
        /// <param name="headingID">Ссылка на рублику</param>
        /// <returns></returns>
        public List<ArticleInfo> ArticleByHeading(int headingID)
        {
            return mainContent.Articles.Where(m => m.HeadingID == headingID).Select(x => new ArticleInfo
            {
                ID = x.ID,
                Name = x.Name,
                Text = x.Text,
                Autor = x.Autor,
                DateCreate = x.DateCreate,
                HeadingID = x.HeadingID,
            }).ToList();
        }
        
        /// <summary>
        /// Вернуть все статьи
        /// </summary>
        /// <returns></returns>
        public List<ArticleInfo> GetAll()
        {
            return mainContent.Articles.Select(x => new ArticleInfo
            {
                ID = x.ID,
                Name = x.Name,
                Text  = x.Text,
                Autor = x.Autor,
                DateCreate = x.DateCreate,
                HeadingID = x.HeadingID, 
            }).ToList();
        }
        
        /// <summary>
        /// Добавить новой статьи 
        /// </summary>
        /// <param name="name">Наимование</param>
        /// <param name="text">Тест статьи</param>
        /// <param name="autor">Автор</param>
        /// <param name="headingID">Ссылка на рублику</param>
        public void Insert(string name, string text, string autor, int headingID)
        {
            Check(name, text, autor);
            SetValue(name, text, autor, headingID);
        }

        /// <summary>
        /// Добавить новой статьи 
        /// </summary>
        /// <param name="article"></param>
        public void Insert(ArticleInfo article)
        {
            if (article == null)
                throw new ArgumentException("Вы не указали объект");
            Insert(article.Name, article.Text, article.Autor, article.HeadingID);
        }

        /// <summary>
        /// Редактирование статьи
        /// </summary>
        /// <param name="id">Индефикатор</param>
        /// <param name="name">Наимование</param>
        /// <param name="text">Тест статьи</param>
        /// <param name="autor">Автор</param>
        /// <param name="headingID">Ссылка на рублику</param>
        public void Edit(int id, string name, string text, string autor, int headingID)
        {
            article = mainContent.Articles.FirstOrDefault(x => x.ID == id);
            if (article == null)
                throw new ArgumentException("Не найден объект");
            Check(name, text, autor);
            SetValue(name, text, autor, headingID, false);
        }

        /// <summary>
        /// Редактирование статьи
        /// </summary>
        /// <param name="article"></param>
        public void Edit(ArticleInfo article)
        {
            if (article == null)
                throw new ArgumentException("Вы не указали объект");
            Edit(article.ID, article.Name, article.Text, article.Autor, article.HeadingID);
        }
        private void Check(string name, string text, string autor)
        {
            if (name.Trim() == string.Empty)
                throw new ArgumentException("Не заполнено наименование");
            if (text.Trim() == string.Empty)
                throw new ArgumentException("Не заполнен текст статьи");
            if (autor.Trim() == string.Empty)
                throw new ArgumentException("Не указан автор");
            if (article == null)
            {
                if (mainContent.Articles.Any(x => x.Name.Trim().ToLower() == name.Trim().ToLower()))
                    throw new ArgumentException("Текущее наименование уже используется");
            }
            else
            {
                if (mainContent.Articles.Any(x => x.Name.Trim().ToLower() == name.Trim().ToLower() && x.ID != article.ID))
                    throw new ArgumentException("Текущее наименование уже используется");
            }
        }
        /// <summary>
        /// Дабавить/редактировать
        /// </summary>
        /// <param name="name">Наимование</param>
        /// <param name="text">Тест статьи</param>
        /// <param name="autor">Автор</param>
        /// <param name="headingID">Ссылка на рублику</param>
        /// <param name="isNew">Новый</param>
        private void SetValue(string name, string text, string autor, int headingID, bool isNew = true)
        {
            if (isNew)
            {
                article = new Article();
                article.DateCreate = DateTime.Now;
            }
            article.Name = name.Trim();
            article.Text = text.Trim();
            article.Autor = autor.Trim();
            article.HeadingID = headingID;
            if (isNew) mainContent.Articles.Add(article);
            mainContent.SaveChanges();
        }
        /// <summary>
        /// Удалить статью
        /// </summary>
        /// <param name="id">Индефикатоп</param>
        public void Delete(int id)
        {
            article = mainContent.Articles.FirstOrDefault(x => x.ID == id);
            if (article == null)
                throw new ArgumentException("Не найден объект");
            mainContent.Articles.Remove(article);
            mainContent.SaveChanges();
        }

    }
}
