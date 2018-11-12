using Bosch.Libraries.Business.Validations.Interfaces;
using Bosch.Libraries.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bosch.Libraries.Business.Validations.Impl
{
    public class CustomerValidation : ICustomerValidation
    {
        private const int MIN_CREDIT = 1;
        public bool Validate(Customer modelObject)
        {
            var validationStatus = modelObject != default(Customer) &&
                modelObject.CustomerId != default(int) &&
                !string.IsNullOrEmpty(modelObject.CustomerName) &&
                modelObject.CreditLimit >= MIN_CREDIT;

            return validationStatus;
        }
    }
}
