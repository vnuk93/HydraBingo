using Bingo;
using System;
using HydraBingoClient = HydraBingo.Client.MainCore;
namespace HydraBingo.Client.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("HydraBingo Client Test");
            HeartbeatI config = new HeartbeatI{ 
                Data = new RegistryService
                {
                    Id = Guid.NewGuid().ToString(),
                    Status = 0,
                    Name = "HydraBingoClient",
                    Packages = "com.hydraframework.bingo.client.test",
                    Port = 1004,
                    Version = "1.0",
                    Group = "Lobby"
                }
            };
            HydraBingoClient _HydraBingo = new HydraBingoClient("localhost", "1002", config);
            Console.ReadLine();
        }
    }
}


