using Bosch.Libraries.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bosch.Libraries.ORM.Interfaces
{
    public interface ICustomersContext : ISystemContext
    {
        DbSet<Customer> Customers { get; set; }
    }
}
