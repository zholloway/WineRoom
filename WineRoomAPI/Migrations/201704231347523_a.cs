namespace WineRoomAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.DrankWines", new[] { "wineID" });
            CreateIndex("dbo.DrankWines", "WineID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.DrankWines", new[] { "WineID" });
            CreateIndex("dbo.DrankWines", "wineID");
        }
    }
}
