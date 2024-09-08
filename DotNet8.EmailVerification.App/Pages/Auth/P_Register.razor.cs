using DotNet8.EmailVerification.App.Services;
using DotNet8.EmailVerification.DTOs.Features.Account;
using DotNet8.EmailVerification.Utils;
using MudBlazor;

namespace DotNet8.EmailVerification.App.Pages.Auth
{
    public partial class P_Register
    {
        public RegisterUserDTO requestDto { get; set; } = new();

        public async Task RegisterUser()
        {
            var response = await httpClientService.ExecuteAsync<Result<UserDTO>>(
              "/api/account/register",
              EnumHttpMethod.POST,
              requestDto
              );
            if (!response.IsSuccess)
            {
                snackbar.Add(response.Message, Severity.Error);
                return;
            }
            await localStorage.SetItemAsStringAsync("UserId", response.Data.UserId);
            snackbar.Add(response.Message, Severity.Success);
            _nav.NavigateTo("/confirmEmail");
        }
    }
}
