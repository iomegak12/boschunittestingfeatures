using Bosch.Libraries.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bosch.Libraries.DAL.Interfaces
{
    public interface ICustomersRepository : IRepository<Customer, int>
    {
        IEnumerable<Customer> GetCustomersByName(string customerName);
    }
}
