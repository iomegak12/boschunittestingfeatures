using Autofac;
using Bosch.Libraries.Business.Validations.Impl;
using Bosch.Libraries.Business.Validations.Interfaces;
using System;
using Xunit;

namespace Bosch.Libraries.Business.Validations.Tests
{
    public class CustomerNameValidationTests
    {
        [Fact]
        public void ShouldInvalidCustomerNameValidationReturnFalse()
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterType<CustomerValidation>().As<ICustomerValidation>();
            containerBuilder.RegisterType<CustomerNameValidation>().As<ICustomerNameValidation>();

            var container = containerBuilder.Build();

            using (var scope = container.BeginLifetimeScope())
            {
                var customerNameValidation = container.Resolve<ICustomerNameValidation>();
                var badKeyword = "bad";
                var expectedResult = false;
                var actualResult = customerNameValidation.Validate(badKeyword);

                Assert.Equal(expectedResult, actualResult);
            }
        }
    }
}
