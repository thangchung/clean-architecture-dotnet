using System;

namespace N8T.Core.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        void Rollback();
    }
}
