using FlightFolio.Infrastructure;
using FlightFolio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightFolio.Repository
{
    public class AeroplaneRepository : BaseRepository<Aeroplane>
    {
        public AeroplaneRepository(FlightFolioContext flightContext) : base(flightContext)
        {

        }
    }
}
