using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using PG.SharedKernel.Interfaces.Persistence;
using PG.SharedKernel.Model;

namespace PG.SharedKernel.Infrastructure.Persistence
{
    public abstract class BaseRepository<T, TId> : BaseReadOnlyRepository<T, TId>, IRepository<T, TId>
        where T : Entity<TId>
    {
        protected BaseRepository(DbContext context) : base(context)
        {
        }

        public virtual void Create(T entity)
        {
            DbSet.Add(entity);
        }
        public virtual void Create<TC, TCId>(TC entity) where TC : Entity<TCId>
        {
            Context.Set<TC>().Add(entity);
        }
        public virtual void Create(IEnumerable<T> entities)
        {
            DbSet.AddRange(entities);
        }
        public virtual void Create<TC, TCId>(IEnumerable<TC> entities) where TC : Entity<TCId>
        {
            Context.Set<TC>().AddRange(entities);
        }

        public virtual void Update(T entity)
        {
            Context.Attach(entity);
            Context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Update<TC, TCId>(TC entity) where TC : Entity<TCId>
        {
            Context.Attach(entity);
            Context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            Delete(new List<T> {entity});
        }

        public virtual void Delete<TC, TCId>(TC entity) where TC : Entity<TCId>
        {
            Delete<TC, TCId>(new List<TC> {entity});
        }

        public virtual void DeleteById(TId id)
        {
            var entity = GetById(id);
            if (null != entity)
                Delete(entity);
        }

        public virtual void DeleteById<TC, TCId>(TCId id) where TC : Entity<TCId>
        {
            var entity = GetById<TC, TCId>(id);
            if (null != entity)
                Delete<TC, TCId>(entity);
        }

        public virtual void Delete(IEnumerable<T> entities)
        {
            DbSet.RemoveRange(entities);
        }

        public virtual void Delete<TC, TCId>(IEnumerable<TC> entities) where TC : Entity<TCId>
        {
            Context.Set<TC>().RemoveRange(entities);
        }

        public virtual void DeleteById(IEnumerable<TId> ids)
        {
            var entities = DbSet.Where(x => ids.Contains(x.Id));
            if (entities.Any())
                Delete(entities);
        }

        public virtual void DeleteById<TC, TCId>(IEnumerable<TCId> ids) where TC : Entity<TCId>
        {
            var entities = Context.Set<TC>().Where(x => ids.Contains(x.Id));
            if (entities.Any())
                Delete<TC, TCId>(entities);
        }

        public virtual void ExecCommand(string sqlCommand)
        {
            GetConnection().Execute(sqlCommand);
        }

        public Task Save()
        {
           return Context.SaveChangesAsync();
        }
    }
}
