﻿using Flight.DTOs;
using Flight.Infrastructure;
using Flight.Models;
using Microsoft.EntityFrameworkCore;
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

        public IEnumerable<PublishAeroplane> GetAeroplaneToPublish()
        {
            var result = _dbContext.Aeroplanes.Where(s => s.IsActive == true).Select(w => new PublishAeroplane
            {
                Name = w.Name,
                Manufacturer = w.Manufacturer
            }).ToList();

            return result;
        }

        public async Task<IReadOnlyList<PublishAeroplane>> GetAeroplanes()
        {
            var result = await _dbContext.Aeroplanes.Select(w => new PublishAeroplane
            {
                Name = w.Name,
                Manufacturer = w.Manufacturer
            }).ToListAsync();

            return result;
        }
    }
}
