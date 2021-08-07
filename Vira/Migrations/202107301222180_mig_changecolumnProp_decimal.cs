namespace Vira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig_changecolumnProp_decimal : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.YakitTakips", "BitisKm", c => c.Decimal(nullable: false, precision: 18, scale: 1));
            AlterColumn("dbo.YakitTakips", "GidilenKm", c => c.Decimal(nullable: false, precision: 18, scale: 1));
            AlterColumn("dbo.YakitTakips", "OrtalamaTuketim", c => c.Decimal(nullable: false, precision: 18, scale: 4));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.YakitTakips", "OrtalamaTuketim", c => c.Decimal(precision: 18, scale: 4));
            AlterColumn("dbo.YakitTakips", "GidilenKm", c => c.Decimal(precision: 18, scale: 1));
            AlterColumn("dbo.YakitTakips", "BitisKm", c => c.Decimal(precision: 18, scale: 1));
        }
    }
}
