using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;

namespace api.Repositories;

public class AccountRepository : IAccountRepository
{
    private const string _collectionName = "users";
    private readonly IMongoCollection<AppUser>? _collection;
    private readonly ITokenService _tokenService;

    public AccountRepository(IMongoClient client, IMongoDbSettings dbSettings, ITokenService tokenService)
    {
        var database = client.GetDatabase(dbSettings.DatabaseName);
        _collection = database.GetCollection<AppUser>(_collectionName);
        _tokenService = tokenService;
    }

    public async Task<LoggedInDto?> CreateAsync(RegisterDto userInput, CancellationToken cancellationToken)
    {

        bool doseExist = await _collection.Find<AppUser>(user => user.Email == userInput.Email
         .ToLower().Trim()).AnyAsync(cancellationToken);
        if (doseExist)
            return null;
        // befor add password salt and hash
        // AppUser appUser = new(
        //     Id: null,
        //     Name: userInput.Name,
        //     PassWord: userInput.PassWord,
        //     ConFrimPassWord: userInput.ConFrimPassWord,
        //     Email: userInput.Email
        // );

        using var hmac = new HMACSHA256();
        // if (userInput.Introduction is not null) Fixing the warning for trimming because it is null
        // {
            AppUser appUser = new(
                Id: null,
                // Name: userInput.Name, before add knownAs
                PasswordHash: hmac.ComputeHash(Encoding.UTF8.GetBytes(userInput.PassWord)),
                PasswordSalt: hmac.Key,
                Email: userInput.Email.ToLower().Trim(),
                KnownAs: userInput.KnownAs.Trim(),
                Created: DateTime.UtcNow,
                LastActive: DateTime.UtcNow,
                DateOfBirth: userInput.DateOfBirth,
                Gender: userInput.Gender,
                Introduction: userInput.Introduction?.Trim(),
                LookingFor: userInput.LookingFor?.Trim(),
                Interests: userInput.Interests?.Trim(),
                City: userInput.City,
                Country: userInput.Country,
                Photos: []

            );
        // }

        if (_collection is not null)
        {
            await _collection.InsertOneAsync(appUser, null, cancellationToken);
        }
        if (appUser.Id is not null)
        {
            LoggedInDto loggedInDto = new(
                Id: appUser.Id,
                Token: _tokenService.CreateToken(appUser),
                Email: appUser.Email,
                // Name:appUser.Name //  before add knownAs
                KnownAs: appUser.KnownAs
            );

            return loggedInDto;
        }

        return null;
    }

    public async Task<LoggedInDto?> LoginAsync(LoginDto userInput, CancellationToken cancellationToken)
    {

        AppUser appUser = await _collection.Find<AppUser>(user => user.Email == userInput.Email.ToLower().Trim())
        .FirstOrDefaultAsync(cancellationToken);

        if (appUser is null) // if email is not found
            return null;

        // Import and use HMACSHA256 including PasswordSalt
        using var hmac = new HMACSHA256(appUser.PasswordSalt);

        //convert userInputPassword to hash
        var ComputeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userInput.PassWord));

        // check if password is correct and matched with database PasswordHash
        if (appUser.PasswordHash is not null && appUser.PasswordHash.SequenceEqual(ComputeHash))
        {
            if (appUser.Id is not null)
            {
                return new LoggedInDto(
                    // Name:appUser.Name, //  before add knownAs
                    Id: appUser.Id,
                    Token: _tokenService.CreateToken(appUser),
                    Email: appUser.Email,
                    KnownAs: appUser.KnownAs
                );
            }
        }

        // befor add password salt and hash
        // AppUser appUser = await _collection.Find<AppUser>(user => user.Email == userInput.Email.ToLower().Trim()
        // && user.Password == userInput.password).FirstOrDefaultAsync(cancellationToken);
        // if (appUser is null)

        //     return null;

        // if (appUser.Id is not null)
        // {
        //     return new LoggedInDto(
        //          Id: appUser.Id,
        //          Email: appUser.Email
        //      );
        // }

        return null;
    }
}

