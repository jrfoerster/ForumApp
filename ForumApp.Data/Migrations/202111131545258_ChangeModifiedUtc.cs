namespace ForumApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeModifiedUtc : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Thread", "ModifiedUtc", c => c.DateTimeOffset(nullable: false, precision: 7));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Thread", "ModifiedUtc", c => c.DateTimeOffset(precision: 7));
        }
    }
}
