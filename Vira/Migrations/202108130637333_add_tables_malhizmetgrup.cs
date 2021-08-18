namespace Vira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_tables_malhizmetgrup : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MalHizmetGrups",
                c => new
                    {
                        MalHizmetGrupId = c.Int(nullable: false, identity: true),
                        MalHizmetGrupAdi = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.MalHizmetGrupId);
            
            AddColumn("dbo.MalHizmets", "MalHizmetGrupId", c => c.Int());
            CreateIndex("dbo.MalHizmets", "MalHizmetGrupId");
            AddForeignKey("dbo.MalHizmets", "MalHizmetGrupId", "dbo.MalHizmetGrups", "MalHizmetGrupId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MalHizmets", "MalHizmetGrupId", "dbo.MalHizmetGrups");
            DropIndex("dbo.MalHizmets", new[] { "MalHizmetGrupId" });
            DropColumn("dbo.MalHizmets", "MalHizmetGrupId");
            DropTable("dbo.MalHizmetGrups");
        }
    }
}
