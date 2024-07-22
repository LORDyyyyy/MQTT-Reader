namespace App.Services
{
    public class DataProcessingService : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var now = DateTime.Now;

            Console.WriteLine($"Process will start in {60 - now.Second} seconds");
            await Task.Delay(TimeSpan.FromSeconds(60 - now.Second), stoppingToken);

            Console.WriteLine("Process Started.");

            while (!stoppingToken.IsCancellationRequested)
            {
                ProcessDataAsync();
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }

        private void ProcessDataAsync()
        {
            Console.WriteLine(DateTime.Now);
        }
    }
}