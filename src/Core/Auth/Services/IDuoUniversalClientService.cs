using Bit.Core.AdminConsole.Entities;
using Bit.Core.Auth.Models;
using Bit.Core.Entities;
using Duo = DuoUniversal;

namespace Bit.Core.Auth.Services;
public interface IDuoUniversalClientService
{
    Task<Duo.Client> BuildDuoClientAsync(User user, Organization organization = null);
    Task<TwoFactorProvider> UserCanUseDuoTwoFactor(User user);
    TwoFactorProvider OrganizationUserCanUseDuoTwoFactor(Organization organization);
}
