namespace Vira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig_addColumn_tblfirma : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Firmas", "FirmaAdresi", c => c.String(maxLength: 100));
            AddColumn("dbo.Firmas", "FirmaTelefon", c => c.String(maxLength: 11));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Firmas", "FirmaTelefon");
            DropColumn("dbo.Firmas", "FirmaAdresi");
        }
    }
}
