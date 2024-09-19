using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// ----- Add services to the container.

// Register Ocelot Services after adding reference to its configuration file to the Configurations container.
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: false);
builder.Services.AddOcelot(builder.Configuration);

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

// Register Swagger for Ocelot
builder.Services.AddSwaggerForOcelot(builder.Configuration, setup =>
{
    // Enable generation of Swagger Docs for controllers in the gateway.
    setup.GenerateDocsDocsForGatewayItSelf( options =>
    {
        options.GatewayDocsTitle = "MyApp API Gateway";
        options.GatewayDocsOpenApiInfo = new()
        {
            Title = "My Gateway",
            Version = "v1"
        };
    });
});


var app = builder.Build();


// ----- Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();

    app.UseSwaggerForOcelotUI(
        options =>
        {
            options.PathToSwaggerGenerator = "/swagger/docs";
        }, 
        uiOptions =>
        {
            uiOptions.DocumentTitle = "MyApp API Gateway documentation";
        });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Register the Ocelot Middleware
await app.UseOcelot();

app.Run();
