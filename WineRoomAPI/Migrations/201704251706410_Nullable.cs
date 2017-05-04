namespace WineRoomAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Nullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Wines", "DateAdded", c => c.DateTime());
            AlterColumn("dbo.Wines", "DrinkableStart", c => c.DateTime());
            AlterColumn("dbo.Wines", "DrinkableEnd", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Wines", "DrinkableEnd", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Wines", "DrinkableStart", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Wines", "DateAdded", c => c.DateTime(nullable: false));
        }
    }
}
