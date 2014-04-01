namespace eForge.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUpdateDatetoUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "LastUpdateDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "LastUpdateDate");
        }
    }
}
