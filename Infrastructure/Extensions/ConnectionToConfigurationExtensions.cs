using System;
using System.Text.Json;
using Infrastructure.DTO;
using Infrastructure.HelperModels;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Extensions
{
    public static class ConnectionToConfigurationExtensions
    {
        public static async Task AddConfigurationApiSource(this IConfigurationBuilder builder, IConfiguration configuration)
        {
            
            HttpClient httpClient = new HttpClient();
            UriEndPoint uriEndPoint = new UriEndPoint();
            uriEndPoint = configuration.GetSection("Configuration")
                                                     .Get<UriEndPoint>();
            httpClient.BaseAddress = new Uri(uriEndPoint.BaseAddress);
            
            var response = await httpClient.GetAsync(uriEndPoint.Uri);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            var configurationItemsList = JsonSerializer.Deserialize<Dictionary<Guid, ConfigurationItemDTO>>(responseBody);
            Dictionary<string, string> dictonary = new Dictionary<string, string>();
            foreach (var item in configurationItemsList)
            {
                dictonary.Add(item.Value.key, item.Value.value);
            }
            builder.Add(new DictionaryConfigurationSource(dictonary));
        }
    }
}
