using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using DataBase.Contract;
using DataBase.DataModel;

namespace DataBase.Working
{
    public class HeadingWorker : IHeadingRepository
    {

        private MainContent mainContent;
        private Heading heading;

        public HeadingWorker(object mainContent)
        {
            if (mainContent is MainContent) this.mainContent = (MainContent)mainContent;
        }

        public HeadingWorker()
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
        public List<HeadingInfo> GetAll()
        {
            return mainContent.Headings.Select(x => new HeadingInfo
            {
                ID = x.ID,
                Name = x.Name,
                PathLink = x.PathLink,
                //Articles = ArticleByHeading(x.ID)
            }).ToList();
        }

        /// <summary>
        /// Возвратить объект по индефикатору
        /// </summary>
        /// <returns></returns>
        public HeadingInfo GetByID(int id)
        {
            Heading headingTemp = mainContent.Headings.FirstOrDefault(n => n.ID == id);
            if (headingTemp != null)
            {
                return new HeadingInfo
                {
                    ID = headingTemp.ID, 
                    Name = headingTemp.Name,
                    PathLink = headingTemp.PathLink
                };
            }
            return null;
        }

        /// <summary>
        /// Возвратить объект по пути
        /// </summary>
        /// <returns></returns>
        public HeadingInfo GetByPathLink(string pathLink)
        {
            Heading headingTemp = mainContent.Headings.FirstOrDefault(n => n.PathLink == pathLink);
            if (headingTemp != null)
            {
                return new HeadingInfo
                {
                    ID = headingTemp.ID,
                    Name = headingTemp.Name,
                    PathLink = headingTemp.PathLink
                };
            }
            return null;
        }

        /// <summary>
        /// Добавить новую рублику
        /// </summary>
        /// <param name="name">Наименование</param>
        public void Insert(string name)
        {
            if (name == null) name = string.Empty;
            string path = GetPathLink(name);
            Check(name, path);
            SetValue(name, path);
        }

        /// <summary>
        /// Добавить новой рублики
        /// </summary>
        /// <param name="heading"></param>
        public void Insert(HeadingInfo heading)
        {
            if (heading == null)
                throw new ArgumentException("Вы не указали объект");
            Insert(heading.Name);
        }

        /// <summary>
        /// Редактирование рублики
        /// </summary>
        /// <param name="id">Индефикатор</param>
        /// <param name="name">Наименование</param>
        public void Edit(int id, string name)
        {
            heading = mainContent.Headings.FirstOrDefault(x => x.ID == id);
            if (heading == null)
                throw new ArgumentException("Не найден объект");
            if (name == null) name = string.Empty;
            string path = GetPathLink(name);
            Check(name, path);
            SetValue(name, path, false);
        }

        /// <summary>
        /// Редактирование рублики
        /// </summary>
        /// <param name="heading"></param>
        public void Edit(HeadingInfo heading)
        {
            if (heading == null)
                throw new ArgumentException("Вы не указали объект");
            Edit(heading.ID, heading.Name);
        }

        /// <summary>
        /// Проверка
        /// </summary>
        /// <param name="name"></param>
        private void Check(string name, string linkPath)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Не заполнено наименование");
            if (heading == null)
            {
                if (mainContent.Headings.Any(x => x.Name.Trim().ToLower() == name.Trim().ToLower()))
                    throw new ArgumentException("Текущее наименование уже используется");
                if (mainContent.Headings.Any(x => x.PathLink == linkPath))
                    throw new ArgumentException("Текущий путь уже используется");
            }
            else
            {
                if (mainContent.Headings.Any(x => x.Name.Trim().ToLower() == name.Trim().ToLower() && x.ID != heading.ID))
                    throw new ArgumentException("Текущее наименование уже используется");
                if (mainContent.Headings.Any(x => x.PathLink == linkPath && x.ID != heading.ID))
                    throw new ArgumentException("Текущий путь уже используется");
            }
        }
        /// <summary>
        /// Дабавить/редактировать
        /// </summary>
        /// <param name="name">Наимование</param>
        /// <param name="linkPath">Путь в адресной строке</param>
        private void SetValue(string name, string linkPath, bool isNew = true)
        {
            if (isNew) heading = new Heading();
            heading.Name = name.Trim();
            heading.PathLink = linkPath;
            if (isNew) mainContent.Headings.Add(heading);
            mainContent.SaveChanges();
        }
        /// <summary>
        /// Удаление рублики 
        /// </summary>
        /// <param name="id">Индефикатор</param>
        public void Delete(int id)
        {
            heading = mainContent.Headings.FirstOrDefault(x => x.ID == id);
            if (heading == null)
                throw new ArgumentException("Не найден объект");
            List<ArticleInfo> articleList = ArticleByHeading(id);
            if (articleList != null && articleList.Count > 0)
                throw new ArgumentException("В данной рублике есть статьи");
            mainContent.Headings.Remove(heading);
            mainContent.SaveChanges();
        }

        private string GetPathLink(string line)
        {
            line = line.ToLower();
            line = line.Replace("а", "a");
            line = line.Replace("б", "b");
            line = line.Replace("в", "v");
            line = line.Replace("г", "g");
            line = line.Replace("д", "d");
            line = line.Replace("е", "e");
            line = line.Replace("ё", "yo");
            line = line.Replace("ж", "j");
            line = line.Replace("з", "z");
            line = line.Replace("и", "i");
            line = line.Replace("й", "j");
            line = line.Replace("к", "k");
            line = line.Replace("л", "l");
            line = line.Replace("м", "m");
            line = line.Replace("н", "n");
            line = line.Replace("о", "o");
            line = line.Replace("п", "p");
            line = line.Replace("р", "r");
            line = line.Replace("с", "s");
            line = line.Replace("т", "t");
            line = line.Replace("у", "u");
            line = line.Replace("ф", "f");
            line = line.Replace("х", "h");
            line = line.Replace("ц", "c");
            line = line.Replace("ч", "ch");
            line = line.Replace("ш", "sh");
            line = line.Replace("щ", "sch");
            line = line.Replace("ъ", "j");
            line = line.Replace("ы", "i");
            line = line.Replace("ь", "");
            line = line.Replace("э", "e");
            line = line.Replace("ю", "yu");
            line = line.Replace("я", "ya");
            line = line.Replace(" ", "");
            return line;
        }

    }
}
