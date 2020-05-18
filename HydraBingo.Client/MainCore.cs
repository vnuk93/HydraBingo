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
        public MainCore(string IP, string port, PingI config)
        {
            Channel channel = new Channel(IP + ":" + port, ChannelCredentials.Insecure); //Creamos un nuevo canal de cliente incresando direccion y puerto del servidor
            client = new Greeter.GreeterClient(channel); //Creamos un nuevo cliente, pasandole el servicio de proto (Greeter) y junto con el canal.

            ID = config.Id;
            status = config.Status;
            serviceConfig = config;

            Ping();
            var timer = new Timer(30000);
            timer.Elapsed += (sender, args) => Ping();
            timer.Start();
            //channel.ShutdownAsync().Wait();
        }

        public Greeter.GreeterClient client;
        public string ID { get; set; } = "";
        public int IP { get; set; }
        public int port { get; set; }
        public int status { get; set; }
        public PingI serviceConfig { get; set; }

        private void Ping()
        {
            try
            {
                status = client.Ping(serviceConfig).Status;
                Console.WriteLine("> [HydraBingo] Ping successful");
            }
            catch (Exception e) {
                Console.WriteLine("> [HydraBingo] Ping error");

            }

        }

        public void Stop()
        {
            client.Delete(new DeleteI { Id = ID });
        }
    }
}