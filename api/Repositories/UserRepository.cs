namespace api.Repositories;

public class UserRepository : IUserRepository
{
    #region MongoDb
    private const string _collectionName = "users";
    private readonly IMongoCollection<AppUser>? _collection;

    public UserRepository(IMongoClient client, IMongoDbSettings dbSettings)
    {
        var database = client.GetDatabase(dbSettings.DatabaseName);
        _collection = database.GetCollection<AppUser>(_collectionName);
    }

    #endregion MongoDb 

    public async Task<List<UserDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        List<AppUser> appUsers = await _collection.Find<AppUser>(new BsonDocument()).ToListAsync(cancellationToken);
        List<UserDto> userDtos = [];
        if (appUsers.Any())
        {
            foreach (AppUser appUser in appUsers)

            {
                UserDto userDto = _Mappers.ConvertAppUserToUserDto(appUser);

                userDtos.Add(userDto);
            }

            return userDtos;
        }

        return userDtos;
    }


    public async Task<UserDto?> GetByIdAsync(string? userId, CancellationToken cancellationToken)
    {
        AppUser appUser = await _collection.Find<AppUser>(user => user.Id == userId).FirstOrDefaultAsync(cancellationToken);
        // if (appUser is not null)
        if (appUser.Id is not null) // new method check is no not null id 
        {
            return _Mappers.ConvertAppUserToUserDto(appUser);
        }

        return null;
    }
    public async Task<UserDto?> GetByEmailAsync(string userEmail, CancellationToken cancellationToken)
    {
        AppUser appUser = await _collection.Find<AppUser>(user => user.Email == userEmail).FirstOrDefaultAsync(cancellationToken);

        if (appUser.Id is not null)
        {
            // UserDto userDto = GenerateUserDto(appUser);
            // return userDto; // Summarized above two line
            return _Mappers.ConvertAppUserToUserDto(appUser);
        }

        return null;
    }
    // before add _Mappers
    // private UserDto GenerateUserDto(AppUser appUser)
    // {
    //     return new UserDto(
    //       Id: appUser.Id!,
    //       Email: appUser.Email,
    //       KnownAs: appUser.KnownAs,
    //       Dob: appUser.DateOfBirth,
    //       Created: appUser.Created,
    //       LastActive: appUser.LastActive,
    //       Gender: appUser.Gender,
    //       Introduction: appUser.Introduction,
    //       Interests: appUser.Interests,
    //       City: appUser.City,
    //       Country: appUser.Country,
    //       LookingFor: appUser.LookingFor,
    //       Photos: appUser.Photos);
    // }
}