namespace Vira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig_changeLength_kullanimYer : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Araclars", "AracModel", c => c.String(maxLength: 25));
            AlterColumn("dbo.Araclars", "KullanimYeri", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Araclars", "KullanimYeri", c => c.String(maxLength: 25));
            AlterColumn("dbo.Araclars", "AracModel", c => c.String(maxLength: 15));
        }
    }
}
