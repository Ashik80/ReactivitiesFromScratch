using System.Collections.Generic;
using System.Linq;
using Domain;
using Microsoft.AspNetCore.Identity;

namespace Persistence
{
    public class Seed
    {
        public static async System.Threading.Tasks.Task SeedUsersAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var users = new List<AppUser>
                {
                    new AppUser
                    {
                        DisplayName = "Ashik",
                        UserName = "ashik80",
                        Email = "ashikurrahman80forget.ar@gmail.com"
                    },
                    new AppUser
                    {
                        DisplayName = "Sarah",
                        UserName = "buriMiya",
                        Email = "sarah@gmail.com"
                    }
                };
                foreach(var user in users){
                    await userManager.CreateAsync(user, "TestPassw0rd");
                }
            }
        }
    }
}