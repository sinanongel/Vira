namespace Vira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig_addtable_araccinsi : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AracCinsis",
                c => new
                    {
                        AracCinsiId = c.Int(nullable: false, identity: true),
                        AracCinsiAdi = c.String(maxLength: 15),
                    })
                .PrimaryKey(t => t.AracCinsiId);
            
            AddColumn("dbo.Araclars", "AracCinsiId", c => c.Int(nullable: false));
            CreateIndex("dbo.Araclars", "AracCinsiId");
            AddForeignKey("dbo.Araclars", "AracCinsiId", "dbo.AracCinsis", "AracCinsiId", cascadeDelete: true);
            DropColumn("dbo.Araclars", "AracCinsi");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Araclars", "AracCinsi", c => c.String(maxLength: 15));
            DropForeignKey("dbo.Araclars", "AracCinsiId", "dbo.AracCinsis");
            DropIndex("dbo.Araclars", new[] { "AracCinsiId" });
            DropColumn("dbo.Araclars", "AracCinsiId");
            DropTable("dbo.AracCinsis");
        }
    }
}
