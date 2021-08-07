namespace Vira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig_addTablo_araclar : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Araclars",
                c => new
                    {
                        AraclarId = c.Int(nullable: false, identity: true),
                        AracCinsi = c.String(maxLength: 15),
                        AracMarka = c.String(maxLength: 15),
                        AracModel = c.String(maxLength: 15),
                        CalismaSekli = c.String(maxLength: 10),
                        KullanimYeri = c.String(maxLength: 25),
                    })
                .PrimaryKey(t => t.AraclarId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Araclars");
        }
    }
}
