using DotNet8.EmailVerification.App.Services;
using DotNet8.EmailVerification.DTOs.Features.Account;
using DotNet8.EmailVerification.Utils;
using MudBlazor;

namespace DotNet8.EmailVerification.App.Pages.Auth
{
    public partial class P_ConfirmEmail
    {
        public ConfirmEmailDTO requestDto { get; set; } = new();

        public async Task ConfirmEmail()
        {
            requestDto.UserId = await localStorage.GetItemAsStringAsync("UserId");
            if(requestDto.UserId is null)
            {
                snackbar.Add("User Not Found", Severity.Error);
                _nav.NavigateTo("/register");
                return;
            }
            var response = await httpClientService.ExecuteAsync<Result<UserDTO>>(
                "api/account/ConfirmEmail",
                EnumHttpMethod.POST,
                requestDto
                );
            if (!response.IsSuccess)
            {
                snackbar.Add(response.Message, Severity.Error);
                _nav.NavigateTo("/register");
                return;
            }
            snackbar.Add(response.Message, Severity.Success);
            _nav.NavigateTo("/");
        }
    }
}
