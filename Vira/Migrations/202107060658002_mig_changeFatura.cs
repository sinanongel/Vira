namespace Vira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig_changeFatura : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Faturas", "DosyaId", "dbo.Dosyas");
            DropIndex("dbo.Faturas", new[] { "DosyaId" });
            AddColumn("dbo.Faturas", "Dosya", c => c.String(maxLength: 100));
            DropColumn("dbo.Faturas", "DosyaId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Faturas", "DosyaId", c => c.Int());
            DropColumn("dbo.Faturas", "Dosya");
            CreateIndex("dbo.Faturas", "DosyaId");
            AddForeignKey("dbo.Faturas", "DosyaId", "dbo.Dosyas", "DosyaId");
        }
    }
}
