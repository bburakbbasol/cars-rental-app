using Microsoft.AspNetCore.DataProtection;

namespace CarRentalApp.Business.Security
{
    public class DataProtectionService
    {
        private readonly IDataProtector _protector;

        public DataProtectionService(IDataProtectionProvider provider)
        {
            _protector = provider.CreateProtector("CarRental.Security.v1");
        }

        public string Protect(string input)
        {
            return _protector.Protect(input);
        }

        public string Unprotect(string protectedInput)
        {
            return _protector.Unprotect(protectedInput);
        }
    }
}
