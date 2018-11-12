using Bosch.Libraries.Business.Interfaces;
using Bosch.Libraries.Business.Validations.Interfaces;
using Bosch.Libraries.DAL.Interfaces;
using Bosch.Libraries.Models;
using System;
using System.Collections.Generic;

namespace Bosch.Libraries.Business.Impl
{
    public class CustomersBusinessComponent : ICustomersBusinessComponent
    {
        private ICustomersRepository customersRepository = default(ICustomersRepository);
        private ICustomerNameValidation customerNameValidation = default(ICustomerNameValidation);
        private ICustomerValidation customerValidation = default(ICustomerValidation);

        private const string INVALID_CUSTOMERS_REPOSITORY = "Invalid Customers Repository Specified!";
        private const string INVALID_ARGUMENTS = "Invalid Argument(s) Specified!";
        private const string INVALID_SEARCH_STRING = "Invalid Search String Specified!";
        public CustomersBusinessComponent(ICustomersRepository customersRepository,
            ICustomerNameValidation customerNameValidation,
            ICustomerValidation customerValidation)
        {
            if (customersRepository == default(ICustomersRepository))
                throw new ArgumentException(INVALID_CUSTOMERS_REPOSITORY, "customersRepository");

            this.customersRepository = customersRepository;

            if (customerNameValidation == default(ICustomerNameValidation))
                throw new ArgumentException(INVALID_ARGUMENTS, "customerNameValidation");

            this.customerNameValidation = customerNameValidation;

            if (customerValidation == default(ICustomerValidation))
                throw new ArgumentException(INVALID_ARGUMENTS, "customerValidation");

            this.customerValidation = customerValidation;
        }

        public void Dispose() => this.customersRepository?.Dispose();

        public Customer GetCustomerDetail(int customerId)
        {
            var validation = customerId != default(int);

            if (!validation)
                throw new ArgumentNullException(INVALID_ARGUMENTS, "customerId");

            var filteredCustomer = this.customersRepository.GetEntityByKey(customerId);

            return filteredCustomer;
        }

        public IEnumerable<Customer> GetCustomers(string customerName = default(string))
        {
            var customersList = default(IEnumerable<Customer>);

            if (string.IsNullOrEmpty(customerName))
                customersList = this.customersRepository.GetAllEntities();
            else
            {
                var validation = this.customerNameValidation.Validate(customerName);

                if (!validation)
                    throw new ApplicationException(INVALID_SEARCH_STRING);

                customersList = this.customersRepository.GetCustomersByName(customerName);
            }

            return customersList;
        }

        public bool SaveNewCustomerDetail(Customer customerDetail)
        {
            var validation = customerDetail != default(Customer) &&
                this.customerValidation.Validate(customerDetail);

            if (!validation)
                throw new ArgumentException(INVALID_ARGUMENTS, "customerDetail");

            var saveStatus = this.customersRepository.AddNewEntity(customerDetail);

            return saveStatus;
        }
    }
}
