using System.Text.RegularExpressions;
using OmoqoTest.Domain.Common.Errors;
using OmoqoTest.Domain.Common.Models;

namespace OmoqoTest.Domain.Entities
{
    public partial class Ship : AuditableEntity<int>
    {
        private const string SHIP_CODE_PATTERN = @"^[A-Za-z]{4}-[0-9]{4}-[A-Za-z]{1}[0-9]{1}$";

        public Ship() { }

        public Ship(string? Code, string? Name, int Width, int Length)
        {
            this.Update(Code, Name, Width, Length);
        }

        [GeneratedRegex(SHIP_CODE_PATTERN)]
        private static partial Regex ShipCodeRegex();

        public string? Code { get; set; } = null!;
        public string? Name { get; set; } = null!;
        public int Width { get; set; }
        public int Length { get; set; }

        private void Validate()
        {
            if (string.IsNullOrEmpty(Name))
                ErrorsList.Add(Errors.Ship.RequiredName);

            if (string.IsNullOrEmpty(Code))
                ErrorsList.Add(Errors.Ship.RequiredCode);
            else if (!ShipCodeRegex().IsMatch(Code))
                ErrorsList.Add(Errors.Ship.InvalidCode);
        }

        public void Update(string? Code, string? Name, int Width, int Length)
        {
            this.Code = Code;
            this.Name = Name;
            this.Width = Width;
            this.Length = Length;

            Validate();
        }
    }
}