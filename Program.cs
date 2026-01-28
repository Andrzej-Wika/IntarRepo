using Microsoft.Extensions.Configuration;

namespace IntarRepo
{
    internal static class Program
    {
        public static IConfiguration Configuration { get; private set; } = null!;

        [STAThread]
        static void Main()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("IntarRepo.json", optional: false, reloadOnChange: true)
                .Build();

            ApplicationConfiguration.Initialize();
            Application.Run(new frmMain());
        }
    }
}
