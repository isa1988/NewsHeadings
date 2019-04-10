
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Hosting;
using DataBase.Contract;
using DataBase.DataModel;
using DataBase.Media;

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
                Author = x.Author,
                DateCreate = x.DateCreate,
                HeadingID = x.HeadingID,
                FileName = x.FileName
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
                Author = x.Author,
                DateCreate = x.DateCreate,
                HeadingID = x.HeadingID,
                FileName = x.FileName
            }).ToList();
        }

        /// <summary>
        /// Возвратить объект по индефикатору
        /// </summary>
        /// <returns></returns>
        public ArticleInfo GetByID(int id)
        {
            Article articleTenp = mainContent.Articles.FirstOrDefault(n => n.ID == id);
            if (articleTenp != null)
            {
                return new ArticleInfo
                {
                    ID = articleTenp.ID,
                    Name = articleTenp.Name,
                    Text = articleTenp.Text,
                    Author = articleTenp.Author,
                    DateCreate = articleTenp.DateCreate,
                    HeadingID = articleTenp.HeadingID,
                    FileName = articleTenp.FileName
                };
            }
            return null;
        }

        /// <summary>
        /// Добавить новой статьи 
        /// </summary>
        /// <param name="name">Наимование</param>
        /// <param name="text">Тест статьи</param>
        /// <param name="autor">Автор</param>
        /// <param name="headingID">Ссылка на рублику</param>
        /// <param name="nameFile">Наимование файла</param>
        /// <param name="file">Файл</param>
        /// <param name="isDeleteFile">Удаление файла при редактирование</param>
        public void Insert(string name, string text, string autor, int headingID,
                           string nameFile, byte[] file, bool isDeleteFile)
        {
            Check(name, text, autor, headingID);
            SetValue(name, text, autor, headingID, nameFile, file, isDeleteFile);
        }

        /// <summary>
        /// Добавить новой статьи 
        /// </summary>
        /// <param name="article"></param>
        public void Insert(ArticleInfo article)
        {
            if (article == null)
                throw new ArgumentException("Вы не указали объект");
            Insert(article.Name, article.Text, article.Author, article.HeadingID, article.FileName, article.File, article.IsDelete);
        }

        /// <summary>
        /// Редактирование статьи
        /// </summary>
        /// <param name="id">Индефикатор</param>
        /// <param name="name">Наимование</param>
        /// <param name="text">Тест статьи</param>
        /// <param name="autor">Автор</param>
        /// <param name="headingID">Ссылка на рублику</param>
        /// <param name="nameFile">Наимование файла</param>
        /// <param name="file">Файл</param>
        /// <param name="isDeleteFile">Удаление файла при редактирование</param>
        public void Edit(int id, string name, string text, string autor, int headingID,
                         string nameFile, byte[] file, bool isDeleteFile)
        {
            article = mainContent.Articles.FirstOrDefault(x => x.ID == id);
            if (article == null)
                throw new ArgumentException("Не найден объект");
            Check(name, text, autor, headingID);
            SetValue(name, text, autor, headingID, nameFile, file, isDeleteFile, false);
        }

        /// <summary>
        /// Редактирование статьи
        /// </summary>
        /// <param name="article"></param>
        public void Edit(ArticleInfo article)
        {
            if (article == null)
                throw new ArgumentException("Вы не указали объект");
            Edit(article.ID, article.Name, article.Text, article.Author, article.HeadingID,
                article.FileName, article.File, article.IsDelete);
        }
        private void Check(string name, string text, string autor, int headingID)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Не заполнено наименование");
            if (string.IsNullOrEmpty(text))
                throw new ArgumentException("Не заполнен текст статьи");
            if (string.IsNullOrEmpty(autor))
                throw new ArgumentException("Не указан автор");
            if (article == null)
            {
                if (mainContent.Articles.Any(x => x.Name.Trim().ToLower() == name.Trim().ToLower() &&
                                                  x.HeadingID == headingID))
                    throw new ArgumentException("Текущее наименование уже используется");
            }
            else
            {
                if (mainContent.Articles.Any(x => x.Name.Trim().ToLower() == name.Trim().ToLower() &&
                                                  x.ID != article.ID &&
                                                  x.HeadingID == headingID))
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
        /// <param name="nameFile">Наимование файла</param>
        /// <param name="file">Файл</param>
        /// <param name="isDeleteFile">Удаление файла при редактирование</param>
        /// <param name="isNew">Новый</param>
        private void SetValue(string name, string text, string autor, int headingID,
            string nameFile, byte[] file, bool isDeleteFile,  bool isNew = true)
        {
            WorkForFiles workForFiles = WorkForFiles.New;
            if (isNew)
            {
                article = new Article();
                article.DateCreate = DateTime.Now;
                workForFiles = WorkForFiles.New;
            }
            else
            {
                workForFiles = WorkForFiles.Edit;
                nameFile = "foto" + article.ID.ToString() + nameFile;
            }
            
            article.Name = name.Trim();
            article.Text = text.Trim();
            article.Author = autor.Trim();
            
            article.HeadingID = headingID;
            if (isNew) mainContent.Articles.Add(article);
            mainContent.SaveChanges();
            if (workForFiles == WorkForFiles.New)
            {
                nameFile = "foto" + article.ID.ToString() + nameFile;
                if (file?.Length > 0)
                    article.FileName = nameFile;
                else
                    article.FileName = string.Empty;
                mainContent.SaveChanges();
                if (file?.Length > 0)
                    WorkForFile(nameFile, string.Empty, file, isDeleteFile, WorkForFiles.New);
            }
            else
            {
                WorkForFile(nameFile, article.FileName, file, isDeleteFile, WorkForFiles.Edit);
                if (file?.Length > 0 && (article.FileName == null || article.FileName == string.Empty))
                {
                    article.FileName = nameFile;
                    mainContent.SaveChanges();
                }
                else if (isDeleteFile)
                {
                    article.FileName = string.Empty;
                    mainContent.SaveChanges();
                }
            }
        }
        /// <summary>
        /// Работа с файлами
        /// </summary>
        /// <param name="nameFile">Наимование файла</param>
        /// <param name="oldName">Предыдущее наименование из БД для удалеия</param>
        /// <param name="file">Файл</param>
        /// <param name="isDelete">Удаление файла при редактирование</param>
        /// <param name="workForFile">Что нужно сделать</para>
        private void WorkForFile(string nameFile, string oldName, byte[] file, bool isDelete, WorkForFiles workForFile)
        {
            Configuration cfg = null;
            if (HttpContext.Current != null)
            {
                cfg =
                    System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
            }
            else
            {
                cfg =
                    ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            }
            MediaFolderConfigSection section = (MediaFolderConfigSection)cfg.GetSection("MediaFolder");
            if (section == null) return;
            string pathDir = section.FolderItems[0].Path;
            pathDir = HostingEnvironment.MapPath(pathDir);

            switch (workForFile)
            {
                case WorkForFiles.New:
                {
                    if (file?.Length > 0)
                    {
                        //File.WriteAllBytes(pathDir + nameFile, file);
                        using (var stream = new FileStream(pathDir + nameFile, FileMode.Create))
                        {
                            stream.Write(file, 0, file.Length);
                        }
                    }

                    break;
                }
                case WorkForFiles.Edit:
                {
                    if (isDelete && oldName?.Length > 0)
                    {
                        File.Delete(pathDir + oldName);
                    }
                    else if (file?.Length > 0 && nameFile?.Length > 0)
                    {
                        if (oldName == null || oldName == string.Empty) oldName = nameFile;
                        //if (oldName?.Length > 0)
                            //File.Delete(pathDir + oldName);
                        File.WriteAllBytes(pathDir + oldName, file);
                    }
                    break;
                }
                case WorkForFiles.Delete:
                {
                    if (oldName?.Length > 0)
                        File.Delete(pathDir + oldName);
                    break;
                }
            }
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
            WorkForFile(string.Empty, article.FileName, null, true, WorkForFiles.Delete);
            mainContent.Articles.Remove(article);
            mainContent.SaveChanges();
        }

    }

    enum WorkForFiles
    {
        New = 0,
        Edit = 1,
        Delete = 2
    }
}
