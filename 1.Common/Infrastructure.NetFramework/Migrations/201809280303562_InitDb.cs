namespace Infrastructure.NetFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderNo = c.String(nullable: false, maxLength: 40, unicode: false),
                        TotalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UserName = c.String(nullable: false, maxLength: 40),
                        CreateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OrderItem",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductName = c.String(nullable: false, maxLength: 20),
                        Qty = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OrderId = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Order", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.OrderId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderItem", "OrderId", "dbo.Order");
            DropIndex("dbo.OrderItem", new[] { "OrderId" });
            DropTable("dbo.OrderItem");
            DropTable("dbo.Order");
        }
    }
}
