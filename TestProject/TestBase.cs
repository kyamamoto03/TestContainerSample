using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using Infra;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Repository;
using Usecase;

namespace TestProject;

public class TestBase : IDisposable
{
    private PostgreSqlTestcontainer? _postgresqlContainer;

    private IServiceCollection _services { get; } = new ServiceCollection();
    internal ServiceProvider ServiceProvider { get; }

    string UnitTestConnectionString => $"Server=localhost;Port={_postgresqlContainer.Port:d};Database=test;User Id=postgres;Password=postgres;";
    public TestBase()
    {

        #region DI(DB)
        _services.AddDbContext<SampleDbContext>(Options =>
           {
               Options.UseNpgsql(UnitTestConnectionString);
           });

        #endregion

        #region DI(Repository)
        _services.AddScoped<IUserRepository, UserRepository>();
        #endregion

        #region DI(Repository
        _services.AddScoped<IUserUsecase, UserUsecase>();
        #endregion

        ServiceProvider = _services.BuildServiceProvider();
        Initialize();
    }

    public void Initialize()
    {
        Task.Run(InitializeAsync).GetAwaiter().GetResult();
    }
    public async Task InitializeAsync()
    {
        try
        {
            _postgresqlContainer = new TestcontainersBuilder<PostgreSqlTestcontainer>()
                     .WithDatabase(new PostgreSqlTestcontainerConfiguration
                     {
                         Database = "db",
                         Username = "postgres",
                         Password = "postgres",
                     })
                       .WithImage("docker.io/postgres:14.4")
                       .WithBindMount(Dir, @"/docker-entrypoint-initdb.d")
                     .Build();
        }
        catch (Exception ex)
        {
            throw;
        }
        if (_postgresqlContainer == null)
        {
            throw new Exception("TestcontainersBuilderの作成エラー");
        }
        await _postgresqlContainer.StartAsync();
    }
    public void Dispose()
    {
        Task.Run(_postgresqlContainer!.DisposeAsync).GetAwaiter().GetResult();
    }

    private string Dir
    {
        get
        {
            var dir = System.Environment.CurrentDirectory;
            return $"{dir}/../../../../UT/postgresql/init";
        }
    }

}
