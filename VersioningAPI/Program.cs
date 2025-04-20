using Asp.Versioning;
using VersioningAPI.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//API version Reader-> Http- Header, QueryString, URL Segments
// "API- Version" Http Header = v1.0,
// "http://xxx.xxx/xxx/xxx?v=1.0 Query String
// http://localhost/api/v1.0/customers/- URLSegment

builder.Services.AddApiVersioning(setupAction =>
{
    // Major Version changes when the functionalitites are added
    // Minor version changes when the existing functionalities are updated
    // Build Number- what is the successful build.
    // Revision Number - which iteration was successfully built.
    // .NET Framework version - 4.0.30319

    setupAction.DefaultApiVersion = new ApiVersion(1, 0);
    // major version is 0. something then it is in beta testing phase
    // if it is 1 or some thing means it is main version
    setupAction.AssumeDefaultVersionWhenUnspecified = true;
    setupAction.ReportApiVersions = true; // tell the clients the avaailable versions
    setupAction.ApiVersionReader = ApiVersionReader.Combine(
        new UrlSegmentApiVersionReader(),
        new QueryStringApiVersionReader("v"),
        new HeaderApiVersionReader("X-version"),
        new MediaTypeApiVersionReader("X-Version")
        // Content Type : "application/json;x-version=1.0"
        );

})
    .AddMvc(options=> { 
        //options.Conventions.Controller<HomeController>()
        //Action<HomeController>(c=>c.GetGreeting())
        //.Action<HomeController>(char=> char.GetToday())
        //.MapToApiVersion(new ApiVersion(1,0))
    })
    .AddApiExplorer(options=>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
// this line was added to test the github with multiple users with change for conflict
// adding another line for checking conflicts.
app.UseAuthorization();

app.MapControllers();

app.Run();
