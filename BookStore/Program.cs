using BookStore.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var Webhost = CreateHostBuilder(args).Build();
                RunMigrations(Webhost);
                Webhost.Run();
        }

        private static void RunMigrations(IHost webhost)
        {
            using (var scop = webhost.Services.CreateScope())
            {
                var db = scop.ServiceProvider.GetRequiredService<BookStoreDbContext>();
                db.Database.Migrate();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
