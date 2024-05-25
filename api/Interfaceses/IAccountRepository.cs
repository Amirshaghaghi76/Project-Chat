namespace api.Interfaceses;

public interface IAccountRepository
{
    public Task<LoggedInDto?> CreateAsync(RegisterDto userInput, CancellationToken cancellationToken);
    public Task<LoggedInDto?> LoginAsync(LoginDto userInput, CancellationToken cancellationToken);
}
