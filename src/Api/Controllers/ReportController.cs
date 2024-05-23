﻿using Api.AdminConsole.Services;
using Api.Services;
using Bit.Api.AdminConsole.Models.Response;
using Bit.Api.AdminConsole.Models.Response.Organizations;
using Bit.Api.Models.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace Bit.Api.Controllers;

[Route("report")]
[Authorize("Application")]
public class ReportController : ODataController
{
    private readonly IOrganizationUserControllerService _organizationUserControllerService;
    private readonly IGroupsControllerService _groupsControllerService;
    private readonly ICollectionsControllerService _collectionsControllerService;

    public ReportController(
        IOrganizationUserControllerService organizationUserControllerService,
        IGroupsControllerService groupsControllerService,
        ICollectionsControllerService collectionsControllerService)
    {
        _organizationUserControllerService = organizationUserControllerService;
        _groupsControllerService = groupsControllerService;
        _collectionsControllerService = collectionsControllerService;
    }

    [HttpGet("organizations({key})/members")]
    [EnableQuery]
    public async Task<IEnumerable<OrganizationUserUserDetailsResponseModel>> GetMemberAccess(
        Guid key,
        bool includeGroups = false,
        bool includeCollections = false)
    {
        var orgUsers = await _organizationUserControllerService.GetOrganizationUserUserDetails(User, key, includeGroups, includeCollections);
        return orgUsers;
    }

    [HttpGet("Organizations({key})/groups")]
    [EnableQuery]
    public async Task<IEnumerable<GroupDetailsResponseModel>> GetOrganizationGroups(Guid key)
    {
        var orgGroups = await _groupsControllerService.GetGroups(User, key);
        return orgGroups;
    }

    [HttpGet("Organizations({key})/collections")]
    [EnableQuery]
    public async Task<IEnumerable<CollectionResponseModel>> GetOrganizationCollections(Guid key)
    {
        var orgCollections = await _collectionsControllerService.GetOrganizationCollections(User, key);
        return orgCollections;
    }
}
