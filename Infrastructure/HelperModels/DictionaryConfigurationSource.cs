using Microsoft.Extensions.Configuration;

namespace Infrastructure.HelperModels
{
    public class DictionaryConfigurationSource : IConfigurationSource
    {
        private readonly IDictionary<string, string> _data;

        public DictionaryConfigurationSource(IDictionary<string, string> data)
            => _data = data;

        public IConfigurationProvider Build(IConfigurationBuilder builder)
            => new DictionaryConfigurationProvider(_data);
    }
}
