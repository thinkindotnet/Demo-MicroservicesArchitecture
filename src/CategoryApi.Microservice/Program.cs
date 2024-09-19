var builder = WebApplication.CreateBuilder(args);

// ----- Add services to the container.

SetupSqlDbService(builder);

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();


// ----- Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();




static void SetupSqlDbService(WebApplicationBuilder builder)
{

    const string dbConnectionNAME = "DefaultConnection";
    var connectionString
        = builder.Configuration.GetConnectionString(dbConnectionNAME)
          ?? throw new InvalidOperationException($"Connection string '{dbConnectionNAME}' not found.");

    builder.Services
        .AddDbContext<ApplicationDbContext>(options =>
        {

            options.UseSqlServer(
                connectionString,
                sqlServerOptionsAction: builderOptions =>
                {
                    // Configure the Retry On Failure Policy
                    builderOptions.EnableRetryOnFailure(
                        maxRetryCount: 3,
                        maxRetryDelay: TimeSpan.FromSeconds(5),
                        errorNumbersToAdd: null
                    );
                });

            // Disable the ROWCOUNT tracking behaviour for the data context.
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

            if (builder.Environment.IsDevelopment())
            {
                // Enables detailed errors when handling of data value exceptions that occur
                // during processing of store query results, mainly due to misconfiguration of entity properties.
                options.EnableDetailedErrors();

                // Include the values assigned to properties of the entity,
                // parameter values for commands being sent to the database,
                // and other such data.
                // NOTE: During EF Migrations, it will show a warning, even though this has been enabled
                //       inside the development environment only.  So, kindly ignore the warning.
                options.EnableSensitiveDataLogging();

                // log the SQL Query
                options.LogTo(Console.WriteLine, LogLevel.Information);
            }
        }, ServiceLifetime.Transient);          // once per Dependency Injection instead of the default Scoped (HTTP Request)


    if (builder.Environment.IsDevelopment())
    {
        builder.Services
               .AddDatabaseDeveloperPageExceptionFilter();
    }

}