using Appointments.Domain.Interfaces;

namespace Appointments.Infrastructure.Services
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
