namespace DAL_Layer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddReviseQuantity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderDetailModels", "ReviseQuantity", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderDetailModels", "ReviseQuantity");
        }
    }
}
