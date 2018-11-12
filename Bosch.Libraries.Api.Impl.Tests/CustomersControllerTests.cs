using Autofac;
using Bosch.Libraries.Api.Interfaces;
using Bosch.Libraries.Business.Interfaces;
using Bosch.Libraries.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Bosch.Libraries.Api.Impl.Tests
{
    public class CustomersControllerTests
    {
        [Fact]
        public void ShouldRouteGetCustomersReturnAllRecordsInOkResult()
        {
            var mockRepository = new MockRepository(MockBehavior.Default);
            var customersBusinessComponentMock = mockRepository.Create<ICustomersBusinessComponent>();
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

            customersBusinessComponentMock
                .Setup(businessComponent => businessComponent.GetCustomers(null))
                .Returns(mockCustomers);

            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterInstance<ICustomersBusinessComponent>(customersBusinessComponentMock.Object);
            containerBuilder.RegisterType<CustomersController>()
                .As<ICustomersApiController>();

            var container = containerBuilder.Build();

            using (var scope = container.BeginLifetimeScope())
            {
                var controller = scope.Resolve<ICustomersApiController>();
                var actionResult = controller.GetCustomers();
                var okResult = actionResult as OkObjectResult;

                Assert.NotNull(actionResult);
                Assert.NotNull(okResult);

                var model = okResult.Value as IEnumerable<Customer>;

                Assert.NotNull(model);

                var expectedNoOfCustomers = 3;
                var actualNoOfCustomers = model.Count();

                Assert.Equal(expectedNoOfCustomers, actualNoOfCustomers);
            }
        }
    }
}
