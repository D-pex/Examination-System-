using Examination.Core.Dtos;
using Examination.Core.Requests;
using Examination.Services.Exceptions;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Examination.Web.EndPoints;

public static class TestEndpoints
{
    // Create group
    public static IEndpointRouteBuilder MapTestGroup(this IEndpointRouteBuilder endpoints)
    {
        return endpoints.MapGroup("/tests");
    }

    // Map all endpoints
    public static IEndpointRouteBuilder MapTestEndpoints(this IEndpointRouteBuilder endpoints)
    {
        ArgumentNullException.ThrowIfNull(endpoints);

        var group = endpoints.MapTestGroup();

        group.MapPost("", CreateTest);
        group.MapGet("/all", GetAllTests);
        group.MapGet("", GetPublishedTests);
        group.MapGet("/{id:int}", GetTestById);
        group.MapPut("/{id:int}/publish", PublishTest);

        return endpoints;
    }

    private static IResult CreateTest(TestService service, CreateTestRequest request)
    {
        try
        {
            var result = service.CreateTest(request);

            return result is null
                ? TypedResults.Problem("Error while creating test.")
                : TypedResults.Ok(result);
        }
        catch (ConflictException ex)
        {
            return TypedResults.BadRequest(ex.Message);
        }
    }

    private static Ok<IEnumerable<TestDto>> GetAllTests(TestService service)
    {
        try
        {
            var result = service.GetAllTests();
            return TypedResults.Ok(result);
        }
        catch (ConflictException ex)
        {
            throw new Exception(ex.Message);
        }
    }

    private static Ok<IEnumerable<TestDto>> GetPublishedTests(TestService service)
    {
        try
        {
            var result = service.GetPublishedTests();
            return TypedResults.Ok<IEnumerable<TestDto>>(result);
        }
        catch (ConflictException ex)
        {
            throw new Exception(ex.Message);
        }
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
}