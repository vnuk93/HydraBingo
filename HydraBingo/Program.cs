using System;
using System.Threading.Tasks;
using Grpc.Core;
using Bingo;

namespace HydraBingo
{

    class Program
    {
        static void Main(string[] args)
        {
            const int Port = 1002;
            Console.WriteLine("> Starting HydraBingo");
            Server server = new Server
            {
                Services = { Greeter.BindService(new Controllers.HydraBingoController()) }, //Servicios disponibles generados por proto
                Ports = { new ServerPort("localhost", Port, ServerCredentials.Insecure) } //direccion, puerto y seguridad
            };
            server.Start();

            Console.WriteLine("> HydraBingo server listening on port " + Port);
            //Console.WriteLine("Press any key to stop the server...");
            Console.ReadKey();

            server.ShutdownAsync().Wait();


            Console.WriteLine(Guid.NewGuid().ToString());
        }
    }
}
