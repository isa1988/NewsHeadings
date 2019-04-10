using System;
using System.ComponentModel;

namespace DataBase.DataModel
{
    /// <summary>
    /// Статьи новостей 
    /// </summary>
    public class ArticleInfo
    {
        /// <summary>
        /// Индефикатор
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Наименование
        /// </summary>
        [DisplayName("Наименование")]
        public string Name { get; set; }

        /// <summary>
        /// Текст статьи
        /// </summary>
        [DisplayName("Текст")]
        public string Text { get; set; }
        /// <summary>
        /// Автор
        /// </summary>
        [DisplayName("Автор")]
        public string Author { get; set; }
        /// <summary>
        /// Дата создания
        /// </summary>
        [DisplayName("Дата создания")]
        public DateTime DateCreate { get; set; }

        /// <summary>
        /// Наименование файла
        /// </summary>
        [DisplayName("Наименование файла")]
        public string FileName { get; set; }

        /// <summary>
        /// Удалить файл
        /// </summary>
        public bool IsDelete { get; set; }

        /// <summary>
        /// Файл
        /// </summary>
        public byte[] File { get; set; }

        [DisplayName("Рублика")]
        public int HeadingID { get; set; }
        public HeadingInfo HeadingCurr { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}
