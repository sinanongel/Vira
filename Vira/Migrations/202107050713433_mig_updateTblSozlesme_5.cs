namespace Vira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig_updateTblSozlesme_5 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Sozlesmes", name: "DosyaId", newName: "Dosya_DosyaId");
            RenameIndex(table: "dbo.Sozlesmes", name: "IX_DosyaId", newName: "IX_Dosya_DosyaId");
            AddColumn("dbo.Sozlesmes", "Dosya", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sozlesmes", "Dosya");
            RenameIndex(table: "dbo.Sozlesmes", name: "IX_Dosya_DosyaId", newName: "IX_DosyaId");
            RenameColumn(table: "dbo.Sozlesmes", name: "Dosya_DosyaId", newName: "DosyaId");
        }
    }
}
