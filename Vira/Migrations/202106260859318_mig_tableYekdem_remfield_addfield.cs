namespace Vira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig_tableYekdem_remfield_addfield : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Yekdems", "AyId", "dbo.Ays");
            DropForeignKey("dbo.Yekdems", "YillarId", "dbo.Yillars");
            DropIndex("dbo.Yekdems", new[] { "AyId" });
            DropIndex("dbo.Yekdems", new[] { "YillarId" });
            AddColumn("dbo.Yekdems", "DonemTarih", c => c.DateTime());
            DropColumn("dbo.Yekdems", "AyId");
            DropColumn("dbo.Yekdems", "YillarId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Yekdems", "YillarId", c => c.Int(nullable: false));
            AddColumn("dbo.Yekdems", "AyId", c => c.Int(nullable: false));
            DropColumn("dbo.Yekdems", "DonemTarih");
            CreateIndex("dbo.Yekdems", "YillarId");
            CreateIndex("dbo.Yekdems", "AyId");
            AddForeignKey("dbo.Yekdems", "YillarId", "dbo.Yillars", "YillarId", cascadeDelete: true);
            AddForeignKey("dbo.Yekdems", "AyId", "dbo.Ays", "AyId", cascadeDelete: true);
        }
    }
}
