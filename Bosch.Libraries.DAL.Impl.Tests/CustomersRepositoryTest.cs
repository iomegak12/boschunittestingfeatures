using Autofac;
using Bosch.Libraries.DAL.Interfaces;
using Bosch.Libraries.Models;
using Bosch.Libraries.ORM.Interfaces;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Bosch.Libraries.DAL.Impl.Tests
{
    public class CustomersRepositoryTest
    {
        [Fact]
        public void ShouldGetAllEntitiesReturnCustomerRecords()
        {
            var mockRepository = new MockRepository(MockBehavior.Default);
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

            var mockCustomersContext = mockRepository.Create<ICustomersContext>();
            var mockDbSet = mockRepository.Create<DbSet<Customer>>();

            mockDbSet.As<IQueryable<Customer>>().Setup(m => m.Provider).Returns(
                mockCustomers.AsQueryable().Provider);
            mockDbSet.As<IQueryable<Customer>>().Setup(m => m.Expression).Returns(
                mockCustomers.AsQueryable().Expression);
            mockDbSet.As<IQueryable<Customer>>().Setup(m => m.ElementType).Returns(
                mockCustomers.AsQueryable().ElementType);
            mockDbSet.As<IQueryable<Customer>>().Setup(m => m.GetEnumerator()).Returns(
                mockCustomers.AsQueryable().GetEnumerator());

            mockCustomersContext
                .Setup(context => context.Customers)
                .Returns(mockDbSet.Object);

            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterInstance<ICustomersContext>(mockCustomersContext.Object);
            containerBuilder.RegisterType<CustomersRepository>().As<ICustomersRepository>();

            var container = containerBuilder.Build();

            using (var scope = container.BeginLifetimeScope())
            {
                var customersRepository = scope.Resolve<ICustomersRepository>();
                var entities = customersRepository.GetAllEntities();
                var expectedNoOfCustomers = 3;
                var actualCustomers = entities.Count();

                Assert.NotNull(customersRepository);
                Assert.NotNull(entities);
                Assert.Equal<int>(expectedNoOfCustomers, actualCustomers);
            }
        }
    }
}
