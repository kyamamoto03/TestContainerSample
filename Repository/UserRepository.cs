using Infra;
using Microsoft.EntityFrameworkCore;
using Model;

namespace Repository;

public interface IUserRepository
{
    Task<IList<User>> GetAllAsync();
    Task AddAsync(User user);
}
public class UserRepository : IUserRepository
{
    private readonly SampleDbContext sampleDbContext;

    public UserRepository(SampleDbContext sampleDbContext)
    {
        this.sampleDbContext = sampleDbContext;
    }

    public async Task<IList<User>> GetAllAsync()
    {
        return await sampleDbContext.Users.OrderBy(x => x.Id).ToListAsync();
    }

    public async Task AddAsync(User user)
    {
        sampleDbContext.Users.Add(user);
        await sampleDbContext.SaveChangesAsync();
    }
}
