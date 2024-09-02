using Microsoft.EntityFrameworkCore;
using CentralSystem.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configure Entity Framework Core with SQL Server
builder.Services.AddDbContext<CentralSystemContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Add other services, e.g., authentication, logging, etc.
// builder.Services.AddAuthentication(...);
// builder.Services.AddAuthorization(...);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
        app.UseSwagger();
    app.UseSwaggerUI();
}

// Use HTTPS redirection
app.UseHttpsRedirection();

// Use authorization (if needed)
// app.UseAuthentication();
// app.UseAuthorization();

// Map controllers to routes
app.MapControllers();

app.Run();
