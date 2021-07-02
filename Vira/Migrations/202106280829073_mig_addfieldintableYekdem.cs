namespace Vira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig_addfieldintableYekdem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Yekdems", "BirimFiyatUsd", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Yekdems", "BirimFiyatUsd");
        }
    }
}
