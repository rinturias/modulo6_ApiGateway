using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Solution.Aerolinea.Gateway.Aggregator;

var MyAllowSpecificOrigins = "OriginsCors";
var builder = WebApplication.CreateBuilder(args);



builder.Configuration.AddJsonFile("ocelot.json");
builder.Services.AddOcelot()
            .AddTransientDefinedAggregator<DetalleAsientoPorVueloAggregator>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("*")
                         .AllowAnyHeader()
                         .AllowAnyMethod();
});

});

var app = builder.Build();
app.UseCors(MyAllowSpecificOrigins);
app.UseOcelot().Wait();

//app.UseAuthorization();
app.MapGet("/", () => "Hello World!");

app.Run();
