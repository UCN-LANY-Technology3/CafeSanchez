
using CafeSanchez.API.Middleware;
using CafeSanchez.DataAccess;
using CafeSanchez.DataAccess.DAO;

namespace CafeSanchez.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string connectionString = builder.Configuration.GetConnectionString("Default") ?? throw new Exception("Connectionstring not found");

            // Add services to the container.
            builder.Services.AddScoped(_ => DaoFactory.Create<IProductDao>(connectionString));
            builder.Services.AddScoped(_ => DaoFactory.Create<IOrderDao>(connectionString));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseMiddleware<KeyAuthorizationMiddleware>();

            app.MapControllers();

            app.Run();
        }
    }
}
