using TaskManager.Api.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Register our in-memory repository — one shared instance for the app's lifetime.
builder.Services.AddSingleton<ITaskRepository, InMemoryTaskRepository>();
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
app.UseSwagger();
app.UseSwaggerUI();
}
else
{
    app.UseHttpsRedirection();
}
if (!app.Environment.IsDevelopment())
{
app.UseExceptionHandler(errorApp =>
{
errorApp.Run(async context =>
{
context.Response.StatusCode = StatusCodes.Status500InternalServerError;
context.Response.ContentType = "application/json";
await context.Response.WriteAsync("{\"error\":\"An unexpected error occurred.\"}");
});
});
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();