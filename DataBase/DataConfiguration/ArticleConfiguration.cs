using System.Data.Entity.ModelConfiguration;
using DataBase.DataModel;

namespace DataBase.DataConfiguration
{
    /// <summary>
    /// Структура таблицы статьи 
    /// </summary>
    class ArticleConfiguration : EntityTypeConfiguration<Article>
    {
        public ArticleConfiguration()
        {
            HasKey(x => x.ID);
            Property(x => x.Name).HasMaxLength(100).IsRequired();
            Property(x => x.Author).HasMaxLength(100).IsRequired();
            Property(x => x.Text).HasMaxLength(1000);
            Property(x => x.DateCreate).IsRequired();
            Property(x => x.FileName).HasMaxLength(100);

            HasRequired(x => x.HeadingCurr).WithMany(x => x.Articles).
                               HasForeignKey(x => x.HeadingID).WillCascadeOnDelete(false);
        }
    }
}
