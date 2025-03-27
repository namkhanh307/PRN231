using Microsoft.EntityFrameworkCore;
using SPHealthSupportSystem_Repositories.Base;
using SPHealthSupportSystem_Repositories.Models;

namespace SPHealthSupportSystem_Repositories
{
    public class UserAccountRepository : GenericRepository<UserAccount>
    {
        public async Task<UserAccount?> GetUserAccount(string username, string password)
        {
            return await _context.UserAccounts.Where(u => u.UserName == username && u.Password == password && u.IsActive == true).FirstOrDefaultAsync();
        }
    }
}
