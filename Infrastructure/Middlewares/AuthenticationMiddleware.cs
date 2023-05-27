using Infrastructure.Attributes;
using Infrastructure.DTO;
using Infrastructure.HelperModels;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using System.Text.Json;

namespace Infrastructure.Middlewares
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly UriEndPoint uriEndPoint;
        public AuthenticationMiddleware(RequestDelegate next, IOptions<UriEndPoint> options)
        {
            _next = next;
            uriEndPoint = options.Value;
        }

        public async Task Invoke(HttpContext context, IUserSessionSetter userSession)
        {
            HttpClient _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(uriEndPoint.BaseAddress);
            var endpoint = context.Features.Get<IEndpointFeature>()?.Endpoint;
            var attribute = endpoint?.Metadata.GetMetadata<RoleAuthorizeAttribute>();
            var roles = attribute?.Roles;
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var response = new HttpResponseMessage();
            if (roles == null)
            {
                response = await _httpClient.PostAsync($"{uriEndPoint.Uri}", JsonContent.Create(""));
            }
            else
            {
                response = await _httpClient.PostAsync($"{uriEndPoint.Uri}?role={roles}", JsonContent.Create(""));
            }

            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var session = JsonSerializer.Deserialize<UserSession>(responseBody, options);
            userSession.UserId = session.UserId;
            userSession.Role = session.Role;
            await _next(context);
        }

    }

}
