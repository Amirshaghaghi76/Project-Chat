
namespace api.Dtos;

public record RegisterDto
(
    // [Length(3, 8)] string Name, //  before add knownA vbnm
    [DataType(DataType.Password), Length(6, 10)] string PassWord,
    [DataType(DataType.Password), Length(6, 10)] string ConFrimPassWord,
    [MaxLength(50), RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,5})+)$", ErrorMessage ="Bad Email Format.")]
    string Email,
    [Length(2, 30)] string KnownAs
    DateOnly DateOfBirth,
    [Length(3, 30)] string Gender,
    [Length(10, 70)] string? Introduction,
    [Length(10, 70)] string? LookingFor,
    [Length(10, 70)] string? Interests,
    [Length(2, 30)] string City,
    [Length(2, 30)] string Country,
    List<Photo>? Photos
);

public record LoginDto
(
 [MaxLength(50), RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,5})+)$", ErrorMessage ="Bad Email Format.")]
string Email,
    [DataType(DataType.Password), Length(6, 10)] string PassWord
);
public record LoggedInDto
(
    string Id,
    string Email,
    string Token,
    string KnownAs
// string Name  before add knownAs
);