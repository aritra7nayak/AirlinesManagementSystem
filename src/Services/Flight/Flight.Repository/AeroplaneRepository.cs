using Flight.Infrastructure;
using Flight.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flight.Repository
{
    public class AeroplaneRepository : BaseRepository<Aeroplane>
    {
        public AeroplaneRepository(FlightContext flightContext) : base(flightContext)
        {

        }
    }
}
