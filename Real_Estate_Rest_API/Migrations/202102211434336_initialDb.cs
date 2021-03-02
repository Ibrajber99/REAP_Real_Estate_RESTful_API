namespace Real_Estate_Rest_API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CustomerAddresses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StreetAddress = c.String(nullable: false),
                        Municipality = c.String(nullable: false),
                        Province = c.String(nullable: false),
                        PostalCode = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        MiddleName = c.String(),
                        PhoneNumber = c.String(nullable: false),
                        Email = c.String(),
                        dateOfBirth = c.DateTime(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateUpdated = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CustomerAddress_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CustomerAddresses", t => t.CustomerAddress_ID)
                .Index(t => t.CustomerAddress_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Customers", "CustomerAddress_ID", "dbo.CustomerAddresses");
            DropIndex("dbo.Customers", new[] { "CustomerAddress_ID" });
            DropTable("dbo.Customers");
            DropTable("dbo.CustomerAddresses");
        }
    }
}
