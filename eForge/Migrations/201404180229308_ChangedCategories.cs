namespace eForge.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedCategories : DbMigration
    {
        public override void Up()
        {
            //DropForeignKey("dbo.BlogEntryCategoryBlogEntries", "FK_dbo.BlogEntryCategoryBlogEntries_dbo.BlogEntries_BlogEntry_BlogEntryId", "dbo.BlogEntryCategories");
            //DropForeignKey("dbo.BlogEntryCategoryBlogEntries", "FK_dbo.BlogEntryCategoryBlogEntries_dbo.BlogEntryCategories_BlogEntryCategory_BlogEntryCategoryId", "dbo.BlogEntryCategories");
            DropForeignKey("dbo.BlogEntryCategoryBlogEntries", "BlogEntryCategory_BlogEntryCategoryId", "dbo.BlogEntryCategories");
            DropForeignKey("dbo.BlogEntryCategoryBlogEntries", "BlogEntry_BlogEntryId", "dbo.BlogEntries");
            DropIndex("dbo.BlogEntryCategoryBlogEntries", new[] { "BlogEntryCategory_BlogEntryCategoryId" });
            DropIndex("dbo.BlogEntryCategoryBlogEntries", new[] { "BlogEntry_BlogEntryId" });
            AddColumn("dbo.BlogEntries", "BlogEntryCategoryId", c => c.Int(nullable: false));
            CreateIndex("dbo.BlogEntries", "BlogEntryCategoryId");
            Sql("update dbo.BlogEntries set BlogEntryCategoryId = (select top 1 blogentrycategoryid from dbo.blogentrycategories)");
            AddForeignKey("dbo.BlogEntries", "BlogEntryCategoryId", "dbo.BlogEntryCategories", "BlogEntryCategoryId", cascadeDelete: true);
            DropTable("dbo.BlogEntryCategoryBlogEntries");

        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.BlogEntryCategoryBlogEntries",
                c => new
                    {
                        BlogEntryCategory_BlogEntryCategoryId = c.Int(nullable: false),
                        BlogEntry_BlogEntryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.BlogEntryCategory_BlogEntryCategoryId, t.BlogEntry_BlogEntryId });
            
            DropForeignKey("dbo.BlogEntries", "BlogEntryCategoryId", "dbo.BlogEntryCategories");
            DropIndex("dbo.BlogEntries", new[] { "BlogEntryCategoryId" });
            DropColumn("dbo.BlogEntries", "BlogEntryCategoryId");
            CreateIndex("dbo.BlogEntryCategoryBlogEntries", "BlogEntry_BlogEntryId");
            CreateIndex("dbo.BlogEntryCategoryBlogEntries", "BlogEntryCategory_BlogEntryCategoryId");
            AddForeignKey("dbo.BlogEntryCategoryBlogEntries", "BlogEntry_BlogEntryId", "dbo.BlogEntries", "BlogEntryId", cascadeDelete: true);
            AddForeignKey("dbo.BlogEntryCategoryBlogEntries", "BlogEntryCategory_BlogEntryCategoryId", "dbo.BlogEntryCategories", "BlogEntryCategoryId", cascadeDelete: true);
        }
    }
}
