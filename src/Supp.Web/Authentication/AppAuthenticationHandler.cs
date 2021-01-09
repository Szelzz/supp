using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Supp.Web.Authentication
{
    public class AppAuthenticationHandler : AuthenticationHandler<AppAuthenticationOptions>
    {
        public AppAuthenticationHandler(IOptionsMonitor<AppAuthenticationOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            throw new NotImplementedException();
        }
    }

    public class AppAuthenticationOptions : AuthenticationSchemeOptions
    {
    }
}
