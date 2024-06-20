using ACME.SchoolManagement.Core.Domain.Contracts.Repositories;
using ACME.SchoolManagement.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ACME.SchoolManagement.Persistence.BaseRepository
{
    public class Repository<T>(SchoolContext context) : IRepository<T> where T : class
    {
        protected readonly SchoolContext _context = context;
        protected readonly DbSet<T> _dbSet = context.Set<T>();

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }
    }
}
