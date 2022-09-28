using Microsoft.EntityFrameworkCore;
using SP.Catalog.API.Configuration;
using SP.Catalog.API.Data;
using SP.Catalog.API.Data.Repository;
using SP.Catalog.API.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApiConfiguration(builder.Configuration);
builder.Services.AddSwaggerConfiguration();
builder.Services.RegisterServices();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();
app.UseSwaggerConfiguration();
app.UseApiConfiguration(app.Environment);

app.Run();
