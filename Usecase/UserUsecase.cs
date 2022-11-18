using Model;
using Repository;

namespace Usecase;

public interface IUserUsecase
{
    Task<IList<User>> GetAllAsync();
    Task AddAsync(User user);
}
public class UserUsecase : IUserUsecase
{
    private readonly IUserRepository userRepository;

    public UserUsecase(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public Task<IList<User>> GetAllAsync()
    {
        return userRepository.GetAllAsync();
    }

    public async Task AddAsync(User user)
    {
        await userRepository.AddAsync(user);
    }
}
