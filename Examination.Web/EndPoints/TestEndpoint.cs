using Examination.Core.Dtos;
using Examination.Core.Requests;
using Examination.Services;
using Examination.Services.Exceptions;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Examination.Web.EndPoints;

public static class TestEndpoints
{
    public static IEndpointRouteBuilder MapTestGroup(this IEndpointRouteBuilder endpoints)
    {
        return endpoints.MapGroup("/tests");
    }

    public static IEndpointRouteBuilder MapTestEndpoints(this IEndpointRouteBuilder endpoints)
    {
        ArgumentNullException.ThrowIfNull(endpoints);

        var group = endpoints.MapTestGroup();

        group.MapPost("", CreateTest);
        group.MapGet("/all", GetAllTests);
        group.MapGet("", GetPublishedTests);
        group.MapGet("/{id:int}", GetTestById);
        group.MapPut("/{id:int}/publish", PublishTest);
        group.MapDelete("/{id:int}", DeleteTest);

        return endpoints;
    }

    private static IResult CreateTest(TestService service, CreateTestRequest request)
    {
        try
        {
            var result = service.CreateTest(request);

            return TypedResults.Ok(result);
        }
        catch (ConflictException ex)
        {
            return TypedResults.BadRequest(ex.Message);
        }
    }

    private static IResult GetAllTests(TestService service)
    {
        var result = service.GetAllTests();
        return TypedResults.Ok(result);
    }

    private static IResult GetPublishedTests(TestService service)
    {
        var result = service.GetPublishedTests();
        return TypedResults.Ok(result);
    }

    private static IResult GetTestById(TestService service, int id)
    {
        try
        {
            var result = service.GetTestById(id);
            return TypedResults.Ok(result);
        }
        catch (NotFoundException ex)
        {
            return TypedResults.NotFound(ex.Message);
        }
    }

    private static IResult PublishTest(TestService service, int id)
    {
        try
        {
            service.PublishTest(id);
            return TypedResults.Ok("Test published successfully");
        }
        catch (NotFoundException ex)
        {
            return TypedResults.NotFound(ex.Message);
        }
        catch (ConflictException ex)
        {
            return TypedResults.BadRequest(ex.Message);
        }
    }

    private static IResult DeleteTest(TestService service, int id)
    {
        try
        {
            service.DeleteTest(id);
            return TypedResults.Ok("Test deleted successfully");
        }
        catch (NotFoundException ex)
        {
            return TypedResults.NotFound(ex.Message);
        }
        catch (ConflictException ex)
        {
            return TypedResults.BadRequest(ex.Message);
        }
    }
}