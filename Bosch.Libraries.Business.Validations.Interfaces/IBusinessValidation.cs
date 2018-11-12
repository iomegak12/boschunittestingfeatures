using System;

namespace Bosch.Libraries.Business.Validations.Interfaces
{
    public interface IBusinessValidation<ModelType>
    {
        bool Validate(ModelType modelObject);
    }
}
