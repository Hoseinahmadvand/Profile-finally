using Profile.Repository.Interfaces;
using Profile.Services.Interfaces;
using System.Linq.Expressions;
namespace Profile.Services.Implementations;

public class GenericService<T> : IGenericService<T> where T : class
{
    private readonly IGenericRepository<T> _repository;

    public GenericService(IGenericRepository<T> repository)
    {
        _repository = repository;
    }

    public async Task<T> GetByIdAsync(int id) => await _repository.GetByIdAsync(id);

    public async Task<IEnumerable<T>> GetAllAsync() => await _repository.GetAllAsync();

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate) => await _repository.FindAsync(predicate);

    public async Task AddAsync(T entity) => await _repository.AddAsync(entity);

    public async Task UpdateAsync(T entity) => await _repository.UpdateAsync(entity);

    public async Task DeleteAsync(T entity) => await _repository.DeleteAsync(entity);
}
