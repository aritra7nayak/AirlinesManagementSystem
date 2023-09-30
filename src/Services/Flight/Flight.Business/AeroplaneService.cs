using Flight.MessageService;
using Flight.Models;
using Flight.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Flight.Business
{
    public class AeroplaneService
    {
        private readonly AeroplaneRepository _aeroplaneRepository;
        private readonly RabbitMQService _rabbitMQService;

        public AeroplaneService(AeroplaneRepository aeroplaneRepository, RabbitMQService rabbitMQService)
        {
            _aeroplaneRepository = aeroplaneRepository;
            _rabbitMQService = rabbitMQService;
        }

        public async Task<Aeroplane> GetByIdAsync(int id)
        {
            return await _aeroplaneRepository.GetByIdAsync(id);
        }

        public async Task<IReadOnlyList<Aeroplane>> GetAeroplanesAsync()
        {
            return await _aeroplaneRepository.GetAllAsync();
        }

        public async Task<IReadOnlyList<Aeroplane>> GetAeroplanesByFilterAsync(Expression<Func<Aeroplane, bool>> filter)
        {
            return await _aeroplaneRepository.GetAsync(filter);
        }

        public async Task<Aeroplane> AddAeroplaneAsync(Aeroplane aeroplane)
        {
            return await _aeroplaneRepository.AddAsync(aeroplane);
        }

        public async Task UpdateAeroplaneAsync(Aeroplane aeroplane)
        {
            await _aeroplaneRepository.UpdateAsync(aeroplane);
        }

        public async Task DeleteAeroplaneAsync(int id)
        {
            var aeroplaneToDelete = await _aeroplaneRepository.GetByIdAsync(id);
            if (aeroplaneToDelete != null)
            {
                await _aeroplaneRepository.DeleteAsync(aeroplaneToDelete);
            }

        }

        public bool PublishAeroplane()
        {
            var aeroplanes = _aeroplaneRepository.GetAeroplaneToPublish();

            try
            {
                _rabbitMQService.PublishFlightData(aeroplanes);
                return true;

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }

        }


        }
}
