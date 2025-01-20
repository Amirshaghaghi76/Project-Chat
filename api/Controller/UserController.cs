using Microsoft.AspNetCore.Authorization;

namespace api.Controller;
[Authorize]
public class UserController : BaseApiControllers
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    // [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GettAll(CancellationToken cancellationToken)
    {
        List<UserDto> userDtos = await _userRepository.GetAllAsync(cancellationToken);
        if (userDtos.Count == 0)

            return NoContent();

        return userDtos;
    }
    // [Authorize]
    [HttpGet("get-by-id")]
    public async Task<ActionResult<UserDto>> GetById( CancellationToken cancellationToken)
    {
        UserDto? userDto = await _userRepository.GetByIdAsync(ClaimPrincipalExtensions.GetUserId(User), cancellationToken);

        if (userDto is null)

            return NotFound("NO user was found");

        return userDto;
    }

    [HttpGet("get-by-email/{userEmail}")]
    public async Task<ActionResult<UserDto>> GetByEmail(string userEmail, CancellationToken cancellationToken)
    {
        UserDto? userDto = await _userRepository.GetByEmailAsync(userEmail, cancellationToken);
        if (userDto is null)

            return NotFound("NO user with this email address");

        return userDto;
    }
}
