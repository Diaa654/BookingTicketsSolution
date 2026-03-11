using Domain.Contracts;

namespace BookingTeckets.web.Extensions
{
    public static class WebApplicationRegister
    {
        public static async Task SeedDataBaseAsync(this WebApplication app)
        {
            using var Scope = app.Services.CreateScope();
            var ObjectOfDataSeeding = Scope.ServiceProvider.GetRequiredService<IDataSeeding>();
            await ObjectOfDataSeeding.DataSeedAsync();
            
        }
    }
}
