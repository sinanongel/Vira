namespace Vira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig_updateTblSozlesme_6 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Sozlesmes", "Dosya", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Sozlesmes", "Dosya", c => c.String(maxLength: 50));
        }
    }
}
