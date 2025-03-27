using SPHealthSupportSystem_Repositories;
using SPHealthSupportSystem_Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPHealthSupportSystem_Services
{
    public class UserAccountService
    {
        private readonly UserAccountRepository _userAccountRepository;
        public UserAccountService()
        {
            _userAccountRepository = new UserAccountRepository();
        }

        public async Task<UserAccount?> Authenticate(string userName,  string password)
        {
            return await _userAccountRepository.GetUserAccount(userName, password);
        }
    }
}
