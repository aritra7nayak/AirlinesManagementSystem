using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightFolio.Models
{
    public class Aeroplane: Entity
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Manufacturer { get; set; }

    }
}
