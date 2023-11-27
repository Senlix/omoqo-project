using ErrorOr;

namespace OmoqoTest.Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class Ship
        {
            public static Error NotFound => Error.NotFound(code: "Ship.NotFound", description: "Ship not found");
            public static Error RequiredName => Error.Validation(code: "Ship.RequiredName", description: "Ship Name is required");
            public static Error RequiredCode => Error.Validation(code: "Ship.RequiredCode", description: "Ship Code is required");
            public static Error InvalidCode => Error.Validation(code: "Ship.InvalidCode", description: "Ship Code must have a format of AAAA-0000-A0");
        }
    }
}