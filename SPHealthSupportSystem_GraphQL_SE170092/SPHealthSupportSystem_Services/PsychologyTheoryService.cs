using SPHealthSupportSystem_Repositories;
using SPHealthSupportSystem_Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPHealthSupportSystem_Services
{
    public class PsychologyTheoryService : IPsychologyTheoryService
    {
        private readonly PsychologyTheoryRepository _psychologyTheoryRepository;
        public PsychologyTheoryService()
        {
            _psychologyTheoryRepository = new PsychologyTheoryRepository();
        }
        public async Task<List<PsychologyTheory>> GetAll()
        {
            return await _psychologyTheoryRepository.GetAll();
        }

        public PsychologyTheory GetById(int id)
        {
            return _psychologyTheoryRepository.GetById(id);
        }
        public async Task<int> Create(PsychologyTheory PsychologyTheory)
        {
            return await _psychologyTheoryRepository.CreateAsync(PsychologyTheory);
        }
        public async Task<int> Update(PsychologyTheory PsychologyTheory)
        {
            return await _psychologyTheoryRepository.UpdateAsync(PsychologyTheory);
        }
        public async Task<bool> Delete(int id)
        {
            var item = await _psychologyTheoryRepository.GetByIdAsync(id);
            if (item != null)
            {
                return await _psychologyTheoryRepository.RemoveAsync(item);
            }
            return false;
        }

        public async Task<List<PsychologyTheory>> Search(string? name, string? topicName, string? author)
        {
            return await _psychologyTheoryRepository.Search(name, topicName, author);
        }


    }
}
