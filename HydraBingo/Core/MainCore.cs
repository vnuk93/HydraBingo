using System;
using System.Collections.Generic;
using System.Timers;

namespace HydraBingo
{
    public class MainCore
    {
        private List<Core.Models.MRegistryService> services = new List<Core.Models.MRegistryService>();

        public MainCore()
        {
            var timer = new Timer(30000);
            timer.Elapsed += (sender, args) => GarbageDelete();
            timer.Start();
        }
        // AddService y ping podrian ir juntos para resumir el codigo que el cliente mande un ping si no existe lo crea si exite la id hace ping. 
        public Guid AddService(Core.Models.MRegistryService data)
        {
            Core.Models.MRegistryService add = new Core.Models.MRegistryService();

            add = data;

            services.Add(data);
            Console.WriteLine("> [ADD] Service " + add.name + "@" + add.serviceID + " add");
            return add.serviceID;
        }
        // El cliente tambien puede actualizar el status para así ponerse fuera de servicio si el lo considera.
        public void Ping(string id)
        {
            var servicio = services.Find(x => x.serviceID == Guid.Parse(id));
            servicio.up = true;
            servicio.delayNumber = 0;
            Console.WriteLine("> [PING] Service " + servicio.name + "@" + servicio.serviceID + " ping");

        }

        public void DeleteService(string id)
        {
            var servicio = services.Find(x => x.serviceID == Guid.Parse(id));
            var tmp = servicio.serviceID;
            services.Remove(servicio);
            Console.WriteLine("> [DELETE] Service " + tmp + " deleted");

        }

        public Core.Models.MRegistryService InfoService(string id)
        {
            var servicio = services.Find(x => x.serviceID == Guid.Parse(id));
            return servicio;
        }

        public List<Core.Models.MRegistryService> Search(string name)
        {
            var servicio = services.FindAll(x => x.name == name);
            return servicio;
        }

        public void GarbageService()
        {
            //var timer = new Timer(30000);
            //timer.Elapsed += (sender, args) => GarbageDelete();
            //timer.Start();
        }
        public void GarbageDelete()
        {
            Console.WriteLine("> [GARBAGE] Start");
            var servicioFalses = services.FindAll(x => x.up == false);
            Console.WriteLine("> [GARBAGE] Find: " + servicioFalses.Count + " services DOWN");
            foreach (var service in servicioFalses)
            {
                service.delayNumber++;
                if (service.delayNumber <= 4)
                {
                    service.status = 1;
                    Console.WriteLine("> [GARBAGE] Service " + service.serviceID + " OUT_OF_SERVICE -> delay: " + service.delayNumber);
                }
                if (service.delayNumber >= 3)
                {
                    DeleteService(service.serviceID.ToString());
                }
            }
            var servicioTrue = services.FindAll(x => x.up == true);
            Console.WriteLine("> [GARBAGE] Find: " + servicioTrue.Count + " services UP");
            foreach (var service in servicioTrue)
            {
                service.up = false;
            }

        }
    }
}