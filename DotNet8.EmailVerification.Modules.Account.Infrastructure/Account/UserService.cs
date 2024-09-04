using DotNet8.EmailVerification.DTOs.Features.Account;
using DotNet8.EmailVerification.Modules.Account.Domain.Account;
using DotNet8.EmailVerification.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet8.EmailVerification.Modules.Account.Infrastructure.Account
{
    public class UserService : IUserService
    {
        public Task<Result<UserDTO>> ConfirmEmailAsync(ConfirmEmailDTO comfirmEmail, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Result<UserDTO>> RegisterUserAsync(RegisterUserDTO requestModel, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
