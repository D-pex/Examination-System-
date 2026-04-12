using Examination.Core.Requests;
using Examination.Services;
using Examination.Services.Exceptions;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Examination.Web.EndPoints;

public static class TestAttemptEndpoints
{
    public static IEndpointRouteBuilder MapTestAttemptGroup(this IEndpointRouteBuilder endpoints)
    {
        return endpoints.MapGroup("/attempts");
    }

    public static IEndpointRouteBuilder MapTestAttemptEndpoints(this IEndpointRouteBuilder endpoints)
    {
        ArgumentNullException.ThrowIfNull(endpoints);

        var group = endpoints.MapTestAttemptGroup();

        group.MapPost("/start", StartAttempt);
        group.MapPost("/answer", SubmitAnswer);
        group.MapPost("/{attemptId:int}/submit", SubmitTest);

        return endpoints;
    }

    private static IResult StartAttempt(TestAttemptService service, CreateUserRequest request)
    {
        try
        {
            var result = service.StartAttempt(request);
            return TypedResults.Ok(result);
        }
        catch (ConflictException ex)
        {
            return TypedResults.BadRequest(ex.Message);
        }
    }

    private static IResult SubmitAnswer(TestAttemptService service, CreateSubmitAnswerRequest request)
    {
        try
        {
            service.SubmitAnswer(request);
            return TypedResults.Ok("Answer submitted successfully");
        }
        catch (ConflictException ex)
        {
            return TypedResults.BadRequest(ex.Message);
        }
    }

    private static IResult SubmitTest(TestAttemptService service, int attemptId)
    {
        try
        {
            var result = service.SubmitTest(attemptId);
            return TypedResults.Ok(result);
        }
        catch (ConflictException ex)
        {
            return TypedResults.BadRequest(ex.Message);
        }
    }
}