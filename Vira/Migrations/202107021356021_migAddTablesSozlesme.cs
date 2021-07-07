namespace Vira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migAddTablesSozlesme : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Dosyas", "FaturaId", "dbo.Faturas");
            DropIndex("dbo.Dosyas", new[] { "FaturaId" });
            CreateTable(
                "dbo.Sozlesmes",
                c => new
                    {
                        SozlesmeId = c.Int(nullable: false, identity: true),
                        SozlesmeKonusu = c.String(maxLength: 100),
                        SozlesmeTarihi = c.DateTime(nullable: false),
                        SozlesmeSuresi = c.Int(nullable: false),
                        SozlesmeBedeli = c.Decimal(nullable: false, precision: 18, scale: 2),
                        KurumId = c.Int(nullable: false),
                        DosyaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SozlesmeId)
                .ForeignKey("dbo.Dosyas", t => t.DosyaId, cascadeDelete: true)
                .ForeignKey("dbo.Kurums", t => t.KurumId, cascadeDelete: true)
                .Index(t => t.KurumId)
                .Index(t => t.DosyaId);
            
            AddColumn("dbo.Faturas", "DosyaId", c => c.Int());
            CreateIndex("dbo.Faturas", "DosyaId");
            AddForeignKey("dbo.Faturas", "DosyaId", "dbo.Dosyas", "DosyaId");
            DropColumn("dbo.Dosyas", "FaturaId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Dosyas", "FaturaId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Sozlesmes", "KurumId", "dbo.Kurums");
            DropForeignKey("dbo.Sozlesmes", "DosyaId", "dbo.Dosyas");
            DropForeignKey("dbo.Faturas", "DosyaId", "dbo.Dosyas");
            DropIndex("dbo.Sozlesmes", new[] { "DosyaId" });
            DropIndex("dbo.Sozlesmes", new[] { "KurumId" });
            DropIndex("dbo.Faturas", new[] { "DosyaId" });
            DropColumn("dbo.Faturas", "DosyaId");
            DropTable("dbo.Sozlesmes");
            CreateIndex("dbo.Dosyas", "FaturaId");
            AddForeignKey("dbo.Dosyas", "FaturaId", "dbo.Faturas", "FaturaId", cascadeDelete: true);
        }
    }
}
