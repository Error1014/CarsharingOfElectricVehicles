using Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using XAct.Messages;

namespace Infrastructure.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;
            var message = string.Empty;
            switch (exception)
            {
                case NotFoundException notFoundException:
                    code = HttpStatusCode.NotFound;
                    message = notFoundException.Message;
                    break;
                case BadRequestException badRequestException:
                    code = HttpStatusCode.BadRequest;
                    message = badRequestException.Message;
                    break;
                case UnauthorizedException unauthorizedException:
                    code = HttpStatusCode.Unauthorized;
                    message = unauthorizedException.Message;
                    break;
                case ForbiddenException unauthorizedException:
                    code = HttpStatusCode.Forbidden;
                    message = unauthorizedException.Message;
                    break;
                default:
                    message = exception.Message;
                    break;
            }


            var result = JsonSerializer.Serialize(new { error = message , code = code});
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}
