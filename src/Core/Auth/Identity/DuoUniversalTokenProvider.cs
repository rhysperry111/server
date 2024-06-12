using Bit.Core.Auth.Models.Business.Tokenables;
using Bit.Core.Auth.Services;
using Bit.Core.Entities;
using Bit.Core.Tokens;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Bit.Core.Auth.Identity;

public class DuoUniversalTokenProvider : IUserTwoFactorTokenProvider<User>
{
    private readonly IServiceProvider _serviceProvider;

    public DuoUniversalTokenProvider(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<bool> CanGenerateTwoFactorTokenAsync(UserManager<User> manager, User user)
    {
        var duoClientService = _serviceProvider.GetRequiredService<IDuoUniversalClientService>();
        if (await duoClientService.UserCanUseDuoTwoFactor(user) == null)
        {
            return false;
        }
        return true;
    }

    public async Task<string> GenerateAsync(string purpose, UserManager<User> manager, User user)
    {
        var duoClientService = _serviceProvider.GetRequiredService<IDuoUniversalClientService>();
        var duoClient = await duoClientService.BuildDuoClientAsync(user);
        if (duoClient == null)
        {
            return null;
        }

        var dataProtectionService = _serviceProvider.GetRequiredService<IDataProtectorTokenFactory<DuoUserStateTokenable>>();
        var state = dataProtectionService.Protect(new DuoUserStateTokenable(user));
        var authUrl = duoClient.GenerateAuthUri(user.Email, state);

        return authUrl;
    }

    /// <summary>
    /// Validates Duo SDK v4 response
    /// </summary>
    /// <param name="token">response form Duo</param>
    /// <param name="provider">TwoFactorProviderType Duo or OrganizationDuo</param>
    /// <param name="user">self</param>
    /// <returns>true or false depending on result of verification</returns>
    public async Task<bool> ValidateAsync(string purpose, string token, UserManager<User> manager, User user)
    {
        var duoClientService = _serviceProvider.GetRequiredService<IDuoUniversalClientService>();
        var duoClient = await duoClientService.BuildDuoClientAsync(user);
        if (duoClient == null)
        {
            return false;
        }

        var dataProtectionService = _serviceProvider.GetRequiredService<IDataProtectorTokenFactory<DuoUserStateTokenable>>();

        var parts = token.Split("|");
        var authCode = parts[0];
        var state = parts[1];

        dataProtectionService.TryUnprotect(state, out var tokenable);
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
