using App.Interfaces;
using EasyModbus;

namespace App.Services
{
    public class DataProcessingService : BackgroundService
    {
        private readonly IServiceProvider _ServiceProvider;
        private IConfiguration _configuration;
        // private IReadingsProcessor readingsProcessor;

        public DataProcessingService(IServiceProvider serviceProvider)
        {
            this._ServiceProvider = serviceProvider;
            _configuration = serviceProvider.GetRequiredService<IConfiguration>();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using (var scope = _ServiceProvider.CreateScope())
            {
                string ip = _configuration["SlaveIP"] ?? "127.0.0.1";
                int port = int.Parse(_configuration["SlavePort"] ?? "502");
                var now = DateTime.Now;

                var readingsProcessor = scope.ServiceProvider.GetRequiredService<IReadingsProcessor>();

                Console.WriteLine($"Process will start in {60 - now.Second} seconds");
                await Task.Delay(TimeSpan.FromSeconds(2), stoppingToken);
                Console.WriteLine("Process Started.");

                while (!stoppingToken.IsCancellationRequested)
                {
                    readingsProcessor.ProcessData(ip, port);
                    await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
                }
            }
        }
    }
}