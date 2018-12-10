namespace DAL_Layer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddItemNumber : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderDetailModels", "ItemNumber", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderDetailModels", "ItemNumber");
        }
    }
}
