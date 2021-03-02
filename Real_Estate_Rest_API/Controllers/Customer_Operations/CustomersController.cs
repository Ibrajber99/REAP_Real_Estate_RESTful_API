using Real_Estate_Rest_API.Data;
using Real_Estate_Rest_API.Data.Entities.Customer_Related;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Real_Estate_Rest_API.Controllers.Customer_Operations
{
    /// <summary>
    /// Customer Information
    /// </summary>

    [RoutePrefix("api/Customers")]
    public class CustomersController : ApiController
    {

        private readonly IRealEstateRepo _repo;

        /// <summary>
        /// Entry Point
        /// </summary>
        public CustomersController(IRealEstateRepo repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Get All Customers
        /// </summary>
        /// <param name="includeAddress">Adds the address to the result</param>
        /// <param name="includeArchived">Adds the archived customers to the result</param>
        /// <returns>200 for successful result or 500 as server error</returns>
        // GET: api/Customers
        [Route()]//Default to --> api/Customers 
        public async Task<IHttpActionResult> Get(bool includeAddress = false,bool includeArchived = false)
        {
            try
            {
                var customers = await _repo.GetCustomers
                    (includeAddress, includeArchived);

                if (customers == null)
                    return NotFound();

                return Ok(customers);

            }
            catch (Exception ex)
            {
                return InternalServerError(ex);

            }
        }

        /// <summary>
        /// Get a certain customer
        /// </summary>
        /// <param name="id">Identifier for the customer</param>
        /// <param name="includeAddress">Adds the address to the result</param>
        /// <returns>returns 200 for successful result or 500 for server error</returns>
        // GET: api/Customers/5
        [Route("{id:int}",Name ="GetCustomer")]
        public async Task<IHttpActionResult> Get(int id,bool includeAddress = false)
        {
            try
            {
                var customer = await _repo.GetCustomer(id, includeAddress);
                if (customer == null)
                    return NotFound();

                return Ok(customer);
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Couldn't find resource");
                return InternalServerError();
            }

        }

        /// <summary>
        /// Creates a new Entry
        /// </summary>
        /// <param name="customer">Form Data</param>
        /// <returns>201 for successfully created resoruce or 500 as a server error</returns>
        // POST: api/Customers
        [Route()]
        public async Task<IHttpActionResult> Post(Customer customer)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _repo.AddCustomer(customer);

                    var insertResult = await _repo.SaveChangesAsync();

                    if (insertResult)
                    {
                        return CreatedAtRoute("GetCustomer", new { id = customer.ID }, customer);
                    }
                    else
                    {
                        return BadRequest("Something went wrong");
                    }
                }
                catch (Exception ex)
                {

                    return InternalServerError(ex);
                }
            }
            return BadRequest(ModelState);
        }

        /// <summary>
        /// Updates the entity's data
        /// </summary>
        /// <param name="id">Ident</param>
        /// <param name="customer">Identifier for the customer</param>
        /// <returns>200 for successfull result or 500 for server error or 400 as a bad request</returns>
        // PUT: api/Customers/5
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Put(int id,Customer customer)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var customerInDb = await _repo.GetCustomer(id);

                    if (customerInDb == null)
                        return NotFound();

                    customer.DateCreated = customerInDb.DateCreated;
                    customer.DateUpdated = DateTime.Now;

                    var updatedResult =await 
                        _repo.SaveCustomer(customer);

                    if (updatedResult)
                    {
                        return Ok(await _repo.GetCustomer(id));
                    }
                    else
                    {
                        return InternalServerError();
                    }
                }
                catch (Exception ex)
                {
                    InternalServerError(ex);
                }
            }
            return BadRequest(ModelState);
        }

        /// <summary>
        /// Delete's the Entity ---> by setting it to archived state
        /// </summary>
        /// <param name="id">Identifier for the customer</param>
        /// <returns>200 Success or 500 as server error</returns>
        // DELETE: api/Customers/5
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            try
            {
                var customerToDelete =await _repo.GetCustomer(id);

                if (customerToDelete == null)
                    return NotFound();

                _repo.DeleteCustomer(customerToDelete);//Setting the status of the entity to Inactive

                var insertResult = await _repo.SaveChangesAsync();

                if (insertResult)
                    return Ok(customerToDelete);
                else
                    return InternalServerError();
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
        }
    }
}
