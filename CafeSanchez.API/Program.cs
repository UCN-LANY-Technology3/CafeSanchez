
using CafeSanchez.API.Middleware;
using CafeSanchez.DataAccess;
using CafeSanchez.DataAccess.DAO;
using CafeSanchez.DataAccess.Entities;

namespace CafeSanchez.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string connectionString = "Server=192.168.56.101; Database=CafeSanchez; User Id=sa; Password=P@$$w0rd; TrustServerCertificate=True";

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddScoped(_ => DaoFactory.Create<IProductDao>(connectionString));

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
