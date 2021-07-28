namespace Vira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig_changeColumnName_gegievrak : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EvrakEks", "GidenEvrak_GidenEvrakkId", "dbo.GidenEvraks");
            DropIndex("dbo.EvrakEks", new[] { "GidenEvrak_GidenEvrakkId" });
            DropColumn("dbo.EvrakEks", "GidenEvrakId");
            DropColumn("dbo.EvrakEks", "GidenEvrak_GidenEvrakkId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EvrakEks", "GidenEvrak_GidenEvrakkId", c => c.Int());
            AddColumn("dbo.EvrakEks", "GidenEvrakId", c => c.Int());
            CreateIndex("dbo.EvrakEks", "GidenEvrak_GidenEvrakkId");
            AddForeignKey("dbo.EvrakEks", "GidenEvrak_GidenEvrakkId", "dbo.GidenEvraks", "GidenEvrakkId");
        }
    }
}
