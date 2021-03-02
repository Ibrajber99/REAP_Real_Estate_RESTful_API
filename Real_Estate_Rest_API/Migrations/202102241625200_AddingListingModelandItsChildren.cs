namespace Real_Estate_Rest_API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingListingModelandItsChildren : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PropertyFeatures",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FeatureType = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Listings",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SquareFootage = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NumOfBeds = c.Int(nullable: false),
                        NumofBaths = c.Int(nullable: false),
                        NumofStories = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateUpdated = c.DateTime(nullable: false),
                        Address_ID = c.Int(nullable: false),
                        Customer_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ListingAddresses", t => t.Address_ID, cascadeDelete: true)
                .ForeignKey("dbo.Customers", t => t.Customer_ID, cascadeDelete: true)
                .Index(t => t.Address_ID)
                .Index(t => t.Customer_ID);
            
            CreateTable(
                "dbo.ListingAddresses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StreetAddress = c.String(nullable: false),
                        Province = c.String(nullable: false),
                        PostalCode = c.String(nullable: false),
                        Municiplity = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.HeatingTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        HeatingType = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ListingPropertyFeatures",
                c => new
                    {
                        Listing_ID = c.Int(nullable: false),
                        PropertyFeatures_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Listing_ID, t.PropertyFeatures_ID })
                .ForeignKey("dbo.Listings", t => t.Listing_ID, cascadeDelete: true)
                .ForeignKey("dbo.PropertyFeatures", t => t.PropertyFeatures_ID, cascadeDelete: true)
                .Index(t => t.Listing_ID)
                .Index(t => t.PropertyFeatures_ID);
            
            CreateTable(
                "dbo.HeatingTypesListings",
                c => new
                    {
                        HeatingTypes_ID = c.Int(nullable: false),
                        Listing_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.HeatingTypes_ID, t.Listing_ID })
                .ForeignKey("dbo.HeatingTypes", t => t.HeatingTypes_ID, cascadeDelete: true)
                .ForeignKey("dbo.Listings", t => t.Listing_ID, cascadeDelete: true)
                .Index(t => t.HeatingTypes_ID)
                .Index(t => t.Listing_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HeatingTypesListings", "Listing_ID", "dbo.Listings");
            DropForeignKey("dbo.HeatingTypesListings", "HeatingTypes_ID", "dbo.HeatingTypes");
            DropForeignKey("dbo.ListingPropertyFeatures", "PropertyFeatures_ID", "dbo.PropertyFeatures");
            DropForeignKey("dbo.ListingPropertyFeatures", "Listing_ID", "dbo.Listings");
            DropForeignKey("dbo.Listings", "Customer_ID", "dbo.Customers");
            DropForeignKey("dbo.Listings", "Address_ID", "dbo.ListingAddresses");
            DropIndex("dbo.HeatingTypesListings", new[] { "Listing_ID" });
            DropIndex("dbo.HeatingTypesListings", new[] { "HeatingTypes_ID" });
            DropIndex("dbo.ListingPropertyFeatures", new[] { "PropertyFeatures_ID" });
            DropIndex("dbo.ListingPropertyFeatures", new[] { "Listing_ID" });
            DropIndex("dbo.Listings", new[] { "Customer_ID" });
            DropIndex("dbo.Listings", new[] { "Address_ID" });
            DropTable("dbo.HeatingTypesListings");
            DropTable("dbo.ListingPropertyFeatures");
            DropTable("dbo.HeatingTypes");
            DropTable("dbo.ListingAddresses");
            DropTable("dbo.Listings");
            DropTable("dbo.PropertyFeatures");
        }
    }
}
