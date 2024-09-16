using SharedWhiteboardAPI.Services;

var builder = WebApplication.CreateBuilder(args);

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
