using App.Interfaces;

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
            var now = DateTime.Now;

            Console.WriteLine($"Process will start in {60 - now.Second} seconds");
            await Task.Delay(TimeSpan.FromSeconds(60 - now.Second), stoppingToken);

            Console.WriteLine("Process Started.");

            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _ServiceProvider.CreateScope())
                {

                    var readingsProcessor = scope.ServiceProvider.GetRequiredService<IReadingsProcessor>();

                    readingsProcessor.ProcessData();
                    await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);

                    Console.WriteLine(readingsProcessor.GetHashCode());
                }
            }
        }
    }
}