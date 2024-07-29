﻿using HE.FMS.IntegrationPlatform.BusinessLogic.Grants.Settings;
using HE.FMS.IntegrationPlatform.Contract.Common;
using HE.FMS.IntegrationPlatform.Providers.Mambu.Api.Common.Enums;
using HE.FMS.IntegrationPlatform.Providers.Mambu.Api.Group;
using HE.FMS.IntegrationPlatform.Providers.Mambu.Api.Group.Contract;

namespace HE.FMS.IntegrationPlatform.BusinessLogic.Grants.Services;

internal sealed class GroupService : IGroupService
{
    private readonly IMambuGroupApiClient _groupApiClient;

    private readonly IGrantsSettings _grantsSettings;

    public GroupService(IMambuGroupApiClient groupApiClient, IGrantsSettings grantsSettings)
    {
        _groupApiClient = groupApiClient;
        _grantsSettings = grantsSettings;
    }

    public async Task<GroupReadDto> GetOrCreateGroup(OrganisationContract organisation, CancellationToken cancellationToken)
    {
        return await _groupApiClient.GetById(organisation.Id, DetailsLevel.Full, cancellationToken)
               ?? await _groupApiClient.Create(ToDto(organisation), cancellationToken);
    }

    private GroupDto ToDto(OrganisationContract organisation)
    {
        return new GroupDto
        {
            Id = organisation.Id,
            GroupName = organisation.Name,
            Addresses =
            [
                new GroupAddressDto
                {
                    Country = organisation.Address.Country,
                    City = organisation.Address.City,
                    Line1 = organisation.Address.Line1,
                    Line2 = organisation.Address.Line2,
                },
            ],
            EmailAddress = organisation.Email,
            HomePhone = organisation.PhoneNumber,
            AssignedBranchKey = _grantsSettings.BranchId,
            Notes = organisation.Notes,
        };
    }
}
