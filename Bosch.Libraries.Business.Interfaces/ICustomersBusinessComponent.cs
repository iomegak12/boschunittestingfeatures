using Bosch.Libraries.Models;
using System;
using System.Collections.Generic;

namespace Bosch.Libraries.Business.Interfaces
{
    public interface ICustomersBusinessComponent : IDisposable
    {
        IEnumerable<Customer> GetCustomers(string customerName = default(string));
        Customer GetCustomerDetail(int customerId);
        bool SaveNewCustomerDetail(Customer customerDetail);
    }
}
