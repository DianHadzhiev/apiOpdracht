using System.Net.WebSockets;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Movie API", Version = "v1" });
});
builder.Services.AddControllers().AddJsonOptions(options => {options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Movie API"));
}

app.UseRouting();
app.MapControllers();
app.UseHttpsRedirection();

using (var scope = app.Services.CreateScope()) {
    var services = scope.ServiceProvider;
    try{
        var context = services.GetRequiredService<MovieContext>();
        context.Database.Migrate();
    }catch (Exception ex) {
        Console.WriteLine(ex);
    }
}
app.Run();
