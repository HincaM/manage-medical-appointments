using Prescriptions.Domain.Interfaces;

namespace Prescriptions.Infrastructure.Services
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
