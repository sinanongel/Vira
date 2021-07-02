namespace Vira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig_tableYekdem_addfield : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Yekdems", "AyId", c => c.Int(nullable: false));
            AddColumn("dbo.Yekdems", "YillarId", c => c.Int(nullable: false));
            CreateIndex("dbo.Yekdems", "AyId");
            CreateIndex("dbo.Yekdems", "YillarId");
            AddForeignKey("dbo.Yekdems", "AyId", "dbo.Ays", "AyId", cascadeDelete: true);
            AddForeignKey("dbo.Yekdems", "YillarId", "dbo.Yillars", "YillarId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Yekdems", "YillarId", "dbo.Yillars");
            DropForeignKey("dbo.Yekdems", "AyId", "dbo.Ays");
            DropIndex("dbo.Yekdems", new[] { "YillarId" });
            DropIndex("dbo.Yekdems", new[] { "AyId" });
            DropColumn("dbo.Yekdems", "YillarId");
            DropColumn("dbo.Yekdems", "AyId");
        }
    }
}
