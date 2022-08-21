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
    public class UserService : IUserService
    {
        private readonly IRUser _rUser;

        public UserService(IRUser rUser)
        {
            this._rUser = rUser;
        }

        public async Task<UserResponse> Create(User user)
        {
            //Validaçãó para um create antes de encaminhar para a repository
            var hasAdded = await _rUser.CreateAsync(user);

            if (hasAdded)
                return new UserResponse { HasError = false, Message = "" };
            else
                return new UserResponse { HasError = true, Message = "Não foi possível criar um novo usuário! Favor verificar." };

        }

        public async Task<List<User>> List()
        {
            List<User> result = await _rUser.GetList();
            return result;
        }

    }
}
