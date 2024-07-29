namespace api.Models;
public record AppUser
(
    [property: BsonId, BsonRepresentation(BsonType.ObjectId)] string? Id,
    [Length(3,8)] string Name,
    byte[] PasswordSalt,
    byte[] PasswordHash,
    [MaxLength(50), RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,5})+)$", ErrorMessage ="Bad Email Format.")]
     string Email
);
