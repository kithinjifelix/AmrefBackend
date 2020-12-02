using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using PG.SharedKernel.Model;

namespace PG.SharedKernel.Interfaces.Persistence
{
    public interface IReadOnlyRepository<T, in TId> : IDisposable where T : Entity<TId>
    {
        string ConnectionString { get; }

        IQueryable<T> GetAll();
        IQueryable<TC> GetAll<TC, TCId>() where TC : Entity<TCId>;
        IQueryable<T> GetAll(Expression<Func<T, bool>> predicate);
        IQueryable<TC> GetAll<TC, TCId>(Expression<Func<TC, bool>> predicate) where TC : Entity<TCId>;
        T GetById(TId id);
        TC GetById<TC, TCId>(TCId id) where TC : Entity<TCId>;
        IEnumerable<TC> ExecQuery<TC>(string selectStatement);
        IDbConnection GetConnection(bool open = true);
        void CloseConnection();
    }
}
