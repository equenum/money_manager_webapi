namespace MoneyManager.Api.Core.Interfaces.Entities.Authentication
{
    public interface IUser
    {
        int Id { get; set; }
        string Username { get; set; }
        string Password { get; set; }
        string Role { get; set; }
        string Token { get; set; }
    }
}