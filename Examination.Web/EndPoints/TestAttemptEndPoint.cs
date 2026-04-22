using Examination.Core.Requests;
using Examination.Services;
using Examination.Services.Exceptions;

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
        group.MapPost("/submit-all", SubmitAllAnswers);

        return endpoints;
    }

    private static IResult StartAttempt(
        TestAttemptService service,
        CreateUserAttemptRequest request)
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

    private static IResult SubmitAllAnswers(
        
        TestAttemptService service,
        CreateSubmitAllAnswerRequest request)
    {
        try
        {
            var result = service.SubmitAllAnswers(request);
            return TypedResults.Ok(result);
        }
        catch (ConflictException ex)
        {
            return TypedResults.BadRequest(ex.Message);
        }
    }
}