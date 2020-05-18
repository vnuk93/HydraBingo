using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Grpc.Core;
using Bingo;

namespace HydraBingo.Controllers
{
    class HydraBingoController : Greeter.GreeterBase
    {
        MainCore _ = new MainCore();
        // Server side handler of the SayHello RPC
        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply { Message = "Hello " + request.Name });
        }
        public override Task<PingO> Ping(PingI request, ServerCallContext context)
        {
            
            PingO _out = new PingO();
            var serviceinfo = _.InfoService(request.Id) == null ? "" : _.InfoService(request.Id).serviceID.ToString();
            if(serviceinfo != request.Id)
            {
                Core.Models.MRegistryService newData = new Core.Models.MRegistryService();
                newData.serviceID = Guid.Parse(request.Id);
                newData.name = request.Name;
                newData.packages = request.Packages;
                newData.version = request.Version;
                newData.ip = request.Ip;
                newData.status = request.Status;
                newData.port = request.Port;
                newData.group = request.Group;
                _out.Id = _.AddService(newData).ToString();
                _out.Status = newData.status;
            }
            if (serviceinfo == request.Id) {
                _.Ping(request.Id);
            }
            return Task.FromResult(_out);
        }
        public override Task<DeleteO> Delete (DeleteI request, ServerCallContext context)
        {
            return Task.FromResult(new DeleteO { Res = false });
        }
        public override Task<InfoO> Info(InfoI request, ServerCallContext context)
        {
            return Task.FromResult(new InfoO {  });
        }
        public override Task<SearchO> Search(SearchI request, ServerCallContext context)
        {
            return Task.FromResult(new SearchO { });
        }

    }
}
