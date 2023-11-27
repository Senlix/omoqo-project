using System.Text;
using OmoqoTest.Domain.Entities;

namespace OmoqoTest.Infrastructure.Persistence
{
    public static class OmoqoTestDbContextSeed
    {
        public static void SeedData(OmoqoTestDbContext context)
        {
            if (context.Ships.Any())
            {
                return;
            }

            var random = new Random();
            var ships = new List<Ship>();

            for (int i = 1; i <= 1000; i++)
            {
                var ship = new Ship(GenerateShipCode(), $"Ship-{i}", random.Next(10, 50), random.Next(100, 500));

                ships.Add(ship);
            }

            context.Ships.AddRange(ships);
            context.SaveChanges();
        }

        private static string GenerateShipCode()
        {
            var random = new Random();
            var code = new StringBuilder();

            code.Append(GenerateRandomString(4));
            code.Append('-');
            code.Append(random.Next(1000, 9999));
            code.Append('-');
            code.Append(GenerateRandomString(1));
            code.Append(random.Next(10));

            return code.ToString();
        }

        private static string GenerateRandomString(int length)
        {
            string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            var random = new Random();
            return new string(Enumerable.Repeat(alphabet, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
