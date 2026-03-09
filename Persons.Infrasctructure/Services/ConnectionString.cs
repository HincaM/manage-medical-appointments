using Persons.Domain.Interfaces;

namespace Persons.Infrastructure.Services
{
    public class ConnectionString : IConnectionString
    {
        public string Value { get; }

        public ConnectionString(string value)
        {
            Value = value;
        }
    }
}
