using SPHealthSupportSystem_Repositories.Models;

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
