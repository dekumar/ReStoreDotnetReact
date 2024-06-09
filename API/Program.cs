
using API.Data;
using Microsoft.EntityFrameworkCore;

namespace API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        
        builder.Services.AddDbContext<StoreContext>(opt=>{
            opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
        });

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

        var scope = app.Services.CreateScope();
        var context=scope.ServiceProvider.GetRequiredService<StoreContext>();
        var logger=scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        try
        {
            context.Database.Migrate();
            DbInitializer.DbInitialize(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "a problem has occured during migration");
        }

        app.Run();
    }
}
