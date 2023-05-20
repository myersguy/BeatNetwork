using System;
using System.Security.Claims;
using System.Threading.Tasks;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Encodings.Web;
using BeatNetworkAPI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.JsonWebTokens;

public class FirebaseAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly FirebaseAuth _auth;

    public FirebaseAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock,
        FirebaseAuth firebaseAuth)
        : base(options, logger, encoder, clock)
    {
        _auth = firebaseAuth;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        try
        {
            string token = GetTokenFromRequest();
            if (token != null)
            {
                var decodedToken = await _auth.VerifyIdTokenAsync(token);
                Context.User = CreateClaimsPrincipal(decodedToken);
            }
        }
        catch (Exception ex)
        {
            Logger.LogError("Authentication failed: {0}", ex.Message);
        }

        if (Context.User.Identity.IsAuthenticated)
        {
            var ticket = new AuthenticationTicket(Context.User, Scheme.Name);
            return AuthenticateResult.Success(ticket);
        }
        else
        {
            return AuthenticateResult.NoResult();
        }
    }

    private string GetTokenFromRequest()
    {
        string authHeader = Context.Request.Headers["Authorization"];
        if (authHeader != null && authHeader.StartsWith("Bearer "))
        {
            return authHeader.Substring("Bearer ".Length).Trim();
        }

        return null;
    }

    private ClaimsPrincipal CreateClaimsPrincipal(FirebaseToken decodedToken)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, decodedToken.Uid),
            new Claim(JwtRegisteredClaimNames.Email, decodedToken.Claims["email"]?.ToString() ?? string.Empty),
            new Claim(ClaimTypes.NameIdentifier, decodedToken.Uid),
        };

        var identity = new ClaimsIdentity(claims, "Firebase");
        return new ClaimsPrincipal(identity);
    }

}