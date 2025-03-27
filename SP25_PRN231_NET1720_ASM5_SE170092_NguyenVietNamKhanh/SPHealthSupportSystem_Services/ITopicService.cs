using SPHealthSupportSystem_Repositories.Models;

namespace SPHealthSupportSystem_Services
{
    public interface ITopicService
    {
        Task<List<Topic>> GetAll();

    }
}
