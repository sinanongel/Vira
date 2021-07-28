namespace Vira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig_addTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AltBirims",
                c => new
                    {
                        AltBirimId = c.Int(nullable: false, identity: true),
                        AltBirimAdi = c.String(maxLength: 200),
                        KurumId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AltBirimId)
                .ForeignKey("dbo.Kurums", t => t.KurumId, cascadeDelete: true)
                .Index(t => t.KurumId);
            
            CreateTable(
                "dbo.GelenEvraks",
                c => new
                    {
                        GelenEvrakId = c.Int(nullable: false, identity: true),
                        GelenEvrakSayi = c.String(maxLength: 50),
                        GelenEvrakTarih = c.DateTime(nullable: false),
                        TebligatTarihi = c.DateTime(nullable: false),
                        GelenEvrakKonu = c.String(maxLength: 500),
                        GelenEvrakDurum = c.Boolean(nullable: false),
                        GelenEvrakAciklama = c.String(maxLength: 500),
                        GelenEvrakdosya = c.String(maxLength: 100),
                        IlisikGidenEvrakId = c.Int(),
                        KonuBasligiId = c.Int(nullable: false),
                        FirmaId = c.Int(nullable: false),
                        KurumId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.GelenEvrakId)
                .ForeignKey("dbo.Firmas", t => t.FirmaId, cascadeDelete: true)
                .ForeignKey("dbo.KonuBasligis", t => t.KonuBasligiId, cascadeDelete: true)
                .ForeignKey("dbo.Kurums", t => t.KurumId, cascadeDelete: true)
                .Index(t => t.KonuBasligiId)
                .Index(t => t.FirmaId)
                .Index(t => t.KurumId);
            
            CreateTable(
                "dbo.Firmas",
                c => new
                    {
                        FirmaId = c.Int(nullable: false, identity: true),
                        FirmaAdi = c.String(maxLength: 50),
                        FirmaUnvani = c.String(maxLength: 100),
                        IllerId = c.Int(nullable: false),
                        IlcelerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FirmaId)
                .ForeignKey("dbo.Ilcelers", t => t.IlcelerId, cascadeDelete: true)
                .ForeignKey("dbo.Illers", t => t.IllerId, cascadeDelete: true)
                .Index(t => t.IllerId)
                .Index(t => t.IlcelerId);
            
            CreateTable(
                "dbo.GidenEvraks",
                c => new
                    {
                        GidenEvrakkId = c.Int(nullable: false, identity: true),
                        GidenEvrakSayi = c.String(maxLength: 50),
                        GidenEvrakTarih = c.DateTime(nullable: false),
                        TebligatTarihi = c.DateTime(nullable: false),
                        GidenEvrakKonu = c.String(maxLength: 500),
                        GidenEvrakDurum = c.Boolean(nullable: false),
                        GidenEvrakAciklama = c.String(maxLength: 500),
                        GidenEvrakdosya = c.String(maxLength: 100),
                        IlisikGelenEvrakId = c.Int(),
                        KonuBasligiId = c.Int(nullable: false),
                        FirmaId = c.Int(nullable: false),
                        KurumId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.GidenEvrakkId)
                .ForeignKey("dbo.Firmas", t => t.FirmaId, cascadeDelete: true)
                .ForeignKey("dbo.KonuBasligis", t => t.KonuBasligiId, cascadeDelete: true)
                .ForeignKey("dbo.Kurums", t => t.KurumId, cascadeDelete: true)
                .Index(t => t.KonuBasligiId)
                .Index(t => t.FirmaId)
                .Index(t => t.KurumId);
            
            CreateTable(
                "dbo.KonuBasligis",
                c => new
                    {
                        KonuBasligiId = c.Int(nullable: false, identity: true),
                        BaslikAdi = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.KonuBasligiId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GelenEvraks", "KurumId", "dbo.Kurums");
            DropForeignKey("dbo.Firmas", "IllerId", "dbo.Illers");
            DropForeignKey("dbo.Firmas", "IlcelerId", "dbo.Ilcelers");
            DropForeignKey("dbo.GidenEvraks", "KurumId", "dbo.Kurums");
            DropForeignKey("dbo.GidenEvraks", "KonuBasligiId", "dbo.KonuBasligis");
            DropForeignKey("dbo.GelenEvraks", "KonuBasligiId", "dbo.KonuBasligis");
            DropForeignKey("dbo.GidenEvraks", "FirmaId", "dbo.Firmas");
            DropForeignKey("dbo.GelenEvraks", "FirmaId", "dbo.Firmas");
            DropForeignKey("dbo.AltBirims", "KurumId", "dbo.Kurums");
            DropIndex("dbo.GidenEvraks", new[] { "KurumId" });
            DropIndex("dbo.GidenEvraks", new[] { "FirmaId" });
            DropIndex("dbo.GidenEvraks", new[] { "KonuBasligiId" });
            DropIndex("dbo.Firmas", new[] { "IlcelerId" });
            DropIndex("dbo.Firmas", new[] { "IllerId" });
            DropIndex("dbo.GelenEvraks", new[] { "KurumId" });
            DropIndex("dbo.GelenEvraks", new[] { "FirmaId" });
            DropIndex("dbo.GelenEvraks", new[] { "KonuBasligiId" });
            DropIndex("dbo.AltBirims", new[] { "KurumId" });
            DropTable("dbo.KonuBasligis");
            DropTable("dbo.GidenEvraks");
            DropTable("dbo.Firmas");
            DropTable("dbo.GelenEvraks");
            DropTable("dbo.AltBirims");
        }
    }
}
