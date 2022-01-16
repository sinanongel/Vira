namespace Vira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_table_saatlikuretim : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SaatlikUretims",
                c => new
                    {
                        SaatlikUretimId = c.Int(nullable: false, identity: true),
                        SaatlikUretimTarih = c.DateTime(nullable: false),
                        BasSaat = c.String(),
                        BitSaat = c.String(),
                        SuYukseklik = c.Int(),
                        Sepam1IlkEndeks = c.Decimal(precision: 18, scale: 1),
                        Sepam1SonEndeks = c.Decimal(precision: 18, scale: 1),
                        Sepam1TopUretim = c.Decimal(precision: 18, scale: 1),
                        Sepam2IlkEndeks = c.Decimal(precision: 18, scale: 1),
                        Sepam2SonEndeks = c.Decimal(precision: 18, scale: 1),
                        Sepam2TopUretim = c.Decimal(precision: 18, scale: 1),
                        Tug1Tug2TopUretim = c.Decimal(precision: 18, scale: 2),
                        Uni1IlkEndeks = c.Decimal(precision: 18, scale: 1),
                        Uni1SonEndeks = c.Decimal(precision: 18, scale: 1),
                        Uni1Uretim = c.Decimal(precision: 18, scale: 1),
                        Uni2IlkEndeks = c.Decimal(precision: 18, scale: 1),
                        Uni2SonEndeks = c.Decimal(precision: 18, scale: 1),
                        Uni2Uretim = c.Decimal(precision: 18, scale: 1),
                        Tug1TopUretim = c.Decimal(precision: 18, scale: 1),
                        Tug2TopUretim = c.Decimal(precision: 18, scale: 1),
                        AnlikMaksGuc = c.Decimal(precision: 18, scale: 1),
                        GucFaktoru = c.Decimal(precision: 18, scale: 2),
                        EnerjiNakHatGerilim = c.Decimal(precision: 18, scale: 2),
                        OrtamSicakligi = c.Decimal(precision: 18, scale: 1),
                        Unite1 = c.String(maxLength: 1),
                        Unite2 = c.String(maxLength: 1),
                        Uni1YatakSicY1 = c.Decimal(precision: 18, scale: 1),
                        Uni1YatakSicY2 = c.Decimal(precision: 18, scale: 1),
                        Uni1GenMakMSic = c.Decimal(precision: 18, scale: 1),
                        Uni1GovRBasinci = c.Decimal(precision: 18, scale: 1),
                        Uni2YatakSicY1 = c.Decimal(precision: 18, scale: 1),
                        Uni2YatakSicY2 = c.Decimal(precision: 18, scale: 1),
                        Uni2GenMakMSic = c.Decimal(precision: 18, scale: 1),
                        Uni2GovRBasinci = c.Decimal(precision: 18, scale: 1),
                        Uni1NozAcikYuzN1 = c.Int(),
                        Uni1NozAcikYuzN2 = c.Int(),
                        Uni2NozAcikYuzN1 = c.Int(),
                        Uni2NozAcikYuzN2 = c.Int(),
                        ArizaNotu = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.SaatlikUretimId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SaatlikUretims");
        }
    }
}
