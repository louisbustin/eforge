namespace eForge.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedLinkText : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BlogEntries", "LinkText", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.BlogEntries", "LinkText");
        }
    }
}
