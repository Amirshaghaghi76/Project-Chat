
namespace api.Controller;

public class AccountController :BaseApiControllers
{
    private readonly IAccountRepository _accountRepository;

    public AccountController(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    [HttpPost("register")]
    public async Task<ActionResult<LoggedInDto>> Register(RegisterDto UserInput, CancellationToken cancellationToken)
    {
        if (UserInput.PassWord != UserInput.ConFrimPassWord)
            BadRequest("Password Dont Match");

        LoggedInDto? loggedInDto = await _accountRepository.CreateAsync(UserInput, cancellationToken);

        if (loggedInDto is null)
        {
            return BadRequest("Email/User is Taken");
        }

        return loggedInDto;
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoggedInDto>> Login(LoginDto userInput, CancellationToken cancellationToken)
    {
        LoggedInDto? userDto = await _accountRepository.LoginAsync(userInput, cancellationToken);

        if (userDto is null)

            return Unauthorized("Wrong username or password");

        return userDto;
    }
}
