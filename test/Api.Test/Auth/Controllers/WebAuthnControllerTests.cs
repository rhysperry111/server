﻿using Bit.Api.Auth.Controllers;
using Bit.Api.Auth.Models.Request.Accounts;
using Bit.Api.Auth.Models.Request.Webauthn;
using Bit.Core.Services;
using Bit.Test.Common.AutoFixture;
using Bit.Test.Common.AutoFixture.Attributes;
using NSubstitute.ReturnsExtensions;
using Xunit;

namespace Bit.Api.Test.Auth.Controllers;

[ControllerCustomize(typeof(WebAuthnController))]
[SutProviderCustomize]
public class WebAuthnControllerTests
{
    [Theory, BitAutoData]
    public async Task Get_UserNotFound_ThrowsUnauthorizedAccessException(SutProvider<WebAuthnController> sutProvider)
    {
        // Arrange 
        sutProvider.GetDependency<IUserService>().GetUserByPrincipalAsync(default).ReturnsNullForAnyArgs();

        // Act
        var result = () => sutProvider.Sut.Get();

        // Assert
        await Assert.ThrowsAsync<UnauthorizedAccessException>(result);
    }

    [Theory, BitAutoData]
    public async Task PostOptions_UserNotFound_ThrowsUnauthorizedAccessException(SecretVerificationRequestModel requestModel, SutProvider<WebAuthnController> sutProvider)
    {
        // Arrange 
        sutProvider.GetDependency<IUserService>().GetUserByPrincipalAsync(default).ReturnsNullForAnyArgs();

        // Act
        var result = () => sutProvider.Sut.PostOptions(requestModel);

        // Assert
        await Assert.ThrowsAsync<UnauthorizedAccessException>(result);
    }

    [Theory, BitAutoData]
    public async Task Post_UserNotFound_ThrowsUnauthorizedAccessException(WebAuthnCredentialRequestModel requestModel, SutProvider<WebAuthnController> sutProvider)
    {
        // Arrange 
        sutProvider.GetDependency<IUserService>().GetUserByPrincipalAsync(default).ReturnsNullForAnyArgs();

        // Act
        var result = () => sutProvider.Sut.Post(requestModel);

        // Assert
        await Assert.ThrowsAsync<UnauthorizedAccessException>(result);
    }

    [Theory, BitAutoData]
    public async Task Delete_UserNotFound_ThrowsUnauthorizedAccessException(Guid credentialId, SecretVerificationRequestModel requestModel, SutProvider<WebAuthnController> sutProvider)
    {
        // Arrange 
        sutProvider.GetDependency<IUserService>().GetUserByPrincipalAsync(default).ReturnsNullForAnyArgs();

        // Act
        var result = () => sutProvider.Sut.Delete(credentialId, requestModel);

        // Assert
        await Assert.ThrowsAsync<UnauthorizedAccessException>(result);
    }
}

