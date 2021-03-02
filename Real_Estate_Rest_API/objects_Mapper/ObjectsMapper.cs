using Real_Estate_Rest_API.Data.Entities.Listing_Related;
using Real_Estate_Rest_API.DTO_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Real_Estate_Rest_API.objects_Mapper
{
    public class ObjectsMapper : IObjectsMapper
    {
        public Listing MapListingDtoToModel(ListingDTOModel model)
        {
            var listingModel = new Listing();

            listingModel.ID = model.ID;
            listingModel.Address = model.Address;
            listingModel.NumofBaths = model.NumofBaths;
            listingModel.NumOfBeds = model.NumOfBeds;
            listingModel.NumofStories = model.NumofStories;
            listingModel.Price = model.Price;
            listingModel.SquareFootage = model.SquareFootage;
            listingModel.DateUpdated = DateTime.Now;
            listingModel.Customer.ID = model.customerId;

            listingModel.Heatings.Clear();
            listingModel.Features.Clear();


            foreach (var heatingId in model.Heatings)
            {
                listingModel.Heatings.Add(new HeatingTypes { ID = heatingId });
            }

            foreach (var featureId in model.Features)
            {
                listingModel.Features.Add(new PropertyFeatures { ID = featureId });
            }

            return listingModel;
        }

        public ListingDTOReadModel MapListingModelToDTOModel(Listing listing)
        {
            var dtoViewModel = new ListingDTOReadModel();

            dtoViewModel.ID = listing.ID;
            dtoViewModel.customer = listing.Customer;
            dtoViewModel.Address = listing.Address;
            dtoViewModel.Price = listing.Price;
            dtoViewModel.SquareFootage = listing.SquareFootage;
            dtoViewModel.NumOfBeds = listing.NumOfBeds;
            dtoViewModel.NumofBaths = listing.NumofBaths;
            dtoViewModel.NumofStories = listing.NumofStories;
            dtoViewModel.DateCreated = listing.DateCreated;
            dtoViewModel.DateUpdated = listing.DateUpdated;

            dtoViewModel.Heatings = listing.Heatings.Select
                (x=> new HeatingTypes {ID = x.ID,HeatingType = x.HeatingType }).ToList();

            dtoViewModel.Features = listing.Features.Select
                (x => new PropertyFeatures { ID = x.ID, FeatureType = x.FeatureType }).ToList();

            return dtoViewModel;

        }
    }
}