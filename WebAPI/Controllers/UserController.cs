using Microsoft.AspNetCore.Mvc;
using Model;
using Usecase;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserUsecase userUsecase;

        public UserController(ILogger<UserController> logger, IUserUsecase userUsecase)
        {
            _logger = logger;
            this.userUsecase = userUsecase;
        }

        [HttpGet]
        public async Task<IEnumerable<User>> Get()
        {
            var users = await userUsecase.GetAllAsync();

            return users;
        }
    }
}