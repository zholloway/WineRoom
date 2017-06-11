namespace WineRoomAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RatingaddedtoWinesmodel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Wines", "Rating", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Wines", "Rating", c => c.Int(nullable: false));
        }
    }
}
