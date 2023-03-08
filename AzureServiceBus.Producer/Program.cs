using AzureServiceBus.Producer.Services;
using Microsoft.Azure.ServiceBus.Management;
using ServiceBusApp.Common;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddSingleton<AzureService>();
//dependeny injection ile çaðrýldýðýnda ne zaman Managementclient çaðrýldýðýnda newleyerek yeni bir ManagementClient getir demek için
builder.Services.AddSingleton<ManagementClient>(i => new ManagementClient(Constants.ConnectionString));

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
