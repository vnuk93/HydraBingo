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
        public override Task<HeartbeatO> Heartbeat(HeartbeatI request, ServerCallContext context)
        {           
            HeartbeatO _out = new HeartbeatO();
            var serviceinfo = _.InfoService(request.Data.Id) == null ? "" : _.InfoService(request.Data.Id).serviceID.ToString();
            if(serviceinfo != request.Data.Id)
            {
                Core.Models.MRegistryService newData = new Core.Models.MRegistryService();
                newData.serviceID = Guid.Parse(request.Data.Id);
                newData.name = request.Data.Name;
                newData.packages = request.Data.Packages;
                newData.version = request.Data.Version;
                newData.ip = request.Data.Ip;
                newData.status = request.Data.Status;
                newData.port = request.Data.Port;
                newData.group = request.Data.Group;
                _out.Id = _.AddService(newData).ToString();
                _out.Status = newData.status;
            }
            if (serviceinfo == request.Data.Id) {
                _.Heartbeat(request.Data.Id);
            }
            return Task.FromResult(_out);
        }
        public override Task<DeleteO> Delete (DeleteI request, ServerCallContext context)
        {
            var _out = _.DeleteService(request.Id);
            return Task.FromResult(new DeleteO { Res = _out });
        }
        public override Task<InfoO> Info(InfoI request, ServerCallContext context)
        {
            var infoData = _.InfoService(request.Id);
            return Task.FromResult(new InfoO { 
                Id = infoData.serviceID.ToString(),
                DelayNumber = infoData.delayNumber,
                Group = infoData.group,
                Name = infoData.name,
                Packages = infoData.packages,
                Status = infoData.status,
                Version = infoData.version,
                Ip = infoData.ip,
                Port = infoData.port
            });
        }
        public override Task<SearchO> Search(SearchI request, ServerCallContext context)
        {           
            var datas = _.Search(request.Name);
            var _out = new SearchO { };

            foreach (var data in datas)
            {
                _out.Dummy.Add(new RegistryService { 
                    Id = data.serviceID.ToString(),
                    Name = data.name,
                    Packages = data.packages,
                    Version = data.version,
                    Ip = data.ip,
                    Status = data.status,
                    DelayNumber = data.delayNumber,
                    Port = data.port,
                    Group = data.group
                });

            }

            return Task.FromResult(_out);
        }

    }
}
