namespace eForge.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BlogEntries",
                c => new
                    {
                        BlogEntryId = c.Int(nullable: false, identity: true),
                        Body = c.String(),
                        LastModifiedDate = c.DateTime(nullable: false),
                        PublicationDate = c.DateTime(),
                        Subject = c.String(),
                        Summary = c.String(),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BlogEntryId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        EmailAddress = c.String(),
                        CanPublish = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.BlogEntryCategories",
                c => new
                    {
                        BlogEntryCategoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.BlogEntryCategoryId);
            
            CreateTable(
                "dbo.BlogEntryCategoryBlogEntries",
                c => new
                    {
                        BlogEntryCategory_BlogEntryCategoryId = c.Int(nullable: false),
                        BlogEntry_BlogEntryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.BlogEntryCategory_BlogEntryCategoryId, t.BlogEntry_BlogEntryId })
                .ForeignKey("dbo.BlogEntryCategories", t => t.BlogEntryCategory_BlogEntryCategoryId, cascadeDelete: true)
                .ForeignKey("dbo.BlogEntries", t => t.BlogEntry_BlogEntryId, cascadeDelete: true)
                .Index(t => t.BlogEntryCategory_BlogEntryCategoryId)
                .Index(t => t.BlogEntry_BlogEntryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BlogEntryCategoryBlogEntries", "BlogEntry_BlogEntryId", "dbo.BlogEntries");
            DropForeignKey("dbo.BlogEntryCategoryBlogEntries", "BlogEntryCategory_BlogEntryCategoryId", "dbo.BlogEntryCategories");
            DropForeignKey("dbo.BlogEntries", "UserId", "dbo.Users");
            DropIndex("dbo.BlogEntryCategoryBlogEntries", new[] { "BlogEntry_BlogEntryId" });
            DropIndex("dbo.BlogEntryCategoryBlogEntries", new[] { "BlogEntryCategory_BlogEntryCategoryId" });
            DropIndex("dbo.BlogEntries", new[] { "UserId" });
            DropTable("dbo.BlogEntryCategoryBlogEntries");
            DropTable("dbo.BlogEntryCategories");
            DropTable("dbo.Users");
            DropTable("dbo.BlogEntries");
        }
    }
}
