using DataBase.DataConfiguration;
using System.Data.Entity;

namespace DataBase.DataModel
{
    /// <summary>
    /// работа с базой
    /// </summary>
    sealed class DataContent : DbContext
    {
        /// <summary>
        /// Рубрики
        /// </summary>
        public DbSet<Heading> Headings { get; set; }
        /// <summary>
        /// Статьи новостей
        /// </summary>
        public DbSet<Article> Articles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new HeadingConfiguration());
            modelBuilder.Configurations.Add(new ArticleConfiguration());

        }
    }
}
