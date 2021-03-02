using Real_Estate_Rest_API.Data.Entities.Customer_Related;
using Real_Estate_Rest_API.Data.Entities.Listing_Related;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Real_Estate_Rest_API.Data
{


    public class RealEstateRepo : IRealEstateRepo
    {
        private readonly RealEstateContext _context;

        public RealEstateRepo()
        {
            _context = new RealEstateContext();
        }

        #region Customer Related Methods
        public void AddCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
        }

        public void DeleteCustomer(Customer customer)
        {
            customer.IsActive = false;

            _context.Entry(customer).State
                = EntityState.Modified;
        }

        public async Task<Customer> GetCustomer(int id, 
            bool includeAddress = false)
        {
            using (var context = new RealEstateContext())
            {
                IQueryable<Customer> query = _context.Customers;

                if (includeAddress)
                {
                    query = query.Include(add => add.CustomerAddress);
                }

                query = query.Where(c => c.ID == id);

                return await query.AsNoTracking().FirstOrDefaultAsync();
            }
        }

        public async Task<List<Customer>> GetCustomers(bool includeAddress = false,
            bool includeArchived = false)
        {
            IQueryable<Customer> query = _context.Customers;

            if (includeAddress)
            {
                query = query.Include(add => add.CustomerAddress);
            }

            if (!includeArchived)
            {
                query = query.Where(c => c.IsActive == true);
            }

            return await query.ToListAsync();
        }

        public async Task<List<CustomerAddress>> GetCustomersAddresses()
        {
            IQueryable<CustomerAddress> query = _context.CustomerAddress;

            return await query.ToListAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public  async Task<bool> SaveCustomer(Customer customer)
        {
            using (var context = new RealEstateContext())
            {
                context.Customers.Attach(customer);
                context.Entry(customer).State = EntityState.Modified;
                return await context.SaveChangesAsync() > 0;
            }
        }

        #endregion

        #region Listing related methods
        public async Task<List<Listing>> GetListings
            (bool includeHeating = false, bool includeFeatures = false)
        {
            //Default Mandatory Entry before filtering any other properties
            var query = _context.Listings.Include(c => c.Address)
                .Include(c => c.Customer).AsNoTracking();

            if (includeFeatures)
                query = query.Include(f => f.Features);

            if (includeHeating)
                query = query.Include(h => h.Heatings);

            return await query.ToListAsync();
        }

        public async Task<Listing> GetListing
            (int id, bool includeHeating = false, bool includeFeatures = false)
        {
            var query = _context.Listings.Include(c => c.Address).Include(c => c.Customer)
                            .AsNoTracking();

            if (includeFeatures)
                query = query.Include(f => f.Features);

            if (includeHeating)
                query = query.Include(h => h.Heatings);

            return await query.FirstOrDefaultAsync(c => c.ID == id);

        }

        public async Task<int> AddListing(Listing listing)
        {
            using (var context = new RealEstateContext())
            {
                var heatingList = new List<HeatingTypes>(listing.Heatings);
                var featuresList = new List<PropertyFeatures>(listing.Features);

                listing.Heatings.Clear();
                listing.Features.Clear();

                listing.Customer = context.Customers.SingleOrDefault(c => c.ID == listing.Customer.ID);

                foreach (var heating in heatingList)
                {
                    var findHeating = context.ListingHeatings.FirstOrDefault(x => x.ID == heating.ID);
                    listing.Heatings.Add(findHeating);
                }

                foreach (var features in featuresList)
                {
                    var findFeature = context.ListingFeatures.FirstOrDefault(x => x.ID == features.ID);
                    listing.Features.Add(findFeature);
                }

                context.Listings.Add(listing);
                return await context.SaveChangesAsync();

            }
        }

        public async Task<int> UpdateListing(Listing listing)
        {
            using (var context = new RealEstateContext())
            {
                var heatingList = new List<HeatingTypes>(listing.Heatings);
                var featuresList = new List<PropertyFeatures>(listing.Features);

                listing.Heatings.Clear();
                listing.Features.Clear();

                listing.Customer = context.Customers.SingleOrDefault(c => c.ID == listing.Customer.ID);

                foreach (var heating in heatingList)
                {
                    var findHeating = context.ListingHeatings.FirstOrDefault(x => x.ID == heating.ID);
                    listing.Heatings.Add(findHeating);
                }

                foreach (var features in featuresList)
                {
                    var findFeature = context.ListingFeatures.FirstOrDefault(x => x.ID == features.ID);
                    listing.Features.Add(findFeature);
                }

                context.Listings.Attach(listing);

                context.Entry(listing.Address).State = EntityState.Modified;
                context.Entry(listing).State = EntityState.Modified;

                return await context.SaveChangesAsync();

            }
        }

        public async Task<List<HeatingTypes>> GetHeatings()
        {
            var query = _context.ListingHeatings.AsNoTracking().AsQueryable();

            return await query.ToListAsync();
        }

        public Task<HeatingTypes> GetHeating(int id)
        {
            var query = _context.ListingHeatings
                .AsNoTracking().FirstOrDefaultAsync(x => x.ID == id);

            return query;
        }

        public async Task<int> AddHeating(HeatingTypes heating)
        {
            _context.ListingHeatings.Add(heating);

            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateHeating(HeatingTypes heating)
        {
            _context.ListingHeatings.Attach(heating);
            _context.Entry(heating).State = EntityState.Modified;

            return await _context.SaveChangesAsync();
        }

        public async Task<List<PropertyFeatures>> GetFeatures()
        {
            var query = _context.ListingFeatures.AsNoTracking().AsQueryable();

            return await query.ToListAsync();
        }

        public async Task<PropertyFeatures> GetFeature(int id)
        {
            var query = _context.ListingFeatures.AsNoTracking().AsQueryable();

            return await query.FirstOrDefaultAsync(x => x.ID == id);
        }

        public async Task<int> AddFeature(PropertyFeatures feature)
        {
            _context.ListingFeatures.Add(feature);

            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateFeature(PropertyFeatures feature)
        {
            _context.ListingFeatures.Attach(feature);
            _context.Entry(feature).State = EntityState.Modified;

            return await _context.SaveChangesAsync();
        }
        #endregion
    }
}