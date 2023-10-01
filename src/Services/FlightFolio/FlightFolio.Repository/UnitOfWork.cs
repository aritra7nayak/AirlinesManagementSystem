using FlightFolio.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightFolio.Repository
{
    public class UnitOfWork : IDisposable
    {
        private readonly FlightFolioContext _context;

        public AeroplaneRepository AeroplaneRepository { get; private set; }
        // Add other repository properties here if needed.

        public UnitOfWork(FlightFolioContext context)
        {
            _context = context;
            AeroplaneRepository = new AeroplaneRepository(_context);
            // Add other repository instantiations here if needed.
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
