namespace Vira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig_addcolumn_yakittakip_ot : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.YakitTakips", "OrtalamaTuketim", c => c.Decimal(precision: 18, scale: 4));
        }
        
        public override void Down()
        {
            DropColumn("dbo.YakitTakips", "OrtalamaTuketim");
        }
    }
}
