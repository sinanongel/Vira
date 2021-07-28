namespace Vira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig_changeColumnName_gievrak1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EvrakEks", "GidenEvrak_GidenEvrakkId", "dbo.GidenEvraks");
            DropColumn("dbo.EvrakEks", "GidenEvrakId");
            RenameColumn(table: "dbo.EvrakEks", name: "GidenEvrak_GidenEvrakkId", newName: "GidenEvrakId");
            RenameIndex(table: "dbo.EvrakEks", name: "IX_GidenEvrak_GidenEvrakkId", newName: "IX_GidenEvrakId");
            DropPrimaryKey("dbo.GidenEvraks");
            AddColumn("dbo.GidenEvraks", "GidenEvrakId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.GidenEvraks", "GidenEvrakId");
            AddForeignKey("dbo.EvrakEks", "GidenEvrakId", "dbo.GidenEvraks", "GidenEvrakId");
            DropColumn("dbo.GidenEvraks", "GidenEvrakkId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.GidenEvraks", "GidenEvrakkId", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.EvrakEks", "GidenEvrakId", "dbo.GidenEvraks");
            DropPrimaryKey("dbo.GidenEvraks");
            DropColumn("dbo.GidenEvraks", "GidenEvrakId");
            AddPrimaryKey("dbo.GidenEvraks", "GidenEvrakkId");
            RenameIndex(table: "dbo.EvrakEks", name: "IX_GidenEvrakId", newName: "IX_GidenEvrak_GidenEvrakkId");
            RenameColumn(table: "dbo.EvrakEks", name: "GidenEvrakId", newName: "GidenEvrak_GidenEvrakkId");
            AddColumn("dbo.EvrakEks", "GidenEvrakId", c => c.Int());
            AddForeignKey("dbo.EvrakEks", "GidenEvrak_GidenEvrakkId", "dbo.GidenEvraks", "GidenEvrakkId");
        }
    }
}
