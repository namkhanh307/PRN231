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
    }
}
