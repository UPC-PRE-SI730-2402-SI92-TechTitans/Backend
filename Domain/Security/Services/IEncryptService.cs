namespace Domain.Security.Services;

public interface IEncryptService
{
    public string Encrypt(string password);
    public bool Verify(string password, string passwordHashed);
}