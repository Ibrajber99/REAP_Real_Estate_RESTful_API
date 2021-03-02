using Real_Estate_Rest_API.Data;
using Real_Estate_Rest_API.Data.Entities.Customer_Related;
using Real_Estate_Rest_API.Data.Entities.Listing_Related;
using Real_Estate_Rest_API.DTO_Models;
using Real_Estate_Rest_API.objects_Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;


namespace Real_Estate_Rest_API.Controllers.Listing_Operations
{

    [RoutePrefix("api/listings")]
    public class ListingsController : ApiController
    {
        private readonly IRealEstateRepo _repo;
        private readonly IObjectsMapper _mapper;

        /// <summary>
        /// CTOR
        /// </summary>
        /// <param name="repo">DI object</param>
        /// <param name="mapper">DI object</param>
        public ListingsController(IRealEstateRepo repo, IObjectsMapper mapper )
        {
            _repo = repo;
            _mapper = mapper;
        }

        /// <summary>
        /// Returns List of Listings
        /// </summary>
        /// <param name="includeHeating">Get Proeprty Heating Types</param>
        /// <param name="includeFeatures">Get Property Features</param>
        /// <returns></returns>
        [Route()]
        public async Task<IHttpActionResult>Get(bool includeHeating = false,bool includeFeatures = false)
        {
            try
            {
                var res =await  _repo.GetListings(includeHeating, includeFeatures);

                if (res == null)
                    return NotFound();

                var viewModel = new List<ListingDTOReadModel>();

                foreach (var listing in res)
                {
                    viewModel.Add(_mapper.MapListingModelToDTOModel(listing));
                }

                return Ok(viewModel);
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// GetListing by it's Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="includeHeating">Get Proeprty Heating Types</param>
        /// <param name="includeFeatures">Get Property Features</param>
        /// <returns>200 Success || 404 not found || 500 server error</returns>
        [Route("{id:int}",Name ="GetListing")]
        public async Task<IHttpActionResult> Get(int id, bool includeHeating = false, bool includeFeatures = false)
        {
            try
            {
                var res = await _repo.GetListing
                    (id, includeHeating, includeFeatures);

                if (res == null)
                    return NotFound();

                var viewModel = _mapper.MapListingModelToDTOModel(res);


                return Ok(viewModel);
            }
            catch (Exception)
            {

                return InternalServerError();
            }
        }

        /// <summary>
        /// Insert new Listing Entry
        /// </summary>
        /// <param name="model">Json Array of Properties</param>
        /// <returns>200 Success || 404 bad request || 500 server error</returns>
        [Route()]
        public async Task<IHttpActionResult> Post(ListingDTOModel model)
        {
            if (ModelState.IsValid)
            {
                var listingModel = _mapper.MapListingDtoToModel(model);

                var res = await _repo.AddListing(listingModel);

                if (res > 0)
                {
                    return CreatedAtRoute("GetListing", new { id = listingModel.ID }, listingModel);
                }
                else
                    return InternalServerError();
            }

            return BadRequest(ModelState);
        }

        /// <summary>
        /// Update current Listing
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model">Json Array of Properties</param>
        /// <returns>200 Success || 404 bad request || 500 server error</returns>
        // PUT: api/Listings/5
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Put(int id, ListingDTOModel model)
        {
            if (ModelState.IsValid)
            {

                var listingModel = await _repo.GetListing(id);
                if (listingModel == null)
                    return NotFound();

                var dateCreated = listingModel.DateCreated;

                listingModel = _mapper.MapListingDtoToModel(model);

                listingModel.DateCreated = dateCreated;

                var res = await _repo.UpdateListing(listingModel);

                

                if (res > 0) {
                    var modelToReturn = _mapper.MapListingModelToDTOModel(listingModel);

                    return Ok(modelToReturn);
                }
                else
                    return InternalServerError();
            }

            return BadRequest(ModelState);
        }



        // DELETE: api/Listings/5
        public void Delete(int id)
        {
        }
    }
}
