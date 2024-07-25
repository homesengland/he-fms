using HE.FMS.IntegrationPlatform.Contract.Common;
using HE.FMS.IntegrationPlatform.Domain.Grants.Settings;
using HE.FMS.IntegrationPlatform.Providers.Mambu.Api.Group.Contract;

namespace HE.FMS.IntegrationPlatform.Domain.Grants.Mappers;

internal sealed class OrganisationMapper : IOrganisationMapper
{
    private readonly IGrantsSettings _grantsSettings;

    public OrganisationMapper(IGrantsSettings grantsSettings)
    {
        _grantsSettings = grantsSettings;
    }

    public GroupDto ToGroupDto(OrganisationContract organisation)
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

    public OrganisationContract ToOrganisation(GroupDto group)
    {
        var address = group.Addresses?.FirstOrDefault();

        return new OrganisationContract
        {
            Id = group.Id,
            Name = group.GroupName,
            Address = new AddressContract
            {
                City = address?.City,
                Country = address?.Country,
                Line1 = address?.Line1,
                Line2 = address?.Line2,
            },
            Email = group.EmailAddress,
            PhoneNumber = group.HomePhone,
            Notes = group.Notes,
        };
    }
}
