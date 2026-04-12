using Examination.Persistence;
using Examination.Services;
using Examination.Web.EndPoints;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyDbContext"));
});

builder.Services.AddScoped<TestService>();
builder.Services.AddScoped<QuestionService>();
builder.Services.AddScoped<TestAttemptService>();
builder.Services.AddScoped<ResultService>();
builder.Services.AddScoped<ReportService>();

builder.Services.AddCors();

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.MapOpenApi();

app.UseCors(option =>
{
    option.AllowAnyHeader();
    option.AllowAnyMethod();
    option.AllowAnyOrigin();
});

var apiGroup = app.MapGroup("/api");

apiGroup.MapTestEndpoints();
apiGroup.MapQuestionEndpoints();
apiGroup.MapTestAttemptEndpoints();
apiGroup.MapResultEndpoints();
apiGroup.MapReportEndpoints();

app.MapGet("/", () => $"Running in {app.Environment.EnvironmentName} right now.");

app.Run();