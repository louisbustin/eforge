namespace eForge.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addingcontenttypetoimage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Images", "FileName", c => c.String());
            AddColumn("dbo.Images", "ContentType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Images", "ContentType");
            DropColumn("dbo.Images", "FileName");
        }
    }
}
