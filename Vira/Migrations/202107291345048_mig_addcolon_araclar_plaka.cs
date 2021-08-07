namespace Vira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig_addcolon_araclar_plaka : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Araclars", "AracPlakasi", c => c.String(maxLength: 10));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Araclars", "AracPlakasi");
        }
    }
}
