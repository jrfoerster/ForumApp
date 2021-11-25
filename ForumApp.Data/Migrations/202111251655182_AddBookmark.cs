namespace ForumApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBookmark : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bookmark",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ThreadId = c.Int(nullable: false),
                        UserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Thread", t => t.ThreadId, cascadeDelete: true)
                .Index(t => t.ThreadId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bookmark", "ThreadId", "dbo.Thread");
            DropIndex("dbo.Bookmark", new[] { "ThreadId" });
            DropTable("dbo.Bookmark");
        }
    }
}
