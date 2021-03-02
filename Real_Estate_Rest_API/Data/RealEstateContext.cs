using Real_Estate_Rest_API.Data.Entities.Customer_Related;
using Real_Estate_Rest_API.Data.Entities.Listing_Related;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Real_Estate_Rest_API.Data
{
    /// <summary>
    /// Context class
    /// </summary>
    public class RealEstateContext : DbContext
    {
        /// <summary>
        /// CTOR
        /// </summary>
        public RealEstateContext() : base("RealEstateConnection")
        {
            Configuration.LazyLoadingEnabled = false;
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<RealEstateContext, Configuration>());
        }


        /// <summary>
        /// Customers Table
        /// </summary>
        public DbSet<Customer> Customers { get; set; }

        /// <summary>
        /// Custoemr Address Table
        /// </summary>
        public DbSet<CustomerAddress> CustomerAddress { get; set; }

        /// <summary>
        /// Listing Table
        /// </summary>
        public DbSet<Listing> Listings { get; set; }

        /// <summary>
        /// Features Table for Listings
        /// </summary>
        public DbSet<PropertyFeatures> ListingFeatures { get; set; }

        /// <summary>
        /// Heating Types Table for Listings
        /// </summary>
        public DbSet<HeatingTypes> ListingHeatings { get; set; }
    }
}