namespace DataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTableArticleAndHeading : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Text = c.String(maxLength: 1000),
                        Autor = c.String(nullable: false, maxLength: 100),
                        DateCreate = c.DateTime(nullable: false),
                        HeadingID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Headings", t => t.HeadingID)
                .Index(t => t.HeadingID);
            
            CreateTable(
                "dbo.Headings",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        PathLink = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Articles", "HeadingID", "dbo.Headings");
            DropIndex("dbo.Articles", new[] { "HeadingID" });
            DropTable("dbo.Headings");
            DropTable("dbo.Articles");
        }
    }
}
