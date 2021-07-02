namespace Vira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig_tableYekdem_changeDonemTarih : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Yekdems", "DonemTarih", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Yekdems", "DonemTarih", c => c.DateTime());
        }
    }
}
