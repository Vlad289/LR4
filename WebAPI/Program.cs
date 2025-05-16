
using DesignStudio.BusinessLogic;
using DesignStudio.Data.Repositories;
using DesignStudio.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Diagnostics;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // --- 1. Реєструємо DbContext (підключення до бази даних)
            builder.Services.AddDbContext<DesignStudioContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // --- 2. Реєструємо UnitOfWork (репозиторії)
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            // --- 3. Реєструємо бізнес-логіку (сервіси)
            builder.Services.AddScoped<IDesignService, DesignService>();

            // --- 4. Реєструємо контролери
            builder.Services.AddControllers();

            // --- 5. Додаємо Swagger для тестування API (опціонально)
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Обробка глобальних помилок, щоб не було 500 без пояснень
            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    var errorFeature = context.Features.Get<IExceptionHandlerFeature>();
                    var logger = app.Services.GetRequiredService<ILogger<Program>>();
                    if (errorFeature != null)
                    {
                        logger.LogError(errorFeature.Error, "Unhandled exception");
                    }
                    context.Response.StatusCode = 500;
                    await context.Response.WriteAsync("Internal server error. Деталі у логах.");
                });
            });

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
