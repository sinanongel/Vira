namespace Vira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig_addtable_yakittakip : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.YakitTakips",
                c => new
                    {
                        YakitTakipId = c.Int(nullable: false, identity: true),
                        YakitAlimTarihi = c.DateTime(nullable: false),
                        YakitAlimMiktari = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BaslangicKm = c.Decimal(nullable: false, precision: 18, scale: 1),
                        BitisKm = c.Decimal(precision: 18, scale: 1),
                        GidilenKm = c.Decimal(precision: 18, scale: 1),
                        AraclarId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.YakitTakipId)
                .ForeignKey("dbo.Araclars", t => t.AraclarId, cascadeDelete: true)
                .Index(t => t.AraclarId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.YakitTakips", "AraclarId", "dbo.Araclars");
            DropIndex("dbo.YakitTakips", new[] { "AraclarId" });
            DropTable("dbo.YakitTakips");
        }
    }
}
