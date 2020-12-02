using System.Collections.Generic;
using System.Threading.Tasks;
using PG.SharedKernel.Model;

namespace PG.SharedKernel.Interfaces.Persistence
{
    public interface IRepository<T, in TId> : IReadOnlyRepository<T, TId> where T : Entity<TId>
    {
        void Create(T entity);
        void Create<TC, TCId>(TC entity) where TC : Entity<TCId>;

        void Create(IEnumerable<T> entities);
        void Create<TC, TCId>(IEnumerable<TC> entities) where TC : Entity<TCId>;

        void Update(T entity);
        void Update<TC, TCId>(TC entity) where TC : Entity<TCId>;

        void Delete(T entity);
        void Delete<TC, TCId>(TC entity) where TC : Entity<TCId>;

        void DeleteById(TId id);
        void DeleteById<TC, TCId>(TCId id) where TC : Entity<TCId>;

        void Delete(IEnumerable<T> entities);
        void Delete<TC, TCId>(IEnumerable<TC> entities) where TC : Entity<TCId>;

        void DeleteById(IEnumerable<TId> ids);
        void DeleteById<TC, TCId>(IEnumerable<TCId> ids) where TC : Entity<TCId>;

        void ExecCommand(string sqlCommand);
        Task Save();
    }
}
