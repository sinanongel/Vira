namespace Vira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig_addtable_yekdem : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Faturas", "AyId", "dbo.Ays");
            DropForeignKey("dbo.Faturas", "YillarId", "dbo.Yillars");
            DropIndex("dbo.Faturas", new[] { "AyId" });
            DropIndex("dbo.Faturas", new[] { "YillarId" });
            CreateTable(
                "dbo.Yekdems",
                c => new
                    {
                        YekdemId = c.Int(nullable: false, identity: true),
                        YekdemTutar = c.Decimal(nullable: false, precision: 18, scale: 2),
                        YekdemKur = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AyId = c.Int(nullable: false),
                        YillarId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.YekdemId)
                .ForeignKey("dbo.Ays", t => t.AyId, cascadeDelete: true)
                .ForeignKey("dbo.Yillars", t => t.YillarId, cascadeDelete: true)
                .Index(t => t.AyId)
                .Index(t => t.YillarId);
            
            AlterColumn("dbo.Faturas", "AyId", c => c.Int(nullable: false));
            AlterColumn("dbo.Faturas", "YillarId", c => c.Int(nullable: false));
            CreateIndex("dbo.Faturas", "AyId");
            CreateIndex("dbo.Faturas", "YillarId");
            AddForeignKey("dbo.Faturas", "AyId", "dbo.Ays", "AyId", cascadeDelete: true);
            AddForeignKey("dbo.Faturas", "YillarId", "dbo.Yillars", "YillarId", cascadeDelete: true);
            DropColumn("dbo.Faturas", "FaturaDonemi");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Faturas", "FaturaDonemi", c => c.String(maxLength: 12));
            DropForeignKey("dbo.Faturas", "YillarId", "dbo.Yillars");
            DropForeignKey("dbo.Faturas", "AyId", "dbo.Ays");
            DropForeignKey("dbo.Yekdems", "YillarId", "dbo.Yillars");
            DropForeignKey("dbo.Yekdems", "AyId", "dbo.Ays");
            DropIndex("dbo.Yekdems", new[] { "YillarId" });
            DropIndex("dbo.Yekdems", new[] { "AyId" });
            DropIndex("dbo.Faturas", new[] { "YillarId" });
            DropIndex("dbo.Faturas", new[] { "AyId" });
            AlterColumn("dbo.Faturas", "YillarId", c => c.Int());
            AlterColumn("dbo.Faturas", "AyId", c => c.Int());
            DropTable("dbo.Yekdems");
            CreateIndex("dbo.Faturas", "YillarId");
            CreateIndex("dbo.Faturas", "AyId");
            AddForeignKey("dbo.Faturas", "YillarId", "dbo.Yillars", "YillarId");
            AddForeignKey("dbo.Faturas", "AyId", "dbo.Ays", "AyId");
        }
    }
}
