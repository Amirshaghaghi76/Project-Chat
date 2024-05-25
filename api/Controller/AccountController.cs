namespace api.Controller;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    #region MongoDb

    private const string _collectionName = "users";
    private readonly IMongoCollection<AppUser>? _collection;

    public AccountController(IMongoClient client, IMongoDbSettings dbSettings)
    {
        var database = client.GetDatabase(dbSettings.DatabaseName);
        _collection = database.GetCollection<AppUser>(_collectionName);
    }

    #endregion MongoDb

    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Create(RegisterDto UserInput, CancellationToken cancellationToken)
    {
        if (UserInput.PassWord != UserInput.ConFrimPassWord)
            BadRequest("Password Dont Match");

        bool doseExist = await _collection.Find<AppUser>(user => user.Email == UserInput.Email
        .ToLower().Trim()).AnyAsync(cancellationToken);
        if (doseExist)
            return BadRequest("email/userName is taken");

        AppUser appUser = new(
            Id: null,
            Name: UserInput.Name,
            PassWord: UserInput.PassWord,
            ConFrimPassWord: UserInput.ConFrimPassWord,
            Email: UserInput.Email
        );

        if (_collection is not null)
        {
            await _collection.InsertOneAsync(appUser, null, cancellationToken);
        }
        if (appUser.Id is not null)
        {
            UserDto userDto = new(
                Id: appUser.Id,
                Email: appUser.Email
            );

            return userDto;
        }

        return BadRequest("user was not created successfully");
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto userInput, CancellationToken cancellationToken)
    {
        AppUser appUser = await _collection.Find<AppUser>(user => user.Email == userInput.Email.ToLower().Trim()
        && user.PassWord == userInput.PassWord).FirstOrDefaultAsync(cancellationToken);

        if (appUser is null)
            return Unauthorized("Wron userName or Password");

            if (appUser.Id is not null )
            {
                UserDto userDto =new UserDto(
                    Id:appUser.Id,
                    Email:appUser.Email
                );

                return userDto;
            }

            return BadRequest("Task Failed");
    }
}
