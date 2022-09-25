using Entities.Entities;
using Entities.Entities.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface
{
    public interface IUserInternalService
    {
        Task<UserResponse> Create(UserInternal user);
        Task<UserResponse> Login(UserInternal user);
        Task<List<UserInternal>> List();
        Task<bool> ValidatePasswordConfirm(string password);
        Task<bool> UpdatePasswordViaRecovery(UserValidateRecoveryPassword password); 

    }
}
