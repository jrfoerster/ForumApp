namespace ForumApp.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class ForumUserId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Forum", "UserId", c => c.Guid(nullable: false));
        }

        public override void Down()
        {
            DropColumn("dbo.Forum", "UserId");
        }
    }
}
