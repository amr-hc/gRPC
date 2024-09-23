using Grpc.Core;
using WebApplication1.Protos;

namespace WebApplication1.Services
{
    public class myServiceController :myservice.myserviceBase
    {
        private ILogger<myServiceController> logger;

        public myServiceController(ILogger<myServiceController> logger) {
            this.logger = logger;
        }
        public override Task<ResMessage> SendMessage(Reqmessage request, ServerCallContext context)
        {
            logger.LogInformation($"New Message : Id : {request.Id} , Time : {request.Time}  ,Lat : {request.Location.Lat}");
            return Task.FromResult(new ResMessage { Success=true});
        }
    }
}
