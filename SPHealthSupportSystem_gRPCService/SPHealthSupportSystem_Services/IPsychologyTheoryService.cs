using SPHealthSupportSystem_Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPHealthSupportSystem_Services
{
    public interface IPsychologyTheoryService
    {
        Task<List<PsychologyTheory>> GetAll();
        PsychologyTheory GetById(int id);
        Task<int> Create(PsychologyTheory psychologyTheory);
        Task<int> Update(PsychologyTheory psychologyTheory);
        Task<bool> Delete(int id);
        Task<List<PsychologyTheory>> Search(string? name, string? topicName, string? author);
    }
}
