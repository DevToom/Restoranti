using Domain.Interface;
using Entities.Entities;
using Entities.Entities.VM;
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
            string hashPassword = EncodeHashPassword(user.Password);

            if (hashPassword != null)
            {
                user.Password = hashPassword;
                user.ConfirmPassword = hashPassword;

                //Validaçãó para um create antes de encaminhar para a repository
                var hasAdded = await _rUserInternal.CreateAsync(user);

                if (hasAdded)
                    return new UserResponse { HasError = false, Message = "" };
                else
                    return new UserResponse { HasError = true, Message = "Não foi possível criar um novo usuário! Favor verificar." };
            }
            else
                return new UserResponse { HasError = true, Message = "Não foi possível criar um novo usuário! Favor acionar o suporte." };

        }

        public async Task<UserResponse> Login(UserInternal user)
        {
            try
            {
                var userExistent = _rUserInternal.GetByUsername(user).Result;
                if (userExistent != null)
                {
                    if (EncodeHashPassword(user.Password) == userExistent.Password)
                        return new UserResponse { HasError = false, User = userExistent };
                    else
                        return new UserResponse { HasError = true, Message = "Usuários e/ou senha inválido." };
                }
                else
                    return new UserResponse { HasError = true, Message = "Usuários e/ou senha inválido." };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro técnico realizar o login, verificar. Exception: {ex.Message}  StackTrace: {ex.StackTrace}");
            }


            string hashPassword = EncodeHashPassword(user.Password);

            if (hashPassword != null)
            {
                user.Password = hashPassword;
                user.ConfirmPassword = hashPassword;

                //Validaçãó para um create antes de encaminhar para a repository
                var hasAdded = await _rUserInternal.CreateAsync(user);

                if (hasAdded)
                    return new UserResponse { HasError = false, Message = "" };
                else
                    return new UserResponse { HasError = true, Message = "Não foi possível criar um novo usuário! Favor verificar." };
            }
            else
                return new UserResponse { HasError = true, Message = "Não foi possível criar um novo usuário! Favor acionar o suporte." };

        }

        public async Task<List<UserInternal>> List()
        {
            List<UserInternal> result = await _rUserInternal.GetList();
            return result;
        }

        public async Task<bool> ValidatePasswordConfirm(string password)
        {
            try
            {
                UserInternal user = await _rUserInternal.GetUserAdm();
                var passEncoded = EncodeHashPassword(password);

                if (user.Password == passEncoded)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro técnico ao tentar realizar o ValidatePasswordConfirm, verificar. Exception: {ex.Message}  StackTrace: {ex.StackTrace}");
                return false;
            }

        }
        public async Task<bool> UpdatePasswordViaRecovery(UserValidateRecoveryPassword password)
        {
            try
            {
                var user = await _rUserInternal.GetByUsername(new UserInternal { Username = password.Username });
                if (user != null)
                {
                    var passEncoded = EncodeHashPassword(password.Password);
                    user.Password = passEncoded;
                    user.ConfirmPassword = passEncoded;
                    user.ModifiedDate = DateTime.Now;
                    user.ModifiedUserId = 1;
                    await _rUserInternal.Update(user);

                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro técnico ao tentar realizar o ValidatePasswordConfirm, verificar. Exception: {ex.Message}  StackTrace: {ex.StackTrace}");
                return false;
            }

        }

        private string EncodeHashPassword(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in EncodeHashPassword " + ex.Message);
                return null;
            }
        }
        private string DecodeHashPassword(string encodedData)
        {
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Text.Decoder utf8Decode = encoder.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encodedData);
            int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            string result = new String(decoded_char);
            return result;
        }

    }
}
