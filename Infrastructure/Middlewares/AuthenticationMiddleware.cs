using Infrastructure.Attributes;
using Infrastructure.DTO;
using Infrastructure.Exceptions;
using Infrastructure.HelperModels;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using XAct;

namespace Infrastructure.Middlewares
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly UriEndPoint _authorizeEndPoint;
        private readonly IConfiguration _configuration;
        public AuthenticationMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
            _authorizeEndPoint = configuration.GetSection("EndPoint:AuthorizationService").Get<UriEndPoint>();
        }

        public async Task Invoke(HttpContext context, IUserSessionSetter userSession)
        {
            HttpClient _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_authorizeEndPoint.BaseAddress);
            var endpoint = context.Features.Get<IEndpointFeature>()?.Endpoint;
            var attribute = endpoint?.Metadata.GetMetadata<RoleAuthorizeAttribute>();
            var roles = attribute?.Roles;

            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (attribute != null || !token.IsNullOrEmpty())
            {
                _httpClient.DefaultRequestHeaders.Accept.Clear();
                _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                var response = new HttpResponseMessage();
                if (token == null && !roles.IsNullOrEmpty())
                {
                    throw new UnauthorizedException("Вы не авторизованы");
                }
                if (roles == null)
                {
                    response = await _httpClient.PostAsync($"{_authorizeEndPoint.Uri}", JsonContent.Create(""));
                }
                else
                {
                    response = await _httpClient.PostAsync($"{_authorizeEndPoint.Uri}?role={roles}", JsonContent.Create(""));
                }

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new UnauthorizedException("Вы не авторизованы");
                }
                else if (response.StatusCode == HttpStatusCode.Forbidden)
                {
                    throw new ForbiddenException("Вам недоступен данный ресурс");
                }
                //response.EnsureSuccessStatusCode();

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
            else
            {
                await _next(context);
            }
            
        }

    }

}
