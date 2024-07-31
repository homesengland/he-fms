using System.ComponentModel;

namespace HE.FMS.IntegrationPlatform.Providers.Mambu.Api.LoanAccount.Contract;

public enum GuarantorType
{
    [Description("CLIENT")]
    Client,

    [Description("GROUP")]
    Group,
}
