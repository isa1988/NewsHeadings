using System.Collections.Generic;
using System.ComponentModel;

namespace DataBase.DataModel
{
    /// <summary>
    /// Рубрика
    /// </summary>
    public class HeadingInfo
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Наименование
        /// </summary>
        [DisplayName("Наименование")]
        public string Name { get; set; }
        /// <summary>
        // Путь в адресной строки 
        /// </summary>
        [DisplayName("Путь в адресной строки")]
        public string PathLink { get; set; }
        public List<ArticleInfo> Articles { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}
