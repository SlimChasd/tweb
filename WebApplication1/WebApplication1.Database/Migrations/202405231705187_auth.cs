namespace WebApplication1.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class auth : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "Username", c => c.String());
            AddColumn("dbo.Customers", "PasswordHash", c => c.String());
            AddColumn("dbo.Customers", "Role", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "Role");
            DropColumn("dbo.Customers", "PasswordHash");
            DropColumn("dbo.Customers", "Username");
        }
    }
}
