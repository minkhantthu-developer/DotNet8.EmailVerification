namespace DotNet8.EmailVerification.App.Pages.Auth;

public partial class P_ConfirmEmail
{
    public ConfirmEmailDTO requestDto { get; set; } = new();

    public async Task ConfirmEmail()
    {
        requestDto.UserId = await _localStorage.GetItemAsStringAsync("UserId");
        if(requestDto.UserId is null)
        {
            snackbar.Add("User Not Found", Severity.Error);
            _nav.NavigateTo("/register");
            return;
        }
        var response = await _httpClientService.ExecuteAsync<Result<UserDTO>>(
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
