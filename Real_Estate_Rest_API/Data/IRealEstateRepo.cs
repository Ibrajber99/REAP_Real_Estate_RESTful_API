using Real_Estate_Rest_API.Data.Entities.Customer_Related;
using Real_Estate_Rest_API.Data.Entities.Listing_Related;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_Estate_Rest_API.Data
{
    public interface IRealEstateRepo
    {
        Task<bool> SaveChangesAsync();

        #region Customer Related Methods
        Task<List<Customer>> GetCustomers(bool includeAddress = false, bool includeArchived = false);

        Task<Customer> GetCustomer(int id, bool includeAddress = false);

        void AddCustomer(Customer customer);

        Task<bool> SaveCustomer(Customer customer);

        void DeleteCustomer(Customer customer);
        #endregion


        Task<List<Listing>> GetListings(bool includeHeating = false, bool includeFeatures = false);

        Task<Listing> GetListing(int id, bool includeHeating = false, bool includeFeatures = false);

        Task<int> AddListing(Listing listing);

        Task<int> UpdateListing(Listing listing);


        Task<List<HeatingTypes>> GetHeatings();

        Task<HeatingTypes> GetHeating(int id);

        Task<int> AddHeating(HeatingTypes heating);

        Task<int> UpdateHeating(HeatingTypes heating);


        Task<List<PropertyFeatures>> GetFeatures();

        Task<PropertyFeatures> GetFeature(int id);

        Task<int> AddFeature(PropertyFeatures feature);

        Task<int> UpdateFeature(PropertyFeatures feature);

    }
}
