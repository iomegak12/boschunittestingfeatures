using Bosch.Libraries.DAL.Interfaces;
using Bosch.Libraries.Models;
using Bosch.Libraries.ORM.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bosch.Libraries.DAL.Impl
{
    public class CustomersRepository : ICustomersRepository
    {
        private ICustomersContext customersContext = default(ICustomersContext);
        private const string INVALID_CUSTOMERS_CONTEXT = "Invalid Customers Contxt Specified!";
        public CustomersRepository(ICustomersContext customersContext)
        {
            if (customersContext == default(ICustomersContext))
                throw new ArgumentException(INVALID_CUSTOMERS_CONTEXT, "customersContext");

            this.customersContext = customersContext;
        }

        public bool AddNewEntity(Customer entityType)
        {
            var status = default(bool);

            if (this.customersContext != default(ICustomersContext))
            {
                this.customersContext.Customers.Add(entityType);

                status = this.customersContext.CommitChanges();
            }

            return status;
        }

        public void Dispose() => this.customersContext?.Dispose();

        public IEnumerable<Customer> GetAllEntities()
        {
            var customersList = default(IEnumerable<Customer>);

            if (this.customersContext != default(ICustomersContext))
                customersList = this.customersContext.Customers.ToList();

            return customersList;
        }

        public IEnumerable<Customer> GetCustomersByName(string customerName)
        {
            var filteredCustomersList = default(IEnumerable<Customer>);

            if (this.customersContext != default(ICustomersContext))
                filteredCustomersList =
                    this.customersContext.Customers.Where(
                        customer => customer.CustomerName.Contains(customerName)).ToList();

            return filteredCustomersList;
        }

        public Customer GetEntityByKey(int entityKey)
        {
            var filteredCustomer = default(Customer);

            if (this.customersContext != default(ICustomersContext))
                filteredCustomer = this.customersContext.Customers.Where(
                    customer => customer.CustomerId.Equals(entityKey)).FirstOrDefault();

            return filteredCustomer;
        }
    }
}
