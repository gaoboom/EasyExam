namespace EasyExam.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Administrators",
                c => new
                    {
                        AdministratorID = c.Int(nullable: false, identity: true),
                        Accounts = c.String(nullable: false, maxLength: 30),
                        Password = c.String(nullable: false, maxLength: 256),
                        LoginIP = c.String(),
                        LoginTime = c.DateTime(),
                        CreateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.AdministratorID);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        ParentID = c.Int(nullable: false),
                        Description = c.String(maxLength: 1000),
                        Order = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryID);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        QuestionID = c.Int(nullable: false, identity: true),
                        QuestionType = c.String(nullable: false),
                        QuestionTitle = c.String(maxLength: 200),
                        OptionA = c.String(),
                        OptionB = c.String(),
                        OptionC = c.String(),
                        OptionD = c.String(),
                        RightAnswer = c.String(),
                        Note = c.String(),
                        Category_CategoryID = c.Int(),
                    })
                .PrimaryKey(t => t.QuestionID)
                .ForeignKey("dbo.Categories", t => t.Category_CategoryID)
                .Index(t => t.Category_CategoryID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false, maxLength: 256),
                        Name = c.String(maxLength: 20),
                        Company = c.String(maxLength: 20),
                        Department = c.String(maxLength: 20),
                        LastLoginTime = c.DateTime(),
                        LastLoginIP = c.String(),
                    })
                .PrimaryKey(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Questions", "Category_CategoryID", "dbo.Categories");
            DropIndex("dbo.Questions", new[] { "Category_CategoryID" });
            DropTable("dbo.Users");
            DropTable("dbo.Questions");
            DropTable("dbo.Categories");
            DropTable("dbo.Administrators");
        }
    }
}
