using System;

namespace Bosch.Libraries.ORM.Interfaces
{
    public interface ISystemContext : IDisposable
    {
        bool CommitChanges();
    }
}
