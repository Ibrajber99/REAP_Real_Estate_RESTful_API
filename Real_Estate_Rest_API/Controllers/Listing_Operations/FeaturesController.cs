using Real_Estate_Rest_API.Data;
using Real_Estate_Rest_API.Data.Entities.Listing_Related;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Real_Estate_Rest_API.Controllers.Listing_Operations
{

    [RoutePrefix("api/Listings/Features")]
    public class FeaturesController : ApiController
    {
        private readonly IRealEstateRepo _repo;

        public FeaturesController(IRealEstateRepo repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Get all features
        /// </summary>
        /// <returns>200 Success || 500 Server error || 404 Not found</returns>
        [Route()]
        // GET: api/Features
        public async Task<IHttpActionResult> Get()
        {
            try
            {
                var res = await _repo.GetFeatures();
                if (res == null)
                    return NotFound();

                return Ok(res);
            }
            catch (Exception)
            {

                return InternalServerError();
            }
        }

        /// <summary>
        /// Get Feature
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>200 Ok || 500 Server error || 404 Not found</returns>
        [Route("{id:int}",Name ="GetFeature")]
        // GET: api/Features/5
        public async Task<IHttpActionResult> Get(int id)
        {
            try
            {
                var res = await _repo.GetFeature(id);
                if (res == null)
                    return NotFound();

                return Ok(res);
            }
            catch (Exception)
            {

                return InternalServerError();
            }
        }


        /// <summary>
        /// Insert new entry
        /// </summary>
        /// <param name="feature">feature model</param>
        /// <returns>204 Created || 500 Server error || 400 Bad request</returns>
        [Route()]
        // POST: api/Features
        public async Task<IHttpActionResult> Post(PropertyFeatures feature)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var res = await _repo.AddFeature(feature);

                    if(res > 0)
                    {
                        return CreatedAtRoute("GetFeature", new { id = feature.ID }, feature);
                    }
                    else
                    {
                        return InternalServerError();
                    }

                }
                catch (Exception)
                {

                    return InternalServerError();
                }

            }

            return BadRequest(ModelState);

        }


        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <param name="feature">feature model</param>
        /// <returns>200 Success || 500 Server error }} 400 Bad request</returns>
        [Route("{id:int}")]
        // PUT: api/Features/5
        public async Task<IHttpActionResult> Put(int id, PropertyFeatures feature)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var findFeature = await _repo.GetFeature(id);
                    if (findFeature == null)
                        return NotFound();

                    findFeature.FeatureType = feature.FeatureType;

                    var res =await  _repo.UpdateFeature(feature);

                    if(res > 0)
                    {
                        return Ok(feature);
                    }
                    else
                    {
                        return InternalServerError();
                    }
                }
                catch (Exception)
                {

                    return InternalServerError();
                }
            }
            return BadRequest(ModelState);

        }
    }
}
