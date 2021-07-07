namespace Vira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeTblSozlesme_1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Sozlesmes", "KurumId", "dbo.Kurums");
            DropIndex("dbo.Sozlesmes", new[] { "KurumId" });
            RenameColumn(table: "dbo.Sozlesmes", name: "KurumId", newName: "Isveren_KurumId");
            AddColumn("dbo.Sozlesmes", "YukleniciId", c => c.Int(nullable: false));
            AddColumn("dbo.Sozlesmes", "IsverenId", c => c.Int(nullable: false));
            AddColumn("dbo.Sozlesmes", "Yuklenici_KurumId", c => c.Int());
            AlterColumn("dbo.Sozlesmes", "Isveren_KurumId", c => c.Int());
            CreateIndex("dbo.Sozlesmes", "Isveren_KurumId");
            CreateIndex("dbo.Sozlesmes", "Yuklenici_KurumId");
            AddForeignKey("dbo.Sozlesmes", "Yuklenici_KurumId", "dbo.Kurums", "KurumId");
            AddForeignKey("dbo.Sozlesmes", "Isveren_KurumId", "dbo.Kurums", "KurumId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sozlesmes", "Isveren_KurumId", "dbo.Kurums");
            DropForeignKey("dbo.Sozlesmes", "Yuklenici_KurumId", "dbo.Kurums");
            DropIndex("dbo.Sozlesmes", new[] { "Yuklenici_KurumId" });
            DropIndex("dbo.Sozlesmes", new[] { "Isveren_KurumId" });
            AlterColumn("dbo.Sozlesmes", "Isveren_KurumId", c => c.Int(nullable: false));
            DropColumn("dbo.Sozlesmes", "Yuklenici_KurumId");
            DropColumn("dbo.Sozlesmes", "IsverenId");
            DropColumn("dbo.Sozlesmes", "YukleniciId");
            RenameColumn(table: "dbo.Sozlesmes", name: "Isveren_KurumId", newName: "KurumId");
            CreateIndex("dbo.Sozlesmes", "KurumId");
            AddForeignKey("dbo.Sozlesmes", "KurumId", "dbo.Kurums", "KurumId", cascadeDelete: true);
        }
    }
}
