namespace DocuWare.Web.BackendApp.Users;

public record UserLogin
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}

public record UserToken
{
    public required string Token { get; set; }
}
