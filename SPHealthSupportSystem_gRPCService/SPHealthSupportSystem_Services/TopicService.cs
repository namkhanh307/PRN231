using SPHealthSupportSystem_Repositories;
using SPHealthSupportSystem_Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPHealthSupportSystem_Services
{
    public class TopicService : ITopicService
    {
        private readonly TopicRepository _topicRepository;
        public TopicService()
        {
            _topicRepository = new TopicRepository();
        }

        public async Task<List<Topic>> GetAll()
        {
            return _topicRepository.GetAll();
        }
        public Topic GetById(int id)
        {
            return _topicRepository.GetById(id);
        }
        public async Task<int> Create(Topic topic)
        {
            return await _topicRepository.CreateAsync(topic);
        }
        public async Task<int> Update(Topic topic)
        {
            return await _topicRepository.UpdateAsync(topic);
        }
        public async Task<bool> Delete(int id)
        {
            var item = await _topicRepository.GetByIdAsync(id);
            if (item != null)
            {
                return await _topicRepository.RemoveAsync(item);
            }
            return false;
        }

    }
}
