using System.Data.Entity.ModelConfiguration;
using DataBase.DataModel;

namespace DataBase.DataConfiguration
{
    class ArticleConfiguration : EntityTypeConfiguration<Article>
    {
        public ArticleConfiguration()
        {
            HasKey(x => x.ID);
            Property(x => x.Name).HasMaxLength(100).IsRequired();
            Property(x => x.Autor).HasMaxLength(100).IsRequired();
            Property(x => x.Text).HasMaxLength(1000);
            Property(x => x.DateCreate).IsRequired();

            HasRequired(x => x.HeadingCurr).WithMany(x => x.Articles).
                               HasForeignKey(x => x.HeadingID).WillCascadeOnDelete(false);
        }
    }
}
