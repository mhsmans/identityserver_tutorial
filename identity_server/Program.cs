using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Security.Claims;

namespace identity_server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            // seed user
            using (var scope = host.Services.CreateScope())
            {
                var userManager = scope.ServiceProvider
                    .GetRequiredService<UserManager<IdentityUser>>();

                var user = new IdentityUser("bob");
                userManager.CreateAsync(user, "password").GetAwaiter().GetResult();
                // add claim to identity server side, idToken
                userManager.AddClaimAsync(user, new Claim("test.claim", "test.claim.value"))
                    .GetAwaiter().GetResult();
                // this claim will be added to access token
                userManager.AddClaimAsync(user, new Claim("test.at.claim", "test.at.claim.value"))
                    .GetAwaiter().GetResult();
            }             
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
