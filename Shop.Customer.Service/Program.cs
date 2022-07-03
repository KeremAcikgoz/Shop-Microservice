
using Shop.Common.MassTransit;
using Shop.Common.MongoDB;
using Shop.Common.Settings;
using Shop.Customer.Service.Entities;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
ServiceSettings serviceSettings = builder.Configuration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>();

//builder.Services.AddMassTransitHostedService();

builder.Services.AddControllers(options =>
{
    options.SuppressAsyncSuffixInActionNames = false;
});
builder.Services.AddMongo()
    .AddMongoRepository<Customer>("Customers")
    .AddMassTransitWithRabbitMq();
// builder.Services.AddHttpClient<CustomerClient>(client =>
// {
//
// });
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