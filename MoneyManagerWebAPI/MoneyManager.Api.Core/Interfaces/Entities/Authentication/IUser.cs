namespace MoneyManager.Api.Core.Interfaces.Entities.Authentication
{
    public interface IUser
    {
        int Id { get; set; }
        string Name { get; set; }
        string Password { get; set; }
        string Role { get; set; }
        string Token { get; set; }
    }
}