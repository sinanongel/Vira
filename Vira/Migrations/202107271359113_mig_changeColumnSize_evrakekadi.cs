namespace Vira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig_changeColumnSize_evrakekadi : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.EvrakEks", "EvrakEkAdi", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.EvrakEks", "EvrakEkAdi", c => c.String(maxLength: 100));
        }
    }
}
