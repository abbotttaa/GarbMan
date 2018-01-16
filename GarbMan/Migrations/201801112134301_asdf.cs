namespace GarbMan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class asdf : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ZipChecks",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Zip = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ZipChecks");
        }
    }
}
