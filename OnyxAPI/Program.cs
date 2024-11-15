using Microsoft.AspNetCore.Mvc;
using Onyx.Application.Models;
using Onyx.Application.VAT;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddControllers();
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services.Configure<JsonOptions>(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddScoped<IVatVerifier, VatVerifier>();
builder.Services.AddScoped<IVarVerificationService, VatVerificationService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapControllers();

// Use CORS
app.UseCors("AllowAllOrigins");

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseDefaultFiles();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/verify-vat", async (
                     [FromQuery(Name = "countryCode")] string countryCode,
                     [FromQuery(Name = "vatId")] string vatId,
                     [FromServices] IVatVerifier verifier)
                     =>
{
    var result = await verifier.VerifyVAT(countryCode, vatId);
    if(result.Status == VerificationStatus.Unavailable)
    {
        return Results.BadRequest(result);
    }
    return Results.Ok(result);
})
.WithName("ValidateVatId")
.WithOpenApi();

app.MapFallbackToFile("/index.html");

app.Run();