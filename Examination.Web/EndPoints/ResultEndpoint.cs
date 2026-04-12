using Examination.Services;
using Examination.Services.Exceptions;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Examination.Web.EndPoints;

public static class ResultEndpoints
{
    public static IEndpointRouteBuilder MapResultGroup(this IEndpointRouteBuilder endpoints)
    {
        return endpoints.MapGroup("/results");
    }

    public static IEndpointRouteBuilder MapResultEndpoints(this IEndpointRouteBuilder endpoints)
    {
        ArgumentNullException.ThrowIfNull(endpoints);

        var group = endpoints.MapResultGroup();

        group.MapGet("", GetAllResults);
        group.MapGet("/user/{userId:int}", GetResultsByUser);
        group.MapGet("/{attemptId:int}", GetResultByAttempt);

        return endpoints;
    }

    private static IResult GetAllResults(ResultService service)
    {
        var result = service.GetAllResults();
        return TypedResults.Ok(result);
    }

    private static IResult GetResultsByUser(ResultService service, int userId)
    {
        try
        {
            var result = service.GetResultsByUser(userId);
            return TypedResults.Ok(result);
        }
        catch (ConflictException ex)
        {
            return TypedResults.BadRequest(ex.Message);
        }
    }

    private static IResult GetResultByAttempt(ResultService service, int attemptId)
    {
        try
        {
            var result = service.GetResultByAttempt(attemptId);
            return TypedResults.Ok(result);
        }
        catch (ConflictException ex)
        {
            return TypedResults.BadRequest(ex.Message);
        }
    }
}