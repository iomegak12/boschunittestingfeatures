using Bosch.Libraries.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Bosch.Libraries.Api.Interfaces
{
    public interface ICustomersApiController
    {
        IActionResult GetCustomers();
        IActionResult SearchCustomers(string customerName);
        IActionResult GetCustomerDetail(int customerId);
        IActionResult SaveCustomerDetail(Customer customerDetail);
    }
}
