using System;

namespace HydraBingo.Core.Models
{
    public class MRegistryService
    {
        public Guid serviceID { get; set; }
        public string name { get; set; }
        public string packages { get; set; }
        public string version { get; set; }
        public string ip { get; set; }
        public int status { get; set; } = 2; //OUT_OF_SERVICE, DOWN, STARTING, UNKNOWN y UP.
        public int delayNumber { get; set; } = 0; //Si el servicio no tiene ningun ping aumentamos el delay si el delay es mayor de 4 cerramos el servicio.
        public bool up { get; set; } = true;
        public int port { get; set; } = 0;
        public string group { get; set; } = "Lobby";
    }
}
