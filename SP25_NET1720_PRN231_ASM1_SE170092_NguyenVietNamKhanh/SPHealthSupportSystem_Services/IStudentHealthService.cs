using SPHealthSupportSystem_Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPHealthSupportSystem_Services
{
    public interface IStudentHealthService
    {
        Task<List<StudentHealth>> GetAll();
        StudentHealth GetById(int id);
        Task<int> Create(StudentHealth studentHealth);
        Task<int> Update(StudentHealth studentHealth);
        Task<bool> Delete(int id);
        Task<List<StudentHealth>> Search(int? studentGrade, int? riskRevel, string? studentGroup);
    }
}
