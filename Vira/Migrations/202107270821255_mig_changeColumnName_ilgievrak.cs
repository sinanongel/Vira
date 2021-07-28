namespace Vira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig_changeColumnName_ilgievrak : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GelenEvraks", "IlgiEvrak", c => c.String(maxLength: 500));
            AddColumn("dbo.GidenEvraks", "IlgiEvrak", c => c.String(maxLength: 500));
            DropColumn("dbo.GelenEvraks", "IlisikGidenEvrakId");
            DropColumn("dbo.GidenEvraks", "IlisikGelenEvrakId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.GidenEvraks", "IlisikGelenEvrakId", c => c.Int());
            AddColumn("dbo.GelenEvraks", "IlisikGidenEvrakId", c => c.Int());
            DropColumn("dbo.GidenEvraks", "IlgiEvrak");
            DropColumn("dbo.GelenEvraks", "IlgiEvrak");
        }
    }
}
