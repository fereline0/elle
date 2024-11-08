using System;
using System.Windows.Forms;
using elle.Services;
using Microsoft.Extensions.DependencyInjection;

namespace elle
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            var services = new ServiceCollection();
            services.AddScoped<UserService>();
            services.AddScoped<ImmovableService>();
            services.AddScoped<Context>();
            services.AddScoped<Authorization>();

            var serviceProvider = services.BuildServiceProvider();

            Application.Run(serviceProvider.GetRequiredService<Authorization>());
        }
    }
}
