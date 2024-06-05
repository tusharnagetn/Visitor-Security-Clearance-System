using Visitor_Security_Clearance_System.CosmosDB;
using Visitor_Security_Clearance_System.Interface;
using Visitor_Security_Clearance_System.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<ICosmoseDBService, CosmosDBService>();
builder.Services.AddScoped<IManagerService, ManagerService>();
builder.Services.AddScoped<IOfficeService, OfficeService>();
builder.Services.AddScoped<ISecurityService, SecurityService>();
builder.Services.AddScoped<IVisitorService, VisitorService>();
builder.Services.AddScoped<IFunctionalityService, FunctionalityService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
