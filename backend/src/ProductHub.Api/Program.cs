using ProductHub.Api.Extensions;
using ProductHub.Api.Middleware;
using ProductHub.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCorsConfiguration(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerWithJwt();
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddProductHub(builder.Configuration);

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ProductHubDbContext>();
    SeedData.Seed(dbContext);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var policyName = builder.Configuration["CorsSettings:PolicyName"] ?? "AllowAngular";
app.UseCors(policyName);

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();