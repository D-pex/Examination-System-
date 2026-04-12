using Examination.Services;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Examination.Web.EndPoints;

public static class ReportEndpoints
{
    public static IEndpointRouteBuilder MapReportGroup(this IEndpointRouteBuilder endpoints)
    {
        return endpoints.MapGroup("/reports");
    }

    public static IEndpointRouteBuilder MapReportEndpoints(this IEndpointRouteBuilder endpoints)
    {
        ArgumentNullException.ThrowIfNull(endpoints);

        var group = endpoints.MapReportGroup();

        group.MapGet("", GetReports);

        return endpoints;
    }

    private static IResult GetReports(ReportService service)
    {
        var result = service.GetTestReports();
        return TypedResults.Ok(result);
    }
}