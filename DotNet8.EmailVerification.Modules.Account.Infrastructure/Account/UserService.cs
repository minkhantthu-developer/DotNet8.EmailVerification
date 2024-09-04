using DotNet8.EmailVerification.DTOs.Features.Account;
using DotNet8.EmailVerification.DTOs.Features.Setup;
using DotNet8.EmailVerification.Modules.Account.Domain.Account;
using DotNet8.EmailVerification.Modules.Account.Infrastructure.Db;
using DotNet8.EmailVerification.Modules.Account.Infrastructure.Extensions;
using DotNet8.EmailVerification.Utils;
using FluentEmail.Core;
using Hangfire;
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

        public async Task<Result<UserDTO>> RegisterUserAsync(RegisterUserDTO requestModel, CancellationToken cancellationToken)
        {
            Result<UserDTO> response;
            var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                var emailDuplicate = await _context.Tbl_User.AnyAsync(
                    x => x.Email == requestModel.Email,
                    cancellationToken
                    );
                if (emailDuplicate)
                {
                    response = Result<UserDTO>.Duplicate("Email Duplicate");
                    goto result;
                }
                var user = requestModel.ToEntity();
                var randomCode = GetSixRandomNumber();
                await _context.Tbl_User.AddAsync(user, cancellationToken);
                var setup = Extension.ToEntity(user.UserId!, randomCode);
                await _context.Tbl_Setup.AddAsync(setup, cancellationToken);

                BackgroundJob.Schedule<UserService>(x =>
                x.CodeExpireCheck(user.UserId!, setup.SetupId!, cancellationToken),
                TimeSpan.FromMinutes(3)
                );

                var sendEmail = await _fluentEmail
                     .To(requestModel.Email)
                     .Subject(_subject)
                     .Body($"Your Confirmation Code is {randomCode}")
                     .SendAsync();
                if (!sendEmail.Successful)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    response = Result<UserDTO>.Failure();
                    goto result;
                }
                await _context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
                var userDto = new UserDTO
                {
                    UserId = user.UserId!
                };
                response = Result<UserDTO>.Success(userDto);

            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                response = Result<UserDTO>.Failure(ex);
            }
        result:
            return response;
        }

        public async Task<Result<UserDTO>> ConfirmEmailAsync(
            ConfirmEmailDTO comfirmEmail,
            CancellationToken cancellationToken
            )
        {
            Result<UserDTO> response;
            try
            {
                var setup = await _context
                    .Tbl_Setup
                    .FirstOrDefaultAsync(x => 
                    x.SetupCode == comfirmEmail.Code,
                    cancellationToken);
                if(setup is null)
                {
                    response = Result<UserDTO>.NotFound("Invalid Code");
                    goto result;
                }
                var user=await _context
                    .Tbl_User
                    .FirstOrDefaultAsync(x=>
                            x.UserId == comfirmEmail.UserId,
                            cancellationToken);
                if(user is null)
                {
                    response = Result<UserDTO>.NotFound("User not found");
                    goto result;
                }
                user.IsEmailVerified = true;
                _context.Tbl_User.Update(user);
                _context.Tbl_Setup.Remove(setup); 
                await _context.SaveChangesAsync(cancellationToken);
                response = Result<UserDTO>.Success();
            }
            catch (Exception ex)
            {
                response= Result<UserDTO>.Failure(ex);
            }
            result:
            return response;
        }

        private string GetSixRandomNumber()
        {
            Random r = new Random();
            int randomNumber = r.Next(1000000);
            return randomNumber.ToString("D6");
        }

        public async Task<Result<SetupDTO>> CodeExpireCheck(
            string userId,
            string setupId,
            CancellationToken cancellationToken
            )
        {
            Result<SetupDTO> response;
            try
            {
                var setup = await _context
                    .Tbl_Setup
                    .FindAsync(setupId, cancellationToken);
                if (setup is null)
                {
                    response = Result<SetupDTO>.NotFound();
                    goto result;
                }
                var user = await _context
                    .Tbl_User
                    .FindAsync(userId, cancellationToken);
                if (user is null)
                {
                    response = Result<SetupDTO>.NotFound();
                    goto result;
                }
                if (!user.IsEmailVerified)
                {
                    _context.Tbl_User.Remove(user);
                }
                _context.Tbl_Setup.Remove(setup);
                await _context.SaveChangesAsync(cancellationToken);
                response = Result<SetupDTO>.UpdateSuccess();
            }
            catch (Exception ex)
            {
                response = Result<SetupDTO>.Failure(ex);
            }
        result:
            return response;
        }
    }
}
