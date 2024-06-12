using Bit.Core.Auth.Enums;
using Bit.Core.Auth.Models;
using Bit.Core.Services;
using Duo = DuoUniversal;
using Microsoft.Extensions.Logging;
using Bit.Core.Context;
using Bit.Core.Settings;
using Bit.Core.Entities;
using Bit.Core.AdminConsole.Entities;

namespace Bit.Core.Auth.Services.Implementations;
public class DuoUniversalClientService : IDuoUniversalClientService
{
    private readonly IUserService _userService;
    private readonly ICurrentContext _currentContext;
    private readonly GlobalSettings _globalSettings;
    private readonly ILogger<DuoUniversalClientService> _logger;
    public DuoUniversalClientService(
        IUserService userService,
        ICurrentContext currentContext,
        GlobalSettings globalSettings,
        ILogger<DuoUniversalClientService> logger)
    {
        _userService = userService;
        _currentContext = currentContext;
        _globalSettings = globalSettings;
        _logger = logger;
    }

    /// <summary>
    /// Generates a Duo.Client object for use with Duo SDK v4. This combines the health check and the client generation
    /// </summary>
    /// <param name="user">active principal in HttpContext</param>
    /// <returns>Duo.Client object or null</returns>
    public async Task<Duo.Client> BuildDuoClientAsync(User user, Organization organization = null)
    {
        // check for proper Duo configuration
        var provider = organization != null ?
            OrganizationUserCanUseDuoTwoFactor(organization) :
            await UserCanUseDuoTwoFactor(user);

        if (provider == null)
        {
            return null;
        }

        // Fetch Client name from header value since duo auth can be initiated from multiple clients and we want
        // to redirect back to the initiating client
        _currentContext.HttpContext.Request.Headers.TryGetValue("Bitwarden-Client-Name", out var bitwardenClientName);
        var redirectUri = string.Format("{0}/duo-redirect-connector.html?client={1}",
            _globalSettings.BaseServiceUri.Vault, bitwardenClientName.FirstOrDefault() ?? "web");

        var client = new Duo.ClientBuilder(
            (string)provider.MetaData["ClientId"],
            (string)provider.MetaData["ClientSecret"],
            (string)provider.MetaData["Host"],
            redirectUri).Build();

        if (!await client.DoHealthCheck(true))
        {
            _logger.LogError("Unable to connect to Duo. Health check failed.");
            return null;
        }
        return client;
    }

    /// <summary>
    /// Verifies user has access to premium and Duo Two Factor Provider is configured properly
    /// </summary>
    /// <param name="user">active principle in HttpContext</param>
    /// <returns>null if any checks fail, valid TwoFactorProvider otherwise</returns>
    public TwoFactorProvider OrganizationUserCanUseDuoTwoFactor(Organization organization)
    {
        if (organization == null || !organization.Enabled || !organization.Use2fa)
        {
            return null;
        }

        var provider = organization.GetTwoFactorProvider(TwoFactorProviderType.OrganizationDuo);
        if (!HasProperMetaData(provider) || !provider.Enabled)
        {
            return null;
        }

        return provider;
    }

    /// <summary>
    /// Verifies user has access to premium and Duo Two Factor Provider is configured properly
    /// </summary>
    /// <param name="user">active principle in HttpContext</param>
    /// <returns>null if any checks fail, valid TwoFactorProvider otherwise</returns>
    public async Task<TwoFactorProvider> UserCanUseDuoTwoFactor(User user)
    {
        if (!(await _userService.CanAccessPremium(user)))
        {
            return null;
        }

        var provider = user.GetTwoFactorProvider(TwoFactorProviderType.Duo);
        if (!HasProperMetaData(provider) || !provider.Enabled)
        {
            return null;
        }

        return provider;
    }

    private bool HasProperMetaData(TwoFactorProvider provider)
    {
        return provider?.MetaData != null && provider.MetaData.ContainsKey("ClientId") &&
            provider.MetaData.ContainsKey("ClientSecret") && provider.MetaData.ContainsKey("Host");
    }
}
