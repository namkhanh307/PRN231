using SPHealthSupportSystem_Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPHealthSupportSystem_Services
{
    public interface ITopicService
    {
        Task<List<Topic>> GetAll();
        Topic GetById(int id);
        Task<int> Create(Topic topic);
        Task<int> Update(Topic topic);
        Task<bool> Delete(int id);

    }
}
