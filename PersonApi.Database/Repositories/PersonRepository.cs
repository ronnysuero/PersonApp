using PersonApi.Domain.Entities;
using PersonApi.Domain.Interfaces;

namespace PersonApi.Database.Repositories;

public class PersonRepository : IPersonRepository
{
    private readonly MyDbContext _context;

    public PersonRepository(MyDbContext context) => _context = context;

    public async Task<Person?> GetByIdAsync(int id, CancellationToken cancellationToken) =>
        await _context.People.FindAsync(new object?[] { id }, cancellationToken: cancellationToken);

    public async Task<IEnumerable<Person>> GetAllAsync(CancellationToken cancellationToken) =>
        await _context.People.ToListAsync(cancellationToken: cancellationToken);

    public async Task AddAsync(Person person, CancellationToken cancellationToken)
    {
        await _context.People.AddAsync(person, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Person person, CancellationToken cancellationToken)
    {
        _context.People.Update(person);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var person = await _context.People.FindAsync(new object?[] { id }, cancellationToken: cancellationToken);

        if (person != null)
        {
            _context.People.Remove(person);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
