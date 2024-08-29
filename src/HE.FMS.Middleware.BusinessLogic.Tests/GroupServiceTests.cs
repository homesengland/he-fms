using HE.FMS.Middleware.BusinessLogic.Grants.Services;
using HE.FMS.Middleware.BusinessLogic.Grants.Settings;
using HE.FMS.Middleware.Contract.Common;
using HE.FMS.Middleware.Providers.Mambu.Api.Common.Enums;
using HE.FMS.Middleware.Providers.Mambu.Api.Group;
using HE.FMS.Middleware.Providers.Mambu.Api.Group.Contract;
using NSubstitute;
using Xunit;

namespace HE.FMS.Middleware.BusinessLogic.Tests;

public class GroupServiceTests
{
    private readonly IMambuGroupApiClient _groupApiClient;
    private readonly IGrantsSettings _grantsSettings;
    private readonly GroupService _groupService;

    public GroupServiceTests()
    {
        _groupApiClient = Substitute.For<IMambuGroupApiClient>();
        _grantsSettings = Substitute.For<IGrantsSettings>();
        _groupService = new GroupService(_groupApiClient, _grantsSettings);
    }

    [Fact]
    public async Task GetOrCreateGroup_GroupExists_ReturnsGroup()
    {
        // Arrange
        var organisation = new OrganisationContract { Id = "org1" };
        var groupReadDto = new GroupReadDto { Id = "org1" };
        _groupApiClient.GetById(organisation.Id, DetailsLevel.Full, Arg.Any<CancellationToken>())!
            .Returns(Task.FromResult(groupReadDto));

        // Act
        var result = await _groupService.GetOrCreateGroup(organisation, CancellationToken.None);

        // Assert
        Assert.Equal(groupReadDto, result);
    }

    [Fact]
    public async Task GetOrCreateGroup_GroupDoesNotExist_CreatesAndReturnsGroup()
    {
        // Arrange
        var organisation = new OrganisationContract
        {
            Id = "org1",
            Name = "Org Name",
            Address = new AddressContract
            {
                Country = "Country",
                City = "City",
                Line1 = "Line1",
                Line2 = "Line2",
            },
            Email = "email@example.com",
            PhoneNumber = "123456789",
            Notes = "Some notes",
        };
        var groupDto = new GroupDto
        {
            Id = "org1",
            GroupName = "Org Name",
            Addresses =
            [
                new()
                {
                    Country = "Country",
                    City = "City",
                    Line1 = "Line1",
                    Line2 = "Line2",
                },
            ],
            EmailAddress = "email@example.com",
            HomePhone = "123456789",
            AssignedBranchKey = "branch1",
            Notes = "Some notes",
        };
        var groupReadDto = new GroupReadDto { Id = "org1" };
        _groupApiClient.GetById(organisation.Id, DetailsLevel.Full, Arg.Any<CancellationToken>())!
            .Returns(Task.FromResult<GroupReadDto>(null!));
        _groupApiClient.Create(Arg.Any<GroupDto>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(groupReadDto));
        _grantsSettings.BranchId.Returns("branch1");

        // Act
        var result = await _groupService.GetOrCreateGroup(organisation, CancellationToken.None);

        // Assert
        await _groupApiClient.Received(1).Create(
            Arg.Is<GroupDto>(dto =>
            dto.Id == groupDto.Id &&
            dto.GroupName == groupDto.GroupName &&
            dto.Addresses![0].Country == groupDto.Addresses[0].Country &&
            dto.Addresses[0].City == groupDto.Addresses[0].City &&
            dto.Addresses[0].Line1 == groupDto.Addresses[0].Line1 &&
            dto.Addresses[0].Line2 == groupDto.Addresses[0].Line2 &&
            dto.EmailAddress == groupDto.EmailAddress &&
            dto.HomePhone == groupDto.HomePhone &&
            dto.AssignedBranchKey == groupDto.AssignedBranchKey &&
            dto.Notes == groupDto.Notes),
            Arg.Any<CancellationToken>());
        Assert.Equal(groupReadDto, result);
    }
}
