using Bosch.Libraries.Business.Validations.Interfaces;
using System;
using System.Linq;

namespace Bosch.Libraries.Business.Validations.Impl
{
    public class CustomerNameValidation : ICustomerNameValidation
    {
        public bool Validate(string modelObject)
        {
            var badKeywords = new string[]
            {
                "bad",
                "worse",
                "not good"
            };

            return !badKeywords.Contains(modelObject);
        }
    }
}
