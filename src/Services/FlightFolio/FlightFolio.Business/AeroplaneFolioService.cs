using FlightFolio.Models;
using FlightFolio.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FlightFolio.Business
{
    public class AeroplaneFolioService
    {
        private readonly AeroplaneFolioRepository _aeroplaneFolioRepository;

        public AeroplaneFolioService(AeroplaneFolioRepository aeroplaneFolioRepository)
        {
            _aeroplaneFolioRepository = aeroplaneFolioRepository ?? throw new ArgumentNullException(nameof(aeroplaneFolioRepository));
        }

        public async Task<AeroplaneFolio> GetByIdAsync(int id)
        {
            return await _aeroplaneFolioRepository.GetByIdAsync(id);
        }

        public async Task<IReadOnlyList<AeroplaneFolio>> GetAeroplaneFoliosAsync()
        {
            return await _aeroplaneFolioRepository.GetAllAsync();
        }

        public async Task<IReadOnlyList<AeroplaneFolio>> GetAeroplaneFoliosByFilterAsync(Expression<Func<AeroplaneFolio, bool>> filter)
        {
            return await _aeroplaneFolioRepository.GetAsync(filter);
        }

        public async Task<AeroplaneFolio> AddAeroplaneFolioAsync(AeroplaneFolio aeroplaneFolio)
        {
            return await _aeroplaneFolioRepository.AddAsync(aeroplaneFolio);
        }

        public async Task UpdateAeroplaneFolioAsync(AeroplaneFolio aeroplaneFolio)
        {
            await _aeroplaneFolioRepository.UpdateAsync(aeroplaneFolio);
        }

        public async Task DeleteAeroplaneFolioAsync(int id)
        {
            var aeroplaneFolioToDelete = await _aeroplaneFolioRepository.GetByIdAsync(id);
            if (aeroplaneFolioToDelete != null)
            {
                await _aeroplaneFolioRepository.DeleteAsync(aeroplaneFolioToDelete);
            }

        }
    }
}
