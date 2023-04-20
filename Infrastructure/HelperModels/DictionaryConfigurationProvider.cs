using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.HelperModels
{
    public class DictionaryConfigurationProvider : ConfigurationProvider
    {
        private readonly IDictionary<string, string> _data;

        public DictionaryConfigurationProvider(IDictionary<string, string> data)
            => _data = data;

        public override void Load() => Data = _data;
    }
}
