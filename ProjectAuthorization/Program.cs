using Authorization.Application;
using Authorization.Infrastructure.Database;

using Microsoft.OpenApi.Models;

WebApplicationBuilder builder = WebApplication.CreateBuilder( args );
builder.Services.AddControllers();
builder.Services.AddApplication( builder.Configuration );
builder.Services.AddInfrastructureDataBase( builder.Configuration );

builder.Services.AddEndpointsApiExplorer();

#region Swagger Configuration
builder.Services.AddSwaggerGen( swagger =>
{
    //This is to generate the Default UI of Swagger Documentation
    swagger.SwaggerDoc( "v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "JWT Token Authentication API",
        Description = "ASP.NET Core 5.0 Web API"
    } );
    // To Enable authorization using Swagger (JWT)
    swagger.AddSecurityDefinition( "Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description =
            "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
    } );
    swagger.AddSecurityRequirement( new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    } );
} );
#endregion


WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    _ = app.UseSwagger();
    _ = app.UseSwaggerUI( c =>
    {
        c.SwaggerEndpoint( "/swagger/v1/swagger.json", "FractalzBackend" );
        c.RoutePrefix = string.Empty;
    } );
}

app.AddMigrations();

app.UseCors( x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader() );
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
