using Backend.API.IAM.Application.OutboundServices;
using Backend.API.IAM.Domain.Model.Queries;
using Backend.API.IAM.Domain.Services;
using Backend.API.IAM.Infrastructure.Pipeline.Middleware.Attributes;

namespace Backend.API.IAM.Infrastructure.Pipeline.Middleware.Components;

/// <summary>
/// RequestAuthorizationMiddleware is a custom middleware.
/// This middleware is used to authorize requests.
/// It validates a token is included in the request header and that the token is valid.
/// If the token is valid then it sets the user in HttpContext.Items["User"].
/// </summary>
public class RequestAuthorizationMiddleware
{
    private readonly RequestDelegate _next;

    public RequestAuthorizationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    /// <summary>
    /// Authorizes incoming HTTP requests based on the Authorization header and custom IAM attributes.
    /// </summary>
    public async Task InvokeAsync(
        HttpContext context,
        IUserQueryService userQueryService,
        ITokenService tokenService)
    {
        // 1. Si el endpoint tiene [AllowAnonymous], no hacemos nada
        var endpoint = context.Request.HttpContext.GetEndpoint();
        var allowAnonymous = endpoint?.Metadata
            .Any(m => m.GetType() == typeof(AllowAnonymousAttribute)) ?? false;

        if (allowAnonymous)
        {
            await _next(context);
            return;
        }

        // 2. Desde aquí: el endpoint requiere autenticación

        // Obtener header Authorization
        var authorizationHeader = context.Request.Headers["Authorization"].FirstOrDefault();

        if (string.IsNullOrWhiteSpace(authorizationHeader))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Null or invalid token");
            return;
        }

        // Debe venir como: "Bearer <token>"
        var parts = authorizationHeader.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length != 2 || !parts[0].Equals("Bearer", StringComparison.OrdinalIgnoreCase))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Null or invalid token");
            return;
        }

        var token = parts[1];

        if (string.IsNullOrWhiteSpace(token))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Null or invalid token");
            return;
        }

        // 3. Validar token
        int? userId;
        try
        {
            userId = await tokenService.ValidateToken(token);
        }
        catch
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Invalid token");
            return;
        }

        if (userId is null)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Invalid token");
            return;
        }

        // 4. Obtener usuario
        var getUserByIdQuery = new GetUserByIdQuery(userId.Value);
        var user = await userQueryService.Handle(getUserByIdQuery);

        if (user is null)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("User not found");
            return;
        }

        // 5. Guardar usuario en HttpContext para otros componentes
        context.Items["User"] = user;

        // 6. Continuar con el pipeline
        await _next(context);
    }
}
