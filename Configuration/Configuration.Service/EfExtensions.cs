using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Configuration.Service
{
    public static class EfExtensions
    {
        public static async Task<IConfigurationBuilder> AddEfConfiguration(this IConfigurationBuilder builder,
        Action<DbContextOptionsBuilder> optionsAction)
        {
            return builder.Add(new EfConfigurationSource(optionsAction));
        }
    }
}
