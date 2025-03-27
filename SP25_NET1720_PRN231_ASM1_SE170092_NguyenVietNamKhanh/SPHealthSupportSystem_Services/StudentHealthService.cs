using SPHealthSupportSystem_Repositories;
using SPHealthSupportSystem_Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPHealthSupportSystem_Services
{
    public class StudentHealthService : IStudentHealthService
    {
        private readonly StudentHealthRepository _studentHealthRepository;
        public StudentHealthService()
        {
            _studentHealthRepository = new StudentHealthRepository();
        }
        public async Task<List<StudentHealth>> GetAll()
        {
            return await _studentHealthRepository.GetAll();
        }

        public StudentHealth GetById(int id)
        {
            return _studentHealthRepository.GetById(id);
        }
        public async Task<int> Create(StudentHealth studentHealth)
        {
            return await _studentHealthRepository.CreateAsync(studentHealth);
        }
        public async Task<int> Update(StudentHealth studentHealth)
        {
            return await _studentHealthRepository.UpdateAsync(studentHealth);
        }
        public async Task<bool> Delete(int id)
        {
            var item = await _studentHealthRepository.GetByIdAsync(id);
            if (item != null)
            {
                return await _studentHealthRepository.RemoveAsync(item);
            }
            return false;
        }

        public async Task<List<StudentHealth>> Search(int? studentGrade, int? riskRevel, string? studentGroup)
        {
            return await _studentHealthRepository.Search(studentGrade, riskRevel, studentGroup);
        }


    }
}
