using FlightFolio.Infrastructure;
using FlightFolio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightFolio.Repository
{
    public class AeroplaneFolioRepository : BaseRepository<AeroplaneFolio>
    {
        public AeroplaneFolioRepository(FlightFolioContext flightContext) : base(flightContext)
        {

        }
    }
}
