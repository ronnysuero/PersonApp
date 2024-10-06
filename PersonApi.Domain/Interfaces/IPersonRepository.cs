using PersonApi.Domain.Entities;

namespace PersonApi.Domain.Interfaces;

public interface IPersonRepository
{
    Task<Person> GetByIdAsync(int id, CancellationToken cancellationToken);

    Task<IEnumerable<Person>> GetAllAsync(CancellationToken cancellationToken);

    Task AddAsync(Person person, CancellationToken cancellationToken);

    Task UpdateAsync(Person person, CancellationToken cancellationToken);

    Task DeleteAsync(int id, CancellationToken cancellationToken);
}
