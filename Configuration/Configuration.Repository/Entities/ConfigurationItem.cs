using Infrastructure.HelperModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configuration.Repository.Entities
{
    public class ConfigurationItem:BaseEntity<Guid>
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public ConfigurationItem()
        {

        }
        public ConfigurationItem(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }
}
