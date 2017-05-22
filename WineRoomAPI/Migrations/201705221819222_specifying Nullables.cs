namespace WineRoomAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class specifyingNullables : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Wines", "NumberOfBottles", c => c.Int());
            AlterColumn("dbo.Wines", "DateAdded", c => c.DateTime());
            AlterColumn("dbo.Wines", "DrinkableStart", c => c.DateTime());
            AlterColumn("dbo.Wines", "DrinkableEnd", c => c.DateTime());
            AlterColumn("dbo.Wines", "PurchasePrice", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Wines", "MarketPrice", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Wines", "Favorite", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Wines", "Favorite", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Wines", "MarketPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Wines", "PurchasePrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Wines", "DrinkableEnd", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Wines", "DrinkableStart", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Wines", "DateAdded", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Wines", "NumberOfBottles", c => c.Int(nullable: false));
        }
    }
}
