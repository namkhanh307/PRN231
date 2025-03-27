using SPHealthSupportSystem_Repositories;
using SPHealthSupportSystem_Repositories.Models;

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
    }
}
