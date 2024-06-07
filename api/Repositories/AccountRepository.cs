using Microsoft.AspNetCore.Http.HttpResults;

namespace api.Repositories;

public class AccountRepository : IAccountRepository
{
    private const string _collectionName = "users";
    private readonly IMongoCollection<AppUser>? _collection;

    public AccountRepository(IMongoClient client, IMongoDbSettings dbSettings)
    {
        var database = client.GetDatabase(dbSettings.DatabaseName);
        _collection = database.GetCollection<AppUser>(_collectionName);
    }

    public async Task<LoggedInDto?> CreateAsync(RegisterDto userInput, CancellationToken cancellationToken)
    {

        bool doseExist = await _collection.Find<AppUser>(user => user.Email == userInput.Email
         .ToLower().Trim()).AnyAsync(cancellationToken);
        if (doseExist)
            return null;

        AppUser appUser = new(
            Id: null,
            Name: userInput.Name,
            PassWord: userInput.PassWord,
            ConFrimPassWord: userInput.ConFrimPassWord,
            Email: userInput.Email
        );

        if (_collection is not null)
        {
            await _collection.InsertOneAsync(appUser, null, cancellationToken);
        }
        if (appUser.Id is not null)
        {
            LoggedInDto loggedInDto = new(
                Id: appUser.Id,
                Email: appUser.Email
            );

            return loggedInDto;
        }

        return null;
    }

    public async Task<LoggedInDto?> LoginAsync(LoginDto userInput, CancellationToken cancellationToken)
    {

        AppUser appUser = await _collection.Find<AppUser>(user => user.Email == userInput.Email.ToLower().Trim()
        && user.PassWord == userInput.PassWord).FirstOrDefaultAsync(cancellationToken);

        if (appUser is null)
        
            return null;

        if (appUser.Id is not null)
        {
            return new LoggedInDto(
                 Id: appUser.Id,
                 Email: appUser.Email
             );
        }

        return null;
    }
}

