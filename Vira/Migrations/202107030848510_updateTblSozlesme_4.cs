namespace Vira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateTblSozlesme_4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Sozlesmes", "DosyaId", "dbo.Dosyas");
            DropIndex("dbo.Sozlesmes", new[] { "DosyaId" });
            AlterColumn("dbo.Sozlesmes", "DosyaId", c => c.Int());
            CreateIndex("dbo.Sozlesmes", "DosyaId");
            AddForeignKey("dbo.Sozlesmes", "DosyaId", "dbo.Dosyas", "DosyaId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sozlesmes", "DosyaId", "dbo.Dosyas");
            DropIndex("dbo.Sozlesmes", new[] { "DosyaId" });
            AlterColumn("dbo.Sozlesmes", "DosyaId", c => c.Int(nullable: false));
            CreateIndex("dbo.Sozlesmes", "DosyaId");
            AddForeignKey("dbo.Sozlesmes", "DosyaId", "dbo.Dosyas", "DosyaId", cascadeDelete: true);
        }
    }
}
