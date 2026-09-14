using Asp.Versioning;
using FindMyCleaner.Model;
using FindMyCleaner.Services;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
//FindMyCleaner - use Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();



//FindMyCleaner - register MongoDB Settings and service
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));

//FindMyCleaner - use versioning
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
    options.ApiVersionReader = new HeaderApiVersionReader("api-version");
})
.AddMvc();

//FindMyCleaner - CORS setting
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFindMyCleanerWeb", policy =>
    {
        policy.WithOrigins("https://localhost:7075")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddSingleton<CleaningServicesService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    //FindMyCleaner - use Swagger
    app.UseSwagger();
    app.UseSwaggerUI();

}

//FindMyCleaner - use HTTPS ONLY
if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

//FindMyCleaner - for securing API https
app.UseHttpsRedirection();

//FindMyCleaner - CORS
app.UseCors("AllowFindMyCleanerWeb");

// Public portfolio demo: reject writes before controllers or database access.
app.Use(async (context, next) =>
{
    if (!HttpMethods.IsGet(context.Request.Method) &&
        !HttpMethods.IsHead(context.Request.Method) &&
        !HttpMethods.IsOptions(context.Request.Method))
    {
        context.Response.StatusCode = StatusCodes.Status405MethodNotAllowed;
        context.Response.Headers.Allow = "GET, HEAD, OPTIONS";
        await context.Response.WriteAsJsonAsync(new { message = "This demo is read-only." });
        return;
    }

    await next();
});

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
