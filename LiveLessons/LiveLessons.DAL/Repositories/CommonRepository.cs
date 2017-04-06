using System;
using System.Data.Entity;
using System.Linq;
using LiveLessons.DAL.EF;
using LiveLessons.DAL.Entities;
using LiveLessons.DAL.Interfaces;

namespace LiveLessons.DAL.Repositories
{
    public class CommonRepository<TEntity> : IRepository<TEntity> where TEntity : BaseType
    {
        private readonly DatabaseContext db;

        public CommonRepository(DatabaseContext db)
        {
            this.db = db;
        }

        public IQueryable<TEntity> GetAll()
        {
            return db.Set<TEntity>();
        }

        public TEntity Get(int id)
        {
            return db.Set<TEntity>().Find(id);
        }

        public IQueryable<TEntity> Find(Func<TEntity, bool> predicate)
        {
            return db.Set<TEntity>().Where(predicate).AsQueryable();
        }

        public virtual void Create(TEntity item)
        {
            db.Set<TEntity>().Add(item);
        }

        public virtual void Update(TEntity item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public virtual void Delete(int id)
        {
            var item = Get(id);
            db.Set<TEntity>().Remove(item);
        }
    }
}
