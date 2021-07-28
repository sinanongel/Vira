namespace Vira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig_addColumn_tblEvrakEk : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EvrakEks",
                c => new
                    {
                        EvrakEkId = c.Int(nullable: false, identity: true),
                        EvrakEkAdi = c.String(maxLength: 100),
                        GelenEvrakId = c.Int(),
                        GidenEvrakId = c.Int(),
                        GidenEvrak_GidenEvrakkId = c.Int(),
                    })
                .PrimaryKey(t => t.EvrakEkId)
                .ForeignKey("dbo.GelenEvraks", t => t.GelenEvrakId)
                .ForeignKey("dbo.GidenEvraks", t => t.GidenEvrak_GidenEvrakkId)
                .Index(t => t.GelenEvrakId)
                .Index(t => t.GidenEvrak_GidenEvrakkId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EvrakEks", "GidenEvrak_GidenEvrakkId", "dbo.GidenEvraks");
            DropForeignKey("dbo.EvrakEks", "GelenEvrakId", "dbo.GelenEvraks");
            DropIndex("dbo.EvrakEks", new[] { "GidenEvrak_GidenEvrakkId" });
            DropIndex("dbo.EvrakEks", new[] { "GelenEvrakId" });
            DropTable("dbo.EvrakEks");
        }
    }
}
