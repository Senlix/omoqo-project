using ErrorOr;

namespace OmoqoTest.Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class Authentication
        {
            public static Error InvalidCredentials => Error.Validation(code: "User.InvalidCredentials", description: "Invalid user or password");
        }
    }
}