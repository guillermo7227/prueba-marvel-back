namespace Marvel.Application.Common;

public record MarvelAPISettings
(
    string BaseEndpoint,
    string PrivateKey,
    string PublicKey,
    string ComicsEndpoint
);