using DataBase.DataConfiguration;
using System.Data.Entity;

namespace DataBase.DataModel
{
    
    sealed class MainContent : DbContext
    {
        public DbSet<Heading> Headings { get; set; }
        public DbSet<Article> Articles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new HeadingConfiguration());
            modelBuilder.Configurations.Add(new ArticleConfiguration());

        }
    }
}
