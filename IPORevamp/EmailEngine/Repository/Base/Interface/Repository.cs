using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EmailEngine.Base.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Internal;
using IPORevamp.Repository.Base;

namespace EmailEngine.Repository
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class Repository<TIPOEmailLog, TIPOEmailTemplate, TDbContext> : RepositoryBase<TIPOEmailLog, TIPOEmailTemplate> where TIPOEmailLog:IPOEmailLog where TIPOEmailTemplate:IPOEmailTemplate where TDbContext: DbContext
    {
        private readonly TDbContext _dbContextProvider;

        public Repository(TDbContext dbContextProvider)
        {
            _dbContextProvider = dbContextProvider;
        }


        /// <summary>
        /// get the current Entity
        /// <returns><see cref="DbSet{TEntity}"/></returns>
        /// </summary>
        public virtual DbSet<TIPOEmailLog> Table => _dbContextProvider.Set<TIPOEmailLog>();
        public virtual DbSet<TIPOEmailTemplate> TableTemplate => _dbContextProvider.Set<TIPOEmailTemplate>();

        /// <summary>
        /// get the entity <see cref="IQueryable{TEntity}"/>
        /// </summary>
        /// <returns></returns>


        public override TIPOEmailLog GetById(int id)
        {
            return Table.Find(id);
        }


        public override IQueryable<TIPOEmailTemplate> GetAllEmailTemplates()
        {
            return TableTemplate;
        }


        public override void ExecuteStoreprocedure(string storeprocedureName, params object[] parameters)
        {
            //_dbContextProvider.Database.ExecuteSqlCommand("EXEC " + storeprocedureName, parameters);
        }       
        
        public override async Task<EntityEntry<TIPOEmailLog>> InsertOrUpdateAsync(Expression<Func<TIPOEmailLog, bool>> predicate, TIPOEmailLog entity)
        {
            return !Table.Any(predicate) && entity.Id == 0
                ? await InsertAsync(entity)
                : await UpdateAsync(entity);
        }
        public override int SaveChanges()
        {
            return _dbContextProvider.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync()
        {
            return await _dbContextProvider.SaveChangesAsync();
        }

        public override EntityEntry<TIPOEmailLog> Insert(TIPOEmailLog entity)
        {
            return Table.Add(entity);
        }

        public override EntityEntry<TIPOEmailTemplate> Insert(TIPOEmailTemplate entity)
        {
            return TableTemplate.Add(entity);
        }       

        public override Task<EntityEntry<TIPOEmailTemplate>> InsertAsync(TIPOEmailTemplate entity)
        {
            return Task.FromResult(TableTemplate.Add(entity));
        }

        public override int InsertAndGetId(TIPOEmailLog entity)
        {
            entity = Insert(entity).Entity;
            //entity.IsTransient()
            //if (entity.IsTransient())
            //{
                _dbContextProvider.SaveChanges();
            //}

            return entity.Id;
        }

        public override async Task<int> InsertAndGetIdAsync(TIPOEmailLog entity)
        {
            var ent = await InsertAsync(entity);
            entity = ent.Entity;
            //if (entity.IsTransient())
            //{
                await _dbContextProvider.SaveChangesAsync();
            //}

            return entity.Id;
        }

        public override int InsertOrUpdateAndGetId(TIPOEmailTemplate entity)
        {
            entity = InsertOrUpdate(entity).Entity;
            //entity.IsTransient()
            //if (entity.IsTransient())
            //{
                _dbContextProvider.SaveChanges();
            //}

            return entity.Id;
        }

        public override async Task<int> InsertOrUpdateAndGetIdAsync(TIPOEmailTemplate entity)
        {
            var ent = await InsertOrUpdateAsync(entity);
            entity = ent.Entity;
            //if (entity.IsTransient())
            //{
                await _dbContextProvider.SaveChangesAsync();
            //}

            return entity.Id;
        }

        public override EntityEntry<TIPOEmailTemplate> Update(TIPOEmailTemplate entity)
        {
            AttachIfNot(entity);
            var ent = _dbContextProvider.Entry(entity);
            ent.State = EntityState.Modified;
            return ent;
        }

        public override Task<EntityEntry<TIPOEmailLog>> UpdateAsync(TIPOEmailLog entity)
        {
            AttachIfNot(entity);
            var ent = _dbContextProvider.Entry(entity);
            ent.State = EntityState.Modified;
            return Task.FromResult(ent);
        }

        public override void Delete(TIPOEmailLog entity)
        {
            Table.Remove(entity);
            _dbContextProvider.SaveChanges();
        }


        public override TIPOEmailLog Update(int id, Action<TIPOEmailLog> updateAction)
        {
            var entity = Table.Find(id);
            updateAction(entity);
            return entity;
        }

        public override void Delete(int id)
        {
            var email =  TableTemplate.FirstOrDefault(x => x.Id == id);
            TableTemplate.Remove(email);
            _dbContextProvider.SaveChanges();
        }

        public override Task DeleteAsync(int id)
        {
            return Task.Run(() => Delete(id));

        }


        public override void ExecuteRawSqlQuery(string storeprocedureName, object[] param)
        {
            //_dbContextProvider.Database.ExecuteSqlCommand("EXEC " + storeprocedureName, param);
        }


        public override async Task<TIPOEmailLog> UpdateAsync(int id, Func<TIPOEmailLog, Task> updateAction)
        {
            var entity = await Table.FindAsync(id);
            await updateAction(entity);
            return entity;
        }

        public override async Task<TIPOEmailLog> UpdateAsync(Expression<Func<TIPOEmailLog, bool>> predicate, Func<TIPOEmailLog, Task> updateAction)
        {
            var entity = await Table.FirstOrDefaultAsync(predicate);
            await updateAction(entity);
            return entity;
        }


        protected virtual void AttachIfNot(TIPOEmailTemplate entity)
        {
            if (!TableTemplate.Local.Contains(entity))
            {
                TableTemplate.Attach(entity);
            }
        }

        protected virtual void AttachIfNot(TIPOEmailLog entity)
        {
            if (!Table.Local.Contains(entity))
            {
                Table.Attach(entity);
            }
        }

        public override void Remove(int id)
        {
            var entity = Table.FirstOrDefault(x => x.Id == id);
            _dbContextProvider.Remove(entity);
        }

        public override void Remove(TIPOEmailLog entity)
        {
            _dbContextProvider.Remove(entity);
        }

        public override IQueryable<TIPOEmailLog> GetAllWithoutActive()
        {
            return Table;
        }

        public override async Task<TIPOEmailTemplate> UpdateAsync(Expression<Func<TIPOEmailTemplate, bool>> predicate, Func<TIPOEmailTemplate, Task> updateAction)
        {
            var emailTemlate = await TableTemplate.FirstOrDefaultAsync(predicate);
            await updateAction(emailTemlate);
            return emailTemlate;

        }

        public override EntityEntry<TIPOEmailLog> Update(TIPOEmailLog entity)
        {
            AttachIfNot(entity);
            var ent = _dbContextProvider.Entry(entity);
            ent.State = EntityState.Modified;
            return ent;
        }

        public override IQueryable<TIPOEmailLog> GetAll()
        {
            return Table;
        }

        public override IQueryable<TIPOEmailTemplate> GetEMailTemplate(Expression<Func<TIPOEmailTemplate, bool>> predicate)
        {
            return TableTemplate.Where(predicate);
        }

        public override async Task InsertRangeAsync(List<TIPOEmailLog> entities)
        {
            await Table.AddRangeAsync(entities);
        }
    }
}