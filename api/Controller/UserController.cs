namespace api.Controller;
public class UserController : BaseApiControllers
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GettAll(CancellationToken cancellationToken)
    {
        List<UserDto> userDtos = await _userRepository.GetAllAsync(cancellationToken);
        if (userDtos.Count == 0)

            return NoContent();

        return userDtos;
    }

    [HttpGet("get-by-id/{userId}")]
    public async Task<ActionResult<UserDto>> GetById(string userId, CancellationToken cancellationToken)
    {
        UserDto? userDto = await _userRepository.GetByIdAsync(userId, cancellationToken);

        if (userDto is null)

            return NotFound("NO user was found");

        return userDto;
    }
}
