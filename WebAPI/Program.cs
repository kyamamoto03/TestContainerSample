using Infra;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Repository;
using Usecase;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region DB
builder.Services.AddDbContext<SampleDbContext>(Options =>
{
    Options.UseNpgsql("Server=localhost;Port=5555;Database=test;User Id=user;Password=pass;");
});

#endregion

#region DI(Repository)
builder.Services.AddScoped<IUserRepository, UserRepository>();
#endregion

#region DI(Repository
builder.Services.AddScoped<IUserUsecase, UserUsecase>();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
