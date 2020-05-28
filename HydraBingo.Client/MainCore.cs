using System;
using System.Collections.Generic;
using System.Text;
using Grpc.Core;
using Bingo;
using System.Timers;

namespace HydraBingo.Client
{
    public class MainCore
    {
        public MainCore(string IP, string port, HeartbeatI config)
        {
            Channel channel = new Channel(IP + ":" + port, ChannelCredentials.Insecure); //Creamos un nuevo canal de cliente incresando direccion y puerto del servidor
            client = new Greeter.GreeterClient(channel); //Creamos un nuevo cliente, pasandole el servicio de proto (Greeter) y junto con el canal.

            ID = config.Data.Id;
            status = config.Data.Status;
            serviceConfig = config;

            Heartbeat();
            var timer = new Timer(30000);
            timer.Elapsed += (sender, args) => Heartbeat();
            timer.Start();
            //channel.ShutdownAsync().Wait();
        }

        public Greeter.GreeterClient client;
        public string ID { get; set; } = "";
        public int IP { get; set; }
        public int port { get; set; }
        public int status { get; set; }
        public HeartbeatI serviceConfig { get; set; }
        public HeartbeatO BalancerResume { get; set; }

        private void Heartbeat()
        {
            try
            {
                var res = client.Heartbeat(serviceConfig);
                status = res.Status;
                BalancerResume = res;
                Console.WriteLine("> [HydraBingo] Heartbeat successful");
            }
            catch (Exception e) {
                Console.WriteLine("> [HydraBingo] Heartbeat error");

            }

        }

        public void Stop()
        {
            client.Delete(new DeleteI { Id = ID });
        }

        public InfoO Info(InfoI input)
        {
            return client.Info(input);

        }

        public SearchO Search(SearchI input)
        {          
            return client.Search(new SearchI { Name = input.Name });
        }
    }
}