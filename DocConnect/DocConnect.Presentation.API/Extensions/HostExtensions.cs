using DocConnect.Data.Abstraction.Seeder;

namespace DocConnect.Presentation.API.Extensions
{
    public static class HostExtensions
    {
        public static async Task InitializeDBContext(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var databaseInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();

                await databaseInitializer.InitializeAsync();
            }
        }
    }
}
