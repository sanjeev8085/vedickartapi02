//using Microsoft.AspNetCore.HttpOverrides;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Options;
//using NLog;
//using Vadickart.Repository;
//using vedickartApi.Api.Controllers;
//using vedickartApi.Extensions;
//using vedickartApi.Api.Extensions;
////var builder = WebApplication.CreateBuilder(args);0.

////LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(),
////"/nlog.config"));

////builder.Services.ConfigureCors();
////builder.Services.ConfigureIISIntegration();
////builder.Services.ConfigureLoggerService();
////builder.Services.ConfigureSwagger();

////builder.Services.AddControllers();
////// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
////builder.Services.AddOpenApi();

////var app = builder.Build();



////if (app.Environment.IsDevelopment())
////    app.UseDeveloperExceptionPage();
////else
////    app.UseHsts();


////// Configure the HTTP request pipeline.
////if (app.Environment.IsDevelopment())
////{
////    app.MapOpenApi();
////}

////app.UseSwagger();
////app.UseSwaggerUI(s =>
////{
////  //  s.SwaggerEndpoint("/swagger/v2/swagger.json");
////    //s.SwaggerEndpoint("/swagger/v2/swagger.json", "Code Maze API v2");
////});



////app.UseHttpsRedirection();

////app.UseStaticFiles();
////app.UseForwardedHeaders(new ForwardedHeadersOptions
////{
////    ForwardedHeaders = ForwardedHeaders.All
////});

////app.UseCors("CorsPolicy");

////app.UseAuthorization();

////app.MapControllers();

////app.Run();


//var builder = WebApplication.CreateBuilder(args);

//builder.Services.ConfigureCors();
//builder.Services.ConfigureIISIntegration();
////builder.Services.ConfigureLoggerService();
//builder.Services.ConfigureSwagger();

//builder.Services.AddHttpContextAccessor();
//builder.Services.AddControllers();
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
//builder.Services.ConfigureLoggerService(); // your logging setup
//builder.Services.ConfigureServiceManager();
//builder.Services.ConfigureRepositoryManager();
//builder.Services.ConfigureJwt(builder.Configuration);
//builder.Services.AddJwtConfiguration(builder.Configuration);
//builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

////builder.Services.AddAutoMapper(typeof(YourMappingProfile).Assembly);
//builder.Services.AddJwtConfiguration(builder.Configuration);
//builder.Services.ConfigureJwt(builder.Configuration);

//// ...other services
//builder.Services.AddDbContextFactory<RepositoryContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("sqlConnection"))); builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//var app = builder.Build();
//if (app.Environment.IsDevelopment())
//{
//    app.UseDeveloperExceptionPage();
//    app.UseSwagger();
//    app.UseSwaggerUI(c =>
//    {
//        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
//        c.RoutePrefix = ""; // Make swagger UI root, or remove to use /swagger
//    });
//}
//else
//{
//    app.UseHsts();
//}

//app.UseHttpsRedirection();
//app.UseStaticFiles();
//app.UseForwardedHeaders();


//app.UseCors("CorsPolicy");

//app.UseCors("CorsPolicy");

//app.UseAuthorization();
//app.UseAuthentication();
////app.UseAuthorization();

//app.MapControllers();

//app.Run();


using Microsoft.EntityFrameworkCore;
using vedickartApi.Extensions;
using Vadickart.Repository;
using vedickartApi.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Configure CORS
builder.Services.ConfigureCors();

// Configure IIS Integration
builder.Services.ConfigureIISIntegration();

// Configure Logger Service
builder.Services.ConfigureLoggerService();

// Configure Entity Framework
builder.Services.AddDbContext<RepositoryContext>(opts =>
    opts.UseSqlServer(builder.Configuration.GetConnectionString("sqlConnection")));

// Configure AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

// Configure Repository Manager
builder.Services.ConfigureRepositoryManager();

// Configure Service Manager
builder.Services.ConfigureServiceManager(builder.Configuration);

// Configure JWT Configuration
//builder.Services.AddJwtConfiguration(builder.Configuration);

// Configure JWT Authentication
//builder.Services.ConfigureJwt(builder.Configuration);
// Replace this ambiguous call:
// builder.Services.AddJwtConfiguration(builder.Configuration);

// With a fully qualified call to the intended method, for example:
vedickartApi.Extensions.ServiceExtensions.AddJwtConfiguration(builder.Services, builder.Configuration);
// Configure Swagger with JWT support
builder.Services.ConfigureSwagger();

// Replace ambiguous call with fully qualified method name to resolve CS0121
// Change this:
// builder.Services.ConfigureJwt(builder.Configuration);
// To one of the following, depending on which implementation you want:

// If you want the one from vedickartApi.Extensions.ServiceExtensions:
vedickartApi.Extensions.ServiceExtensions.ConfigureJwt(builder.Services, builder.Configuration);
var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "VedicKart API V1");
    });
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseForwardedHeaders();

// Configure CORS
app.UseCors("CorsPolicy");

// Configure Authentication & Authorization
//app.UseAuthentication(); // This must come before UseAuthorization
//app.UseAuthorization();

app.MapControllers();

app.Run();