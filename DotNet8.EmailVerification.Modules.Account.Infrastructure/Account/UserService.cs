using DotNet8.EmailVerification.DTOs.Features.Account;
using DotNet8.EmailVerification.Modules.Account.Domain.Account;
using DotNet8.EmailVerification.Modules.Account.Infrastructure.Db;
using DotNet8.EmailVerification.Utils;
using FluentEmail.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet8.EmailVerification.Modules.Account.Infrastructure.Account
{
    public class UserService : IUserService
    {
        private readonly AccountDbContext _context;
        private IFluentEmail _fluentEmail;
        private readonly string _subject;

        public UserService(AccountDbContext context, IFluentEmail fluentEmail)
        {
            _context = context;
            _fluentEmail = fluentEmail;
            _subject = "Email Verification";
        }

        public Task<Result<UserDTO>> ConfirmEmailAsync(ConfirmEmailDTO comfirmEmail, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Result<UserDTO>> RegisterUserAsync(RegisterUserDTO requestModel, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        private string GetSixRandomNumber()
        {
            Random r=new Random();
            int randomNumber = r.Next(1000000);
            return randomNumber.ToString("D6");
        }
    }
}
