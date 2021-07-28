namespace Vira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig_addColumnGelenGidenEv : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GelenEvraks", "AltBirimId", c => c.Int());
            AddColumn("dbo.GidenEvraks", "AltBirimId", c => c.Int());
            CreateIndex("dbo.GelenEvraks", "AltBirimId");
            CreateIndex("dbo.GidenEvraks", "AltBirimId");
            AddForeignKey("dbo.GelenEvraks", "AltBirimId", "dbo.AltBirims", "AltBirimId");
            AddForeignKey("dbo.GidenEvraks", "AltBirimId", "dbo.AltBirims", "AltBirimId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GidenEvraks", "AltBirimId", "dbo.AltBirims");
            DropForeignKey("dbo.GelenEvraks", "AltBirimId", "dbo.AltBirims");
            DropIndex("dbo.GidenEvraks", new[] { "AltBirimId" });
            DropIndex("dbo.GelenEvraks", new[] { "AltBirimId" });
            DropColumn("dbo.GidenEvraks", "AltBirimId");
            DropColumn("dbo.GelenEvraks", "AltBirimId");
        }
    }
}
