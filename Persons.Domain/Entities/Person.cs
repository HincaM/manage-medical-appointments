using Persons.Domain.Enums;

namespace Persons.Domain.Entities
{
    public class Person
    {
        public int Id { get; set; }
        public string Identification { get; set; } = string.Empty;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public PersonType Type { get; set; }
    }
}
