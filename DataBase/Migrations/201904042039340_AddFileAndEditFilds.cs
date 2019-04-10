namespace DataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFileAndEditFilds : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "Author", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.Articles", "FileName", c => c.String(maxLength: 100));
            DropColumn("dbo.Articles", "Autor");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Articles", "Autor", c => c.String(nullable: false, maxLength: 100));
            DropColumn("dbo.Articles", "FileName");
            DropColumn("dbo.Articles", "Author");
        }
    }
}
