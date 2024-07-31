using HE.FMS.IntegrationPlatform.Providers.Mambu.Api.Common.Enums;

namespace HE.FMS.IntegrationPlatform.Providers.Mambu.Api.CreditArrangement.Contract;

public sealed class AddCreditArrangementAccountDto
{
    public string AccountId { get; set; }

    public AccountType AccountType { get; set; }
}
