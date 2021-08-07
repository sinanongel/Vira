namespace Vira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig_addColumn_oncekikayitid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.YakitTakips", "OncekiKayitId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.YakitTakips", "OncekiKayitId");
        }
    }
}
