namespace ACME.SchoolManagement.Core.Domain.Contracts.Services
{
    public interface IService<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Save(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
