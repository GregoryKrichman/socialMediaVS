using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using socialMedia.Data;
using socialMedia.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using socialMedia.Services;
using System.Text.Json.Serialization;
using socialMedia.Models;
using Microsoft.Extensions.FileProviders;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        // Configure CORS to allow requests from the frontend
        services.AddCors(options =>
        {
            options.AddPolicy("AllowLocalhost",
                builder => builder
                    .WithOrigins("http://localhost:3000")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
        });

        // Add controllers with JSON options to handle reference loops
        services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            });

        // Configure DbContext to use MySQL
        services.AddDbContext<AppDbContext>(options =>
            options.UseMySQL(Configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.")));

        // Configure JWT authentication
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                };
            });

        // Register repositories and services
        services.AddScoped<IRepository<User>, UserRepository>();
        services.AddScoped<IRepository<Post>, PostRepository>();
        services.AddScoped<IRepository<Comment>, CommentRepository>();
        services.AddScoped<IRepository<Like>, LikeRepository>();
        services.AddScoped<IRepository<Relationship>, RelationshipRepository>();
        services.AddScoped<IRepository<Story>, StoryRepository>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<AppDbContext>();  // Registering AppDbContext
        services.AddScoped<ICompositeKeyRepository<Like>, LikeRepository>();

        // Add Swagger for API documentation
        services.AddSwaggerGen();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
          


            // Configure Swagger middleware for development environment
            app.UseSwagger();
            app.UseSwaggerUI(c =>
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));
            
        }

        // Enforce HTTPS
        app.UseHttpsRedirection();

        // Enable static files middleware
        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(Path.Combine(env.WebRootPath, "uploads")),
            RequestPath = "/uploads",
             OnPrepareResponse = ctx =>
             {
                 // Logging the request
                 Console.WriteLine($"Serving static file: {ctx.File.PhysicalPath}");
             }
        });
        // Routing middleware
        app.UseRouting();

        // Enable CORS with the defined policy
        app.UseCors("AllowLocalhost");

        // Enable authentication and authorization
        app.UseAuthentication();
        app.UseAuthorization();

        // Configure endpoints
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        // Seed default users
        using var serviceScope = app.ApplicationServices.CreateScope();
        var serviceProvider = serviceScope.ServiceProvider;
        DefaultUserSeeder.SeedAsync(serviceProvider).Wait();
    }
}
