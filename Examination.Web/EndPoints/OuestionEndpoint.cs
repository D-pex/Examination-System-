using Examination.Core.Requests;
using Examination.Services;
using Examination.Services.Exceptions;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Examination.Web.EndPoints;

public static class QuestionEndpoints
{
    public static IEndpointRouteBuilder MapQuestionGroup(this IEndpointRouteBuilder endpoints)
    {
        return endpoints.MapGroup("/questions");
    }

    public static IEndpointRouteBuilder MapQuestionEndpoints(this IEndpointRouteBuilder endpoints)
    {
        ArgumentNullException.ThrowIfNull(endpoints);

        var group = endpoints.MapQuestionGroup();

        group.MapPost("", CreateQuestion);
        group.MapGet("/{testId:int}", GetQuestionsByTestId);
        group.MapDelete("/{id:int}", DeleteQuestion);

        return endpoints;
    }

    private static IResult CreateQuestion(QuestionService service, CreateQuestionRequest request)
    {
        try
        {
            var result = service.CreateQuestion(request);
            return TypedResults.Ok(result);
        }
        catch (ConflictException ex)
        {
            return TypedResults.BadRequest(ex.Message);
        }
    }

    private static IResult GetQuestionsByTestId(QuestionService service, int testId)
    {
        try
        {
            var result = service.GetQuestionsByTestId(testId);
            return TypedResults.Ok(result);
        }
        catch (ConflictException ex)
        {
            return TypedResults.BadRequest(ex.Message);
        }
    }

    private static IResult DeleteQuestion(QuestionService service, int id)
    {
        try
        {
            service.DeleteQuestion(id);
            return TypedResults.Ok("Question deleted successfully");
        }
        catch (ConflictException ex)
        {
            return TypedResults.BadRequest(ex.Message);
        }
    }
}