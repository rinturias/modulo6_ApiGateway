using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Solution.Aerolinea.Gateway.Aggregator;

var builder = WebApplication.CreateBuilder(args);



builder.Configuration.AddJsonFile("ocelot.json");
builder.Services.AddOcelot()
            .AddTransientDefinedAggregator<DetalleAsientoPorVueloAggregator>();


var app = builder.Build();

app.UseOcelot().Wait();

app.MapGet("/", () => "Hello World!");

app.Run();
