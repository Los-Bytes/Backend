using Backend.API.IAM.Application.ACL.Services;
using Backend.API.IAM.Application.Internal.CommandServices;
using Backend.API.IAM.Application.Internal.QueryServices;
using Backend.API.IAM.Application.OutboundServices;
using Backend.API.IAM.Domain.Repositories;
using Backend.API.IAM.Domain.Services;
using Backend.API.IAM.Infrastructure.Hashing.BCrypt.Services;
using Backend.API.IAM.Infrastructure.Persistence.EFC.Repositories;
using Backend.API.IAM.Infrastructure.Tokens.JWT.Configuration;
using Backend.API.IAM.Infrastructure.Tokens.JWT.Services;
using Backend.API.IAM.Interfaces.ACL;

namespace Backend.API.IAM.Infrastructure.Interfaces.ASP.Configuration.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void AddIamContextServices(this WebApplicationBuilder builder)
    {
        // TokenSettings Configuration

        builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("TokenSettings"));

        // IAM Bounded Context Injection Configuration

        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IUserCommandService, UserCommandService>();
        builder.Services.AddScoped<IUserQueryService, UserQueryService>();
        builder.Services.AddScoped<ITokenService, TokenService>();
        builder.Services.AddScoped<IHashingService, HashingService>();
        builder.Services.AddScoped<IIamContextFacade, IamContextFacade>();
    }
}