using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using Microsoft.VisualBasic;
using WebApplication1.Protos;
namespace WorkerService1
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly Random _random; // Declare a Random object


        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
            _random = new Random();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            var channel = GrpcChannel.ForAddress("https://localhost:7019");
            var client = new myservice.myserviceClient(channel);

            while (!stoppingToken.IsCancellationRequested)
            {

                var request = new Reqmessage()
                {
                    Id= _random.Next(1, 1000),
                    Speed= _random.Next(1, 1000),
                    Location = new Location { Lat =25 , Lang=84},
                    Time= Timestamp.FromDateTime(DateTime.UtcNow)
                };


                request.Sensors.Add(new Sensor() { Key="temp" , Value=44});
                request.Sensors.Add(new Sensor() { Key="door" , Value=1});

                ResMessage response= await client.SendMessageAsync(request);

             
                _logger.LogInformation($"Response: {response.Success}");
                
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
