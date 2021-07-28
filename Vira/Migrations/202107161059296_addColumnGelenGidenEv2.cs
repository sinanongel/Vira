namespace Vira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addColumnGelenGidenEv2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GelenEvraks", "EkAdet", c => c.Int());
            AddColumn("dbo.GidenEvraks", "EkAdet", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.GidenEvraks", "EkAdet");
            DropColumn("dbo.GelenEvraks", "EkAdet");
        }
    }
}
