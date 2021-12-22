using TradeUp.API.HubConfig;
using TradeUp.Core.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IConsumerService, ConsumerService>();
builder.Services.AddSingleton<IResourceService, ResourceService>();
builder.Services.AddSingleton<IResourceContributorService, ResourceContributorService>();
builder.Services.AddSingleton<IResourceConsumerService, ResourceConsumerService>();

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      builder =>
                      {
                          builder
                          .AllowCredentials()
                          .WithOrigins("http://localhost:4200",
                                              "http://www.contoso.com");
                      });
});

builder.Services.AddSignalR();

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
app.MapHub<ResourceHub>("/resource");



app.UseCors(MyAllowSpecificOrigins);

app.Run();
