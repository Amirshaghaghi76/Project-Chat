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
                UserDto userDto = new(
                    Id: appUser.Id!,
                    Email: appUser.Email
                );
                userDtos.Add(userDto);
            }

            return userDtos;
        }
        
        return userDtos;
    }


    public async Task<UserDto?> GetByIdAsync(string userId, CancellationToken cancellationToken)
    {
        AppUser appUser = await _collection.Find<AppUser>(user => user.Id == userId).FirstOrDefaultAsync(cancellationToken);
        if (appUser is not null)

            return new UserDto(
                Id: appUser.Id!,
                Email: appUser.Email
            );

        return null;
    }

}
