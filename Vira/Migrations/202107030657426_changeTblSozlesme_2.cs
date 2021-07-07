namespace Vira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeTblSozlesme_2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sozlesmes", "KurumId", c => c.Int(nullable: false));
            DropColumn("dbo.Sozlesmes", "YukleniciId");
            DropColumn("dbo.Sozlesmes", "IsverenId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sozlesmes", "IsverenId", c => c.Int(nullable: false));
            AddColumn("dbo.Sozlesmes", "YukleniciId", c => c.Int(nullable: false));
            DropColumn("dbo.Sozlesmes", "KurumId");
        }
    }
}
