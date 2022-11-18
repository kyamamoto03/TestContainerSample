using Microsoft.Extensions.DependencyInjection;
using Usecase;

namespace TestProject.Usecase;

public class UserUsecaseTest : IClassFixture<TestBase>
{
    private readonly TestBase _testBase;

    public UserUsecaseTest(TestBase testBase)
    {
        _testBase = testBase;
    }

    [Fact]
    public async Task 全ユーザ取得氏名Test()
    {
        var userUsecase = _testBase.ServiceProvider.GetService<IUserUsecase>();

        var users = await userUsecase!.GetAllAsync();
        Assert.NotEmpty(users);
        Assert.Equal("Testタロウ", users[0].Name);
        Assert.Equal("テスト花子", users[1].Name);
    }

    [Fact]
    public async Task ユーザ追加Test()
    {
        var userUsecase = _testBase.ServiceProvider.GetService<IUserUsecase>();

        await userUsecase!.AddAsync(new Model.User { Id = "003", Name = "あいうえお" });

        var users = await userUsecase!.GetAllAsync();
        Assert.NotEmpty(users);
        Assert.Equal("Testタロウ", users[0].Name);
        Assert.Equal("テスト花子", users[1].Name);
        Assert.Equal("あいうえお", users[2].Name);
    }
}
