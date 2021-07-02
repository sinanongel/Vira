namespace Vira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig_remfieldintableYekdem : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Yekdems", "BirimFiyatUsd", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Yekdems", "YekdemKur");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Yekdems", "YekdemKur", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Yekdems", "BirimFiyatUsd", c => c.Decimal(precision: 18, scale: 2));
        }
    }
}
