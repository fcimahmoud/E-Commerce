
namespace E_Commerce.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Services

            builder.Services.AddCoreServices(builder.Configuration);
            builder.Services.AddInfraStructureServices(builder.Configuration);
            builder.Services.AddPresentationServices();

            #endregion

            var app = builder.Build();

            #region Pipelines

            await app.SeedDbAsync();
            app.UseCustomExceptionMiddleware();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStaticFiles();
            app.UseCors("CORSPolicy");
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run(); 

            #endregion

        }
    }
}
