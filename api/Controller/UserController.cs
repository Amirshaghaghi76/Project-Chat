namespace api.Controller;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    #region MongoDb

    private const string _collectionName = "users";
    private readonly IMongoCollection<AppUser>? _collection;

    public UserController(IMongoClient client, IMongoDbSettings dbSettings)
    {
        var database = client.GetDatabase(dbSettings.DatabaseName);
        _collection = database.GetCollection<AppUser>(_collectionName);
    }

    #endregion MongoDb 

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>> GettAll(CancellationToken cancellationToken)
    {
        List<AppUser> appUsers = await _collection.Find<AppUser>(new BsonDocument()).ToListAsync(cancellationToken);
        if (!appUsers.Any())

            return NoContent();

        return appUsers;
    }

    [HttpGet("get-by-id/{userId}")]
    public async Task<ActionResult<AppUser>> GetById(string userId, CancellationToken cancellationToken)
    {
        AppUser appUser = await _collection.Find<AppUser>(user => user.Id == userId).FirstOrDefaultAsync(cancellationToken);

        if (appUser is null)

            return NotFound("NO user was found");

        return appUser;
    }
}
