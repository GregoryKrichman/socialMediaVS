using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using socialMedia.Models;
using socialMedia.Repositories;

public class DefaultUserSeeder
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var userRepository = scope.ServiceProvider.GetRequiredService<IRepository<User>>();

        await AddDefaultUsers(userRepository);
    }

    private static async Task AddDefaultUsers(IRepository<User> userRepository)
    {
        if (!await userRepository.Exists(u => u.Username == "admin"))
        {
            var adminUser = new User
            {
                Username = "admin",
                Email = "admin@example.com",
                Password = "AdminPassword123!", 
                Name = "Admin User",
                
            };
            await userRepository.Add(adminUser);
            await userRepository.SaveAsync();
        }

        if (!await userRepository.Exists(u => u.Username == "guest1"))
        {
            var guestUser1 = new User
            {
                Username = "guest1",
                Email = "guest1@example.com",
                Password = "GuestPassword123!", 
                Name = "Guest User 1",
                
            };
            await userRepository.Add(guestUser1);
            await userRepository.SaveAsync();
        }

        if (!await userRepository.Exists(u => u.Username == "guest2"))
        {
            var guestUser2 = new User
            {
                Username = "guest2",
                Email = "guest2@example.com",
                Password = "GuestPassword123!", 
                Name = "Guest User 2",
               
            };
            await userRepository.Add(guestUser2);
            await userRepository.SaveAsync();
        }

        Console.WriteLine("Default users added successfully.");
    }
}
