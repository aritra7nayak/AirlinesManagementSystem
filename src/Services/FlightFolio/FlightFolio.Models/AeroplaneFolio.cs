using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightFolio.Models
{
    public class AeroplaneFolio : Entity
    {
        public int Id { get; set; }

        public int AeroplaneId { get; set; }

        public string? Source { get; set; }

        public string? Destination { get; set; }

        public DateTime? Departure { get; set; }

        public DateTime? Arrival { get; set; }

        public int? TotalPassenger { get; set; }

        public virtual Aeroplane Aeroplane { get; set; }
    }
}
