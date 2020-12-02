using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using PG.SharedKernel.Interfaces.Persistence;
using PG.SharedKernel.Model;

namespace PG.SharedKernel.Infrastructure.Persistence
{
    public abstract class BaseReadOnlyRepository<T, TId> : IReadOnlyRepository<T, TId> where T : Entity<TId>
    {
        protected internal DbContext Context;
        protected internal readonly DbSet<T> DbSet;
        private IDbConnection _connection;

        public string ConnectionString => Context.Database.GetDbConnection().ConnectionString;

        protected BaseReadOnlyRepository(DbContext context)
        {
            Context = context;
            DbSet = Context.Set<T>();
        }
        public virtual IQueryable<T> GetAll()
        {
            return DbSet.AsNoTracking();
        }
        public virtual IQueryable<TC> GetAll<TC, TCId>() where TC : Entity<TCId>
        {
            return Context.Set<TC>().AsNoTracking();
        }
        public virtual IQueryable<T> GetAll(Expression<Func<T, bool>> predicate)
        {
            return GetAll().Where(predicate);
        }
        public virtual IQueryable<TC> GetAll<TC, TCId>(Expression<Func<TC, bool>> predicate) where TC : Entity<TCId>
        {
            return GetAll<TC, TCId>().Where(predicate);
        }
        public virtual T GetById(TId id)
        {
            return DbSet.Find(id);
        }
        public virtual TC GetById<TC, TCId>(TCId id) where TC : Entity<TCId>
        {
            return Context.Set<TC>().Find(id);
        }

        public virtual  IEnumerable<TC> ExecQuery<TC>(string selectStatement)
        {
            var entities =  GetConnection().Query<TC>(selectStatement);
            return entities;
        }
        public virtual IDbConnection GetConnection(bool open = true)
        {
            if (null == _connection)
            {
                if (Context.Database.IsSqlServer())
                    _connection = new SqlConnection(ConnectionString);

                if (Context.Database.IsSqlite())
                    _connection = new SqliteConnection(ConnectionString);
            }

            if (null == _connection)
                throw new Exception("Database provider error");

            if (_connection.State != ConnectionState.Open)
                _connection.Open();

            return _connection;
        }

        public void CloseConnection()
        {
            try
            {
                if (null != _connection && _connection.State == ConnectionState.Open)
                    _connection.Close();
            }
            catch
            {

            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                DisposeContext();
                DisposeConnection();
            }
        }
        private void DisposeContext()
        {
            Context?.Dispose();
            Context = null;
        }

        private void DisposeConnection()
        {
            _connection?.Dispose();
            _connection = null;
        }
    }
}
