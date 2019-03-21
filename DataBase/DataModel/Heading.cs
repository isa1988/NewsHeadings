using System.Collections.Generic;
using System.ComponentModel;

namespace DataBase.DataModel
{
    /// <summary>
    /// Рублика yjdjcntq
    /// </summary>
    class Heading
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
        // Путь в адресной строки 
        /// </summary>
        [DisplayName("Путь в адресной строки")]
        public string PathLink { get; set; }
        public virtual ICollection<Article> Articles { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}
