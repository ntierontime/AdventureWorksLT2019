using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureWorksLT2019.MauiX.Services
{
    public class AuthenticationService
    {
        private readonly Framework.MauiX.Services.SecureStorageService _secureStorageService;
        public AuthenticationService(Framework.MauiX.Services.SecureStorageService secureStorageService)
        {
            _secureStorageService = secureStorageService;
        }

    }
}
