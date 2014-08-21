namespace eForge.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingImages : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        ImageId = c.Int(nullable: false, identity: true),
                        ImageData = c.Binary(),
                        Name = c.String(),
                        AltText = c.String(),
                        ImageGuid = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ImageId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Images");
        }
    }
}
