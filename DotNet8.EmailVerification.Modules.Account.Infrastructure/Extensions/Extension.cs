using DotNet8.EmailVerification.DTOs.Features.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet8.EmailVerification.Modules.Account.Infrastructure.Extensions
{
    public static class Extension
    {
        public static Tbl_User ToEntity(this RegisterUserDTO requestDto)
        {
            return new Tbl_User
            {
                UserId = Ulid.NewUlid().ToString(),
                UserName=requestDto.UserName,
                Email=requestDto.Email,
                Password=requestDto.Password,
                IsEmailVerified=false,
                IsActive=true
            };
        }

        public static Tbl_Setup ToEntity(string userId,string code)
        {
            return new Tbl_Setup
            {
                SetupId = Ulid.NewUlid().ToString(),
                UserId = userId,
                SetupCode = code,
                DateCreate = DateTime.Now
            };
        }
    }
}
