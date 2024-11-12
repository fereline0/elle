using System;
using System.Configuration;
using System.Windows.Forms;
using elle.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace elle
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            var services = new ServiceCollection();

            services.AddDbContext<Context>(options =>
                options.UseNpgsql(ConfigurationManager.ConnectionStrings["env"].ConnectionString)
            );

            services.AddScoped<UserService>();
            services.AddScoped<ImmovableService>();
            services.AddScoped<HomeService>();
            services.AddScoped<CityService>();
            services.AddScoped<Authorization>();
            services.AddScoped<AdminPanel>();
            services.AddScoped<ImmovableViewer>();

            var serviceProvider = services.BuildServiceProvider();

            Application.Run(serviceProvider.GetRequiredService<Authorization>());
        }
    }
}
