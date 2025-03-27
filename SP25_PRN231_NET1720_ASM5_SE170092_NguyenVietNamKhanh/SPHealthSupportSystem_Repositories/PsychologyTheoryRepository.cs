using Microsoft.EntityFrameworkCore;
using SPHealthSupportSystem_Repositories.Base;
using SPHealthSupportSystem_Repositories.Models;

namespace SPHealthSupportSystem_Repositories
{
    public class PsychologyTheoryRepository : GenericRepository<PsychologyTheory>
    {
        public async Task<List<PsychologyTheory>> GetAll()
        {
            return await _context.PsychologyTheories.ToListAsync();
        }
        public async Task<List<PsychologyTheory>> Search(string? name, string? topicName, string? author)
        {
            return await _context.PsychologyTheories.Include(s => s.Topic).Where(s => (string.IsNullOrWhiteSpace(topicName) || s.Topic.Name.ToLower().Contains(topicName.ToLower())) && (string.IsNullOrWhiteSpace(name) || s.Name.ToLower().Contains(name.ToLower())) && (string.IsNullOrWhiteSpace(author) || s.Author.ToLower().Contains(author.ToLower()))).ToListAsync();
        }
    }
}