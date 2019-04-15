using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using DataBase.Contract;
using DataBase.DataModel;

namespace DataBase.Working
{
    /// <summary>
    /// Работа с рубриками
    /// </summary>
    public class HeadingWorker : IHeadingRepository
    {

        private DataContent _dataContent;
        private Heading heading;

        /// <summary>
        /// Работа с рубриками
        /// </summary>
        /// <param name="dataContent">работа с базой</param>
        public HeadingWorker(object dataontent)
        {
            if (dataontent is DataContent) this._dataContent = (DataContent)dataontent;
        }

        /// <summary>
        /// Работа с рубриками
        /// </summary>
        public HeadingWorker()
        {
            _dataContent = new DataContent();
        }

        /// <summary>
        /// Получить статей по определенной рубрики
        /// </summary>
        /// <param name="headingID">Ссылка на рубрику</param>
        /// <returns></returns>
        public List<ArticleInfo> ArticleByHeading(int headingID)
        {
            return _dataContent.Articles.Where(m => m.HeadingID == headingID).Select(x => new ArticleInfo
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
        /// Получить все рубрики
        /// </summary>
        /// <returns></returns>
        public List<HeadingInfo> GetAll()
        {
            return _dataContent.Headings.Select(x => new HeadingInfo
            {
                ID = x.ID,
                Name = x.Name,
                PathLink = x.PathLink,
                //Articles = ArticleByHeading(x.ID)
            }).ToList();
        }

        /// <summary>
        /// Получить объект по Идентификатору
        /// </summary>
        /// <returns></returns>
        /// <param name="id">Идентификатор</param>
        public HeadingInfo GetByID(int id)
        {
            Heading headingTemp = _dataContent.Headings.FirstOrDefault(n => n.ID == id);
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
        /// Получить объект по пути
        /// </summary>
        /// <returns></returns>
        /// <param name="pathLink">Путь</param>
        public HeadingInfo GetByPathLink(string pathLink)
        {
            Heading headingTemp = _dataContent.Headings.FirstOrDefault(n => n.PathLink == pathLink);
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
        /// Добавление новой рубрики
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
        /// Добавление новой рубрики
        /// </summary>
        /// <param name="heading">Модель рубрики</param>
        public void Insert(HeadingInfo heading)
        {
            if (heading == null)
                throw new ArgumentException("Вы не указали объект");
            Insert(heading.Name);
        }

        /// <summary>
        /// Редактирование рубрики
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="name">Наименование</param>
        public void Edit(int id, string name)
        {
            heading = _dataContent.Headings.FirstOrDefault(x => x.ID == id);
            if (heading == null)
                throw new ArgumentException("Не найден объект");
            if (name == null) name = string.Empty;
            string path = GetPathLink(name);
            Check(name, path);
            SetValue(name, path, false);
        }

        /// <summary>
        /// Редактирование рубрики
        /// </summary>
        /// <param name="heading">Модель рубрики</param>
        public void Edit(HeadingInfo heading)
        {
            if (heading == null)
                throw new ArgumentException("Вы не указали объект");
            Edit(heading.ID, heading.Name);
        }

        /// <summary>
        /// Проверка входных данных при создание/редактирование
        /// </summary>
        /// <param name="name">Наименование</param>
        /// <param name="linkPath">Путь</param>
        private void Check(string name, string linkPath)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Не заполнено наименование");
            if (heading == null)
            {
                if (_dataContent.Headings.Any(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
                    throw new ArgumentException("Текущее наименование уже используется");
                if (_dataContent.Headings.Any(x => x.PathLink == linkPath))
                    throw new ArgumentException("Текущий путь уже используется");
            }
            else
            {
                if (_dataContent.Headings.Any(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase) && x.ID != heading.ID))
                    throw new ArgumentException("Текущее наименование уже используется");
                if (_dataContent.Headings.Any(x => x.PathLink == linkPath && x.ID != heading.ID))
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
            if (isNew) _dataContent.Headings.Add(heading);
            _dataContent.SaveChanges();
        }
        /// <summary>
        /// Удаление рубрики 
        /// </summary>
        /// <param name="id">Идентификатор</param>
        public void Delete(int id)
        {
            heading = _dataContent.Headings.FirstOrDefault(x => x.ID == id);
            if (heading == null)
                throw new ArgumentException("Не найден объект");
            List<ArticleInfo> articleList = ArticleByHeading(id);
            if (articleList != null && articleList.Count > 0)
                throw new ArgumentException("В данной рубрике есть статьи");
            _dataContent.Headings.Remove(heading);
            _dataContent.SaveChanges();
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
