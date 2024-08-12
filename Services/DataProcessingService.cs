using App.Interfaces;
using Quartz;

namespace App.Services
{
    public class DataProcessingService : BackgroundService, IJob
    {
        private readonly IServiceProvider _ServiceProvider;
        private IConfiguration _configuration;
        // private IReadingsProcessor readingsProcessor;

        public DataProcessingService(IServiceProvider serviceProvider)
        {
            this._ServiceProvider = serviceProvider;
            _configuration = serviceProvider.GetRequiredService<IConfiguration>();
        }

        public Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine("------------------------------------------------------------------------------");
            Console.WriteLine(DateTime.Now);
            using (var scope = _ServiceProvider.CreateScope())
            {
                var readingsProcessor = scope.ServiceProvider.GetRequiredService<IReadingsProcessor>();
                readingsProcessor.ProcessData();
            }
            return Task.CompletedTask;
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
                    while(DateTime.Now.Second%5 != 0)
                        await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
                    readingsProcessor.ProcessData();
                        await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);

                    Console.WriteLine(readingsProcessor.GetHashCode());
                }
            }
        }
    }
}