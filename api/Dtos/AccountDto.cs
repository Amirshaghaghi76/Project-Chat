
namespace api.Dtos;

public record RegisterDto
(
    [Length(3, 8)] string Name,
    [DataType(DataType.Password), Length(6, 10)] string PassWord,
    [DataType(DataType.Password), Length(6, 10)] string ConFrimPassWord,
    [MaxLength(50), RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,5})+)$", ErrorMessage ="Bad Email Format.")]
     string Email
);

public record LoginDto
(
 [MaxLength(50), RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,5})+)$", ErrorMessage ="Bad Email Format.")]
string Email,
    [DataType(DataType.Password), Length(6, 10)] string PassWord
);
// {
//     public static implicit operator LoginDto?(UserDto? v)
//     {
//         throw new NotImplementedException();
//     }
// }


public record LoggedInDto
(
    string Id,
    string Email
);