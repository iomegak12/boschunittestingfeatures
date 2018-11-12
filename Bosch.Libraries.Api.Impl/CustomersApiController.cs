using Bosch.Libraries.Api.Interfaces;
using Bosch.Libraries.Business.Interfaces;
using Bosch.Libraries.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Bosch.Libraries.Api.Impl
{
    [Route("api/[controller]")]
    [Produces(contentType: "application/json")]
    [ApiController]
    public class CustomersController : Controller, ICustomersApiController
    {
        private ICustomersBusinessComponent customersBusinessComponent = default(ICustomersBusinessComponent);
        private const string INVALID_BUSINESS_COMPONENT = "Invalid Customers Business Component Specified!";
        private const int MIN_SEARCH_STR_LENGTH = 3;

        public CustomersController(ICustomersBusinessComponent customersBusinessComponent)
        {
            if (customersBusinessComponent == default(ICustomersBusinessComponent))
                throw new ArgumentException(INVALID_BUSINESS_COMPONENT);

            this.customersBusinessComponent = customersBusinessComponent;
        }

        [Route("detail/{customerId}")]
        [HttpGet]
        public IActionResult GetCustomerDetail(int customerId)
        {
            try
            {
                var validation = customerId != default(int);

                if (!validation)
                    return BadRequest();

                var filteredCustomer = this.customersBusinessComponent.GetCustomerDetail(customerId);

                return Ok(filteredCustomer);
            }
            catch (Exception exceptionObject)
            {
                return BadRequest(exceptionObject);
            }
        }

        [HttpGet]
        public IActionResult GetCustomers()
        {
            var customers = this.customersBusinessComponent.GetCustomers();

            return Ok(customers);
        }

        [HttpPost]
        public IActionResult SaveCustomerDetail([FromBody] Customer customerDetail)
        {
            try
            {
                var validation = customerDetail != default(Customer);

                if (!validation)
                    return BadRequest();

                var status = this.customersBusinessComponent.SaveNewCustomerDetail(customerDetail);

                return Ok(status);
            }
            catch (Exception exceptionObject)
            {
                return BadRequest(exceptionObject);
            }
        }

        [HttpGet]
        [Route("search/{customerName}")]
        public IActionResult SearchCustomers(string customerName)
        {
            try
            {
                var validation = !string.IsNullOrEmpty(customerName) &&
                    customerName.Length >= MIN_SEARCH_STR_LENGTH;

                if (!validation)
                    return BadRequest();

                var filteredCustomers = this.customersBusinessComponent.GetCustomers(customerName);

                return Ok(filteredCustomers);
            }
            catch (Exception exceptionObject)
            {
                return BadRequest(exceptionObject);
            }
        }
    }
}
