using Examination.Core.Requests;
using Examination.Services;
using Examination.Services.Exceptions;

namespace Examination.Web.EndPoints;

public static class UserEndpoints
{
    public static IEndpointRouteBuilder MapUserGroup(this IEndpointRouteBuilder endpoints)
    {
        return endpoints.MapGroup("/users");
    }

    public static IEndpointRouteBuilder MapUserEndpoints(this IEndpointRouteBuilder endpoints)
    {
        ArgumentNullException.ThrowIfNull(endpoints);

        var group = endpoints.MapUserGroup();

        group.MapPost("/register", RegisterUser);
        group.MapPost("/login", LoginUser);

        return endpoints;
    }

    private static IResult RegisterUser(UserService service, CreateUserRequest request)
    {
        try
        {
            var result = service.Register(request);
            return TypedResults.Ok(result);
        }
        catch (ConflictException ex)
        {
            return TypedResults.BadRequest(ex.Message);
        }
    }

    private static IResult LoginUser(UserService service, CreateLoginRequest request)
    {
        try
        {
            var result = service.Login(request.Email, request.Password);
            return TypedResults.Ok(result);
        }
        catch (ConflictException ex)
        {
            return TypedResults.BadRequest(ex.Message);
        }
    }
}