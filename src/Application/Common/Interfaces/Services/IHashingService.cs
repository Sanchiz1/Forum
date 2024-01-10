namespace Application.Common.Interfaces.Services
{
    public interface IHashingService
    {
        string ComputeHash(string password, string salt);

        string GenerateSalt();
    }
}
