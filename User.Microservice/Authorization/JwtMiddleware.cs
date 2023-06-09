﻿using Microsoft.Extensions.Options;
using User.Microservice.Helpers;
using User.Microservice.Services;

namespace User.Microservice.Authorization
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;
        public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
        {
            _next = next;
            _appSettings = appSettings.Value;
        }

        public async Task Invoke(HttpContext context, IUserService userService, IJwtUtils jwtUtils)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userId = jwtUtils.ValidateJwtToken(token!);            
            if (userId != null)
            {
                var userIdGuid = Guid.Parse(userId);
                // attach user to context on successful jwt validation
                context.Items["User"] = userService.GetById(userIdGuid);
            }

            await _next(context);
        }
    }
}
