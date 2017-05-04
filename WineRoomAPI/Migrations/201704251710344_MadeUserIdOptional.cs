namespace WineRoomAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MadeUserIdOptional : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Wines", "UserID", "dbo.Users");
            DropIndex("dbo.Wines", new[] { "UserID" });
            AlterColumn("dbo.Wines", "UserID", c => c.Int());
            CreateIndex("dbo.Wines", "UserID");
            AddForeignKey("dbo.Wines", "UserID", "dbo.Users", "id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Wines", "UserID", "dbo.Users");
            DropIndex("dbo.Wines", new[] { "UserID" });
            AlterColumn("dbo.Wines", "UserID", c => c.Int(nullable: false));
            CreateIndex("dbo.Wines", "UserID");
            AddForeignKey("dbo.Wines", "UserID", "dbo.Users", "id", cascadeDelete: true);
        }
    }
}
