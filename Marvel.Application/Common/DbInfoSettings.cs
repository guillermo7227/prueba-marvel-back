namespace Marvel.Application.Common;

public record DbInfoSettings
(
    string DbHost,
    string DbDatabase,
    string DbUsername,
    string DbPassword
);