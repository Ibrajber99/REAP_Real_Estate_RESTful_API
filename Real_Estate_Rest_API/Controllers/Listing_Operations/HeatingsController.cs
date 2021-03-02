using Real_Estate_Rest_API.Data;
using Real_Estate_Rest_API.Data.Entities.Listing_Related;
using Real_Estate_Rest_API.DTO_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Real_Estate_Rest_API.Controllers.Listing_Operations
{
    /// <summary>
    /// Heating Operations GET,POST,PUT for Heating
    /// </summary>
    [RoutePrefix("api/listings/Heatings")]
    public class HeatingsController : ApiController
    {
        private readonly IRealEstateRepo _repo;

        /// <summary>
        /// CTOR
        /// </summary>
        /// <param name="repo"></param>
        public HeatingsController(IRealEstateRepo repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Gets All Heatings
        /// </summary>
        /// <returns></returns>
        // GET: api/Heatings
        [Route()]
        public async Task<IHttpActionResult> Get()
        {
            try
            {
                var res = await _repo.GetHeatings();
                if (res == null)
                    return NotFound();

                return Ok(res);
            }
            catch (Exception ex)
            {

                return InternalServerError();
            }
        }

        /// <summary>
        /// Gets a Heating 
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>200 Success || 404 Not found || 500 Server error</returns>
        [Route("{id:int}",Name ="GetHeating")]
        // GET: api/Heatings/5
        public async Task<IHttpActionResult> Get(int id)
        {
            try
            {
                var res = await _repo.GetHeating(id);
                if (res == null)
                    return NotFound();

                var viewModel = new HeatingDTOModel
                {
                    ID = res.ID,
                    HeatingType = res.HeatingType
                };

                return Ok(viewModel);
            }
            catch (Exception)
            {

                return InternalServerError();
            }
        }

        /// <summary>
        /// Inserts Heating Model
        /// </summary>
        /// <param name="model">Input Model</param>
        /// <returns>200 Success || 400 Bad Request|| 500 Server error</returns>
        // POST: api/Heatings
        [Route()]
        public async Task<IHttpActionResult> Post(HeatingDTOModel model)
        {
            if (ModelState.IsValid)
            {
                if(model.ID != 0)
                {
                    ModelState.AddModelError(string.Empty,
                        "ID of value bigger than 0 shouldn't be specified on POST" +
                        " Either Remove id or Declare id property with value 0");
                }
                else
                {
                    var modelToInsert = new HeatingTypes
                    {
                        HeatingType = model.HeatingType
                    };

                    var res = await _repo.AddHeating(modelToInsert);

                    if(res > 0)
                    {
                        return CreatedAtRoute("GetHeating", new { id = modelToInsert.ID }, modelToInsert);
                    }
                    else
                    {
                        return InternalServerError();
                    }

                }
            }

            return BadRequest(ModelState);
        }


        /// <summary>
        /// Updates the entity's data
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model">Input Model</param>
        /// <returns>200 Success || 400 Bad Request|| 500 Server error</returns>
        // PUT: api/Heatings/5
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Put(int id,HeatingDTOModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var findModel = await _repo.GetHeating(model.ID);

                    if (findModel == null)
                        return NotFound();

                    findModel.HeatingType = model.HeatingType;

                    var res =await  _repo.UpdateHeating(findModel);

                    if(res > 0)
                    {
                        return Ok(model);
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
