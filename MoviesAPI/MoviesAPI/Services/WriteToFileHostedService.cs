﻿namespace MoviesAPI.Services
{
    public class WriteToFileHostedService : IHostedService
    {
        private readonly IWebHostEnvironment env;
        private readonly string fileName = "File 1.txt";
        private Timer timer;
        public WriteToFileHostedService(IWebHostEnvironment env)
        {
            this.env = env;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
            WriteToFile("Process Started\n");
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            WriteToFile("Process Stopped\n");
            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            WriteToFile("Process Ongoing: " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss\n"));
        }

        private void WriteToFile(string message)
        {
            var path = $@"{env.ContentRootPath}\wwwroot\{fileName}";
            using (StreamWriter writer = new StreamWriter(path, append: true))
            {
                writer.Write(message);
            }
        }
    }
}
