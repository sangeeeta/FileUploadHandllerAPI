using Microsoft.EntityFrameworkCore;
using UploadFiles.Data;
using UploadFiles.Interface;
using UploadFiles.Services;

var builder = WebApplication.CreateBuilder(args);

// ---------------------------
// 1. Add CORS
// ---------------------------
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200") // Angular app URL
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// ---------------------------
// 2. Add Controllers
// ---------------------------
builder.Services.AddControllers(option => option.ReturnHttpNotAcceptable = true);

// ---------------------------
// 3. Add DbContext (PostgreSQL)
// ---------------------------
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// ---------------------------
// 4. Add Swagger services
// ---------------------------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); // ✅ This was missing

// ---------------------------
// 5. Register DI services
// ---------------------------
builder.Services.AddScoped<IFileService, FileService>();

// ---------------------------
// Build the app
// ---------------------------
var app = builder.Build();

// ---------------------------
// Test DB connection
// ---------------------------
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    if (db.Database.CanConnect())
    {
        Console.WriteLine("PostgreSQL connection successful!");
    }
    else
    {
        Console.WriteLine("PostgreSQL connection failed!");
    }
}

// ---------------------------
// 6. Middleware pipeline
// ---------------------------
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "UploadFiles API v1");
        c.RoutePrefix = "swagger"; // https://localhost:7098/swagger
    });
}

app.UseCors("AllowAngularApp");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
