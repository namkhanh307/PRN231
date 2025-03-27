using Microsoft.EntityFrameworkCore;
using SPHealthSupportSystem_Repositories.Base;
using SPHealthSupportSystem_Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPHealthSupportSystem_Repositories
{
    public class StudentHealthRepository : GenericRepository<StudentHealth>
    {
        public async Task<List<StudentHealth>> GetAll()
        {
            return await _context.StudentHealths.ToListAsync();
        }
        public async Task<List<StudentHealth>> Search(int? studentGrade, int? riskRevel, string? studentGroup)
        {
            return await _context.StudentHealths.Include(s => s.StudentInfo).Where(s => (s.StudentInfo.Grade == studentGrade || studentGrade != null) && (s.RiskLevel == riskRevel || riskRevel != null) && (s.StudentInfo.Group == studentGroup || string.IsNullOrWhiteSpace(studentGroup))).ToListAsync();
        }
    }
}
