using Bit.Core.AdminConsole.Entities;
using Bit.Core.Auth.Models.Business.Tokenables;
using Bit.Core.Auth.Services;
using Bit.Core.Entities;
using Bit.Core.Tokens;

namespace Bit.Core.Auth.Identity;

public interface IOrganizationDuoWebTokenProvider : IOrganizationTwoFactorTokenProvider { }

public class OrganizationDuoWebTokenProvider : IOrganizationDuoWebTokenProvider
{
    private readonly IDuoUniversalClientService _duoUniversalClientService;
    private readonly IDataProtectorTokenFactory<DuoUserStateTokenable> _dataProtectionService;

    public OrganizationDuoWebTokenProvider(
        IDuoUniversalClientService duoUniversalClientService,
        IDataProtectorTokenFactory<DuoUserStateTokenable> tokenFactory)
    {
        _duoUniversalClientService = duoUniversalClientService;
        _dataProtectionService = tokenFactory;
    }

    public Task<bool> CanGenerateTwoFactorTokenAsync(Organization organization)
    {
        return Task.FromResult(
            _duoUniversalClientService.OrganizationUserCanUseDuoTwoFactor(organization) != null
            );
    }

    public async Task<string> GenerateAsync(Organization organization, User user)
    {
        var duoClient = await _duoUniversalClientService.BuildDuoClientAsync(user, organization);
        if (duoClient == null)
        {
            return null;
        }

        var state = _dataProtectionService.Protect(new DuoUserStateTokenable(user));
        var authUrl = duoClient.GenerateAuthUri(user.Email, state);

        return authUrl;
    }

    public async Task<bool> ValidateAsync(string token, Organization organization, User user)
    {
        var duoClient = await _duoUniversalClientService.BuildDuoClientAsync(user, organization);
        if (duoClient == null)
        {
            return false;
        }

        var parts = token.Split("|");
        var authCode = parts[0];
        var state = parts[1];

        _dataProtectionService.TryUnprotect(state, out var tokenable);
        if (!tokenable.Valid || !tokenable.TokenIsValid(user))
        {
            return false;
        }

        // Duo.Client compares the email from the received IdToken with user.Email to verify a bad actor hasn't used
        // their authCode with a victims credentials
        var res = await duoClient.ExchangeAuthorizationCodeFor2faResult(authCode, user.Email);
        // If the result of the exchange doesn't throw an exception and it's not null, then it's valid
        return res.AuthResult.Result == "allow";
    }
}
