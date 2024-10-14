
namespace E_Commerce.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Services

            builder.Services.AddCoreServices();
            builder.Services.AddInfraStructureServices(builder.Configuration);
            builder.Services.AddPresentationServices();

            #endregion

            var app = builder.Build();

            #region Pipelines

            app.UseCustomExceptionMiddleware();
            await app.SeedDbAsync();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthorization();
            app.MapControllers();

            app.Run(); 

            #endregion

        }
    }
}
