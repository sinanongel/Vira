namespace Vira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeTblSozlesme_3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Sozlesmes", "Isveren_KurumId", "dbo.Kurums");
            DropForeignKey("dbo.Sozlesmes", "Yuklenici_KurumId", "dbo.Kurums");
            DropIndex("dbo.Sozlesmes", new[] { "Isveren_KurumId" });
            DropIndex("dbo.Sozlesmes", new[] { "Yuklenici_KurumId" });
            RenameColumn(table: "dbo.Sozlesmes", name: "Isveren_KurumId", newName: "IsverenKurumId");
            RenameColumn(table: "dbo.Sozlesmes", name: "Yuklenici_KurumId", newName: "YukleniciKurumId");
            AlterColumn("dbo.Sozlesmes", "IsverenKurumId", c => c.Int(nullable: false));
            AlterColumn("dbo.Sozlesmes", "YukleniciKurumId", c => c.Int(nullable: false));
            CreateIndex("dbo.Sozlesmes", "YukleniciKurumId");
            CreateIndex("dbo.Sozlesmes", "IsverenKurumId");
            AddForeignKey("dbo.Sozlesmes", "IsverenKurumId", "dbo.Kurums", "KurumId", cascadeDelete: true);
            AddForeignKey("dbo.Sozlesmes", "YukleniciKurumId", "dbo.Kurums", "KurumId", cascadeDelete: true);
            DropColumn("dbo.Sozlesmes", "KurumId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sozlesmes", "KurumId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Sozlesmes", "YukleniciKurumId", "dbo.Kurums");
            DropForeignKey("dbo.Sozlesmes", "IsverenKurumId", "dbo.Kurums");
            DropIndex("dbo.Sozlesmes", new[] { "IsverenKurumId" });
            DropIndex("dbo.Sozlesmes", new[] { "YukleniciKurumId" });
            AlterColumn("dbo.Sozlesmes", "YukleniciKurumId", c => c.Int());
            AlterColumn("dbo.Sozlesmes", "IsverenKurumId", c => c.Int());
            RenameColumn(table: "dbo.Sozlesmes", name: "YukleniciKurumId", newName: "Yuklenici_KurumId");
            RenameColumn(table: "dbo.Sozlesmes", name: "IsverenKurumId", newName: "Isveren_KurumId");
            CreateIndex("dbo.Sozlesmes", "Yuklenici_KurumId");
            CreateIndex("dbo.Sozlesmes", "Isveren_KurumId");
            AddForeignKey("dbo.Sozlesmes", "Yuklenici_KurumId", "dbo.Kurums", "KurumId");
            AddForeignKey("dbo.Sozlesmes", "Isveren_KurumId", "dbo.Kurums", "KurumId");
        }
    }
}
