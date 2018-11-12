using Autofac;
using Bosch.Libraries.Business.Interfaces;
using Bosch.Libraries.Business.Validations.Interfaces;
using Bosch.Libraries.DAL.Interfaces;
using Bosch.Libraries.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Bosch.Libraries.Business.Impl.Tests
{
    public class CustomersBusinessComponentTests
    {
        [Fact]
        public void ShouldGetCustomersByNameFilterResults()
        {
            var mockRepository = new MockRepository(MockBehavior.Default);
            var customersRepositoryMock = mockRepository.Create<ICustomersRepository>();
            var customerNameValidationMock = mockRepository.Create<ICustomerNameValidation>();
            var customerValidationMock = mockRepository.Create<ICustomerValidation>();

            var searchString = "wind";
            var mockCustomers = new List<Customer>
            {
                new Customer
                {
                    CustomerId = 1,
                    CustomerName = "Northwind",
                    Address = "Bangalore",
                    CreditLimit = 23000,
                    ActiveStatus = true,
                    Remarks = "Simple Customer Record"
                },
                new Customer
                {
                    CustomerId = 2,
                    CustomerName = "Southwind",
                    Address = "Bangalore",
                    CreditLimit = 23000,
                    ActiveStatus = true,
                    Remarks = "Simple Customer Record"
                },
                new Customer
                {
                    CustomerId = 3,
                    CustomerName = "Eastwind",
                    Address = "Bangalore",
                    CreditLimit = 23000,
                    ActiveStatus = false,
                    Remarks = "Simple Customer Record"
                }
            };

            customersRepositoryMock
                .Setup(repository => repository.GetCustomersByName(searchString))
                .Returns(mockCustomers);

            customerNameValidationMock
                .Setup(validation => validation.Validate(searchString))
                .Returns(true);

            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterInstance<ICustomersRepository>(customersRepositoryMock.Object);
            containerBuilder.RegisterInstance<ICustomerNameValidation>(customerNameValidationMock.Object);
            containerBuilder.RegisterInstance<ICustomerValidation>(customerValidationMock.Object);

            containerBuilder.RegisterType<CustomersBusinessComponent>()
                .As<ICustomersBusinessComponent>();

            var container = containerBuilder.Build();

            using (var scope = container.BeginLifetimeScope())
            {
                var businessComponent = scope.Resolve<ICustomersBusinessComponent>();
                var filteredCustomers = businessComponent.GetCustomers(searchString);
                var expectedNoOfCustomers = 3;
                var actualNoOfCustomers = filteredCustomers.Count();
                var expectedFirstCustomerName = "Northwind";
                var actualFirstCustomerName = filteredCustomers.FirstOrDefault().CustomerName;

                Assert.NotNull(filteredCustomers);
                Assert.Equal<int>(expectedNoOfCustomers, actualNoOfCustomers);
                Assert.Equal(expectedFirstCustomerName, actualFirstCustomerName);
            }
        }
    }
}
