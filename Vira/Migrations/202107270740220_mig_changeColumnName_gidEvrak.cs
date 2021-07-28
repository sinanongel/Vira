namespace Vira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig_changeColumnName_gidEvrak : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GidenEvraks", "IslemTarihi", c => c.DateTime(nullable: false));
            DropColumn("dbo.GidenEvraks", "TebligatTarihi");
        }
        
        public override void Down()
        {
            AddColumn("dbo.GidenEvraks", "TebligatTarihi", c => c.DateTime(nullable: false));
            DropColumn("dbo.GidenEvraks", "IslemTarihi");
        }
    }
}
