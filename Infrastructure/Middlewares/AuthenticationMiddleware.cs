using Microsoft.AspNetCore.Http;
using System.Net.Http.Json;
using XAct;
using System.Text.Json;
using Microsoft.AspNetCore.Http.Features;
using Infrastructure.HelperModels;
using Infrastructure.Interfaces;
using Infrastructure.Attributes;
using Infrastructure.DTO;
using Microsoft.Extensions.Options;

namespace Infrastructure.Middlewares
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly UriEndPoint uriEndPoint;
        public AuthenticationMiddleware(RequestDelegate next,IOptions<UriEndPoint> options)
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
            var response = await _httpClient.PostAsync(uriEndPoint.Uri + roles, JsonContent.Create(""));
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            var session = JsonSerializer.Deserialize<UserSession>(responseBody);
            userSession.UserId = session.UserId;
            userSession.Role = session.Role;
            await _next(context);
        }

    }

}
