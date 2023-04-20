using Configuration.Repository.Context;
using Configuration.Repository.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Configuration.Service
{
    public class EfConfigurationProvider : ConfigurationProvider
    {
        public Action<DbContextOptionsBuilder> OptionsAction { get; }

        public EfConfigurationProvider(Action<DbContextOptionsBuilder> optionsAction)
        {
            OptionsAction = optionsAction;
        }

        public override void Load()
        {
            var builder = new DbContextOptionsBuilder<ConfigurationContext>();

            OptionsAction(builder);

            using (var dbContext = new ConfigurationContext(builder.Options))
            {


                Data = !dbContext.ConfigurationItem.Any()
                    ? CreateAndSaveDefaultValues(dbContext)
                    : dbContext.ConfigurationItem.ToDictionary(c => c.Key, c => c.Value);
            }
        }
        private static IDictionary<string, string> CreateAndSaveDefaultValues(
        ConfigurationContext dbContext)
        {
            var configValues = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {

            };


            dbContext.ConfigurationItem.AddRange(configValues
                .Select(kvp => new ConfigurationItem
                {
                    Key = kvp.Key,
                    Value = kvp.Value
                })
                .ToArray());

            dbContext.SaveChanges();

            return configValues;
        }
    }
}
