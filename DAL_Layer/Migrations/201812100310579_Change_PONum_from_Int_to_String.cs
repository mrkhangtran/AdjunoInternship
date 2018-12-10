namespace DAL_Layer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Change_PONum_from_Int_to_String : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.OrderModels", "PONumber", c => c.String());
            AlterColumn("dbo.OrderDetailModels", "ItemNumber", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.OrderDetailModels", "ItemNumber", c => c.Int(nullable: false));
            AlterColumn("dbo.OrderModels", "PONumber", c => c.Int(nullable: false));
        }
    }
}
