using Prescriptions.Domain.Interfaces;

namespace Prescriptions.Infrastructure.Services
{
    public class UrlPersons : IUrlPersons
    {
        public string Value { get; }

        public UrlPersons(string value)
        {
            Value = value;
        }
    }
}
