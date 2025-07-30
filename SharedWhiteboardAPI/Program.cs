using SharedWhiteboardAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Configure Kestrel to listen on all network interfaces (0.0.0.0)
builder.WebHost.ConfigureKestrel(options =>
{
    options.Listen(System.Net.IPAddress.Any, 5100); // Listen on port 5100 for HTTP
});
builder.Services.Configure<Microsoft.AspNetCore.Server.Kestrel.Core.KestrelServerOptions>(options =>
{
    options.AllowSynchronousIO = true;
});
builder.Services.Configure<Microsoft.AspNetCore.HostFiltering.HostFilteringOptions>(options =>
{
    options.AllowedHosts = new List<string> { "localhost", "192.168.1.71", "127.0.0.1", "*" };
});


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

if (builder.Configuration.GetValue<bool>("Demo"))
{
    builder.Services.AddSingleton<ISessionManager>(new DemoSessionManager());
}
else
{
    builder.Services.AddSingleton<ISessionManager, SessionManager>();
}

builder.Services.AddSingleton<IWhiteboardMerger, WhiteboardMerger>();

var x = builder.Configuration.GetSection("Demo").Value;
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers(); 

app.UseCors(builder =>
    builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

app.Run();
