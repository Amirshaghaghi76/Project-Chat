using System.Security.Cryptography;

namespace api.Dtos;

public static class _Mappers
{
    public static AppUser ConvertRegisterDtoToAppUser(RegisterDto userInput)
    {
        using var hmac = new HMACSHA256();
        // if (userInput.Introduction is not null) Fixing the warning for trimming because it is null
        // {
        return new AppUser(
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
        //}
    }

    public static LoggedInDto ConvertAppUserToLoggedinDto(AppUser appUser, string token)
    {
        return new LoggedInDto(
            Id: appUser.Id!,
            Token: token,
            Email: appUser.Email,
            // Name:appUser.Name //  before add knownAs
            KnownAs: appUser.KnownAs

        );
    }
    public static UserDto ConvertAppUserToUserDto(AppUser appUser)
    {
        return new UserDto(
          Id: appUser.Id!,
          Email: appUser.Email,
          KnownAs: appUser.KnownAs,
          Age: DateTimeExtensions.CalculateAge(appUser.DateOfBirth),
          Created: appUser.Created,
          LastActive: appUser.LastActive,
          Gender: appUser.Gender,
          Introduction: appUser.Introduction,
          Interests: appUser.Interests,
          City: appUser.City,
          Country: appUser.Country,
          LookingFor: appUser.LookingFor,
          Photos: appUser.Photos);
    }
}
