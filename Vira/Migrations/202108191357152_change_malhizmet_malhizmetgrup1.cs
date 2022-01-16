namespace Vira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change_malhizmet_malhizmetgrup1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MalHizmets", "MalHizmetGrupId", "dbo.MalHizmetGrups");
            DropIndex("dbo.MalHizmets", new[] { "MalHizmetGrupId" });
            AlterColumn("dbo.MalHizmets", "MalHizmetGrupId", c => c.Int(nullable: false));
            CreateIndex("dbo.MalHizmets", "MalHizmetGrupId");
            AddForeignKey("dbo.MalHizmets", "MalHizmetGrupId", "dbo.MalHizmetGrups", "MalHizmetGrupId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MalHizmets", "MalHizmetGrupId", "dbo.MalHizmetGrups");
            DropIndex("dbo.MalHizmets", new[] { "MalHizmetGrupId" });
            AlterColumn("dbo.MalHizmets", "MalHizmetGrupId", c => c.Int());
            CreateIndex("dbo.MalHizmets", "MalHizmetGrupId");
            AddForeignKey("dbo.MalHizmets", "MalHizmetGrupId", "dbo.MalHizmetGrups", "MalHizmetGrupId");
        }
    }
}
