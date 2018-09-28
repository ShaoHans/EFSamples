namespace Infrastructure.NetFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSqlLogTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SqlLog",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Sql = c.String(nullable: false, maxLength: 8000, unicode: false),
                        Parameters = c.String(),
                        CommandType = c.String(),
                        Milliseconds = c.Long(nullable: false),
                        Exception = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SqlLog");
        }
    }
}
