namespace WineRoomAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatingdatestodatetimetypes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DrankWines",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DateDrank = c.DateTime(nullable: false),
                        People = c.String(),
                        Location = c.String(),
                        Comments = c.String(),
                        WineID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Wines", t => t.WineID, cascadeDelete: true)
                .Index(t => t.WineID);
            
            CreateTable(
                "dbo.Wines",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NumberOfBottles = c.Int(nullable: false),
                        Format = c.String(),
                        Year = c.DateTime(nullable: false),
                        GrapeType = c.String(),
                        Vineyard = c.String(),
                        DateAdded = c.DateTime(nullable: false),
                        Location = c.String(),
                        DrinkableStart = c.DateTime(nullable: false),
                        DrinkableEnd = c.DateTime(nullable: false),
                        PurchasePrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MarketPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Favorite = c.Boolean(nullable: false),
                        Region = c.String(),
                        Color = c.String(),
                        Tags = c.String(),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.String(),
                        AdminStatus = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DrankWines", "WineID", "dbo.Wines");
            DropForeignKey("dbo.Wines", "UserID", "dbo.Users");
            DropIndex("dbo.Wines", new[] { "UserID" });
            DropIndex("dbo.DrankWines", new[] { "WineID" });
            DropTable("dbo.Users");
            DropTable("dbo.Wines");
            DropTable("dbo.DrankWines");
        }
    }
}
