using System;
using System.Collections.Generic;

namespace Bosch.Libraries.DAL.Interfaces
{
    public interface IRepository<EntityType, EntityKeyType> : IDisposable
    {
        IEnumerable<EntityType> GetAllEntities();
        EntityType GetEntityByKey(EntityKeyType entityKey);
        bool AddNewEntity(EntityType entityType);
    }
}
