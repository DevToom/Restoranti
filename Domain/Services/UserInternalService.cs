using Domain.Interface;
using Entities.Entities;
using Infra.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class UserInternalService : IUserInternalService
    {
        private readonly IRUserInternal _rUserInternal;

        public UserInternalService(IRUserInternal rUserInternal)
        {
            this._rUserInternal = rUserInternal;
        }

        public async Task<UserResponse> Create(UserInternal user)
        {
            //Validaçãó para um create antes de encaminhar para a repository
            var hasAdded = await _rUserInternal.CreateAsync(user);

            if (hasAdded)
                return new UserResponse { HasError = false, Message = "" };
            else
                return new UserResponse { HasError = true, Message = "Não foi possível criar um novo usuário! Favor verificar." };

        }

        public async Task<List<UserInternal>> List()
        {
            List<UserInternal> result = await _rUserInternal.GetList();
            return result;
        }

    }
}
