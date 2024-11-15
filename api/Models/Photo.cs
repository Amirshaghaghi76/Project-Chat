namespace api.Models;

public record Photo(
    string Url_64,
    string Url_128,
    string Url_512,
    string Url_1024,
    bool IsMain
);

