using Coreplus.Sample.Api.Endpoints.Practitioner;
using Coreplus.Sample.Api.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<PractitionerService>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

var app = builder.Build();

var practitionerEndpoints = app.MapGroup("/practitioners");
practitionerEndpoints.MapPractitionerEndpoints();

app.UseCors(builder =>
{
    builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
});

app.Run();
