using ErrorOr;

namespace OmoqoTest.Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class User
        {
            public static Error DuplicatedEmail => Error.Conflict(code: "User.DuplicatedEmail", description: "User already exists with this Email");
        }
    }
}