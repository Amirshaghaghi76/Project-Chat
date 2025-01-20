namespace api.Dtos;

public record UserDto
(
    string Id,
    string Email,
    string KnownAs,
    int Age,  // DateOnly Dob,
    DateTime Created, //time create account
    DateTime LastActive, //Last login time
    string Gender,
    string? Introduction,
    string? LookingFor,
    string? Interests,
    string City,
    string Country,
    List<Photo>? Photos
);