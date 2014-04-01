namespace eForge.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdddatestoBlogEntryCategory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BlogEntryCategories", "CreateDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.BlogEntryCategories", "LastUpdatedDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BlogEntryCategories", "LastUpdatedDate");
            DropColumn("dbo.BlogEntryCategories", "CreateDate");
        }
    }
}
