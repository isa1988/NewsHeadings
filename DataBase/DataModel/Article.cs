using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace DataBase.DataModel
{
    /// <summary>
    /// Статьи новостей 
    /// </summary>
    class Article
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
        public string Autor { get; set; }
        /// <summary>
        /// Дата создания
        /// </summary>
        [DisplayName("Дата создания")]
        public DateTime DateCreate { get; set; }

        public virtual Heading HeadingCurr { get; set; }
        public int HeadingID { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}
