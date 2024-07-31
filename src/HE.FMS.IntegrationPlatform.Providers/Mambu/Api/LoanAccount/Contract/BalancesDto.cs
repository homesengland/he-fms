namespace HE.FMS.IntegrationPlatform.Providers.Mambu.Api.LoanAccount.Contract;

public sealed class BalancesDto
{
    public decimal? FeesBalance { get; set; }

    public decimal? FeesDue { get; set; }

    public decimal? FeesPaid { get; set; }

    public decimal? HoldBalance { get; set; }

    public decimal? InterestBalance { get; set; }

    public decimal? InterestDue { get; set; }

    public decimal? InterestFromArrearsBalance { get; set; }

    public decimal? InterestFromArrearsDue { get; set; }

    public decimal? InterestFromArrearsPaid { get; set; }

    public decimal? InterestPaid { get; set; }

    public decimal? PenaltyBalance { get; set; }

    public decimal? PenaltyDue { get; set; }

    public decimal? PenaltyPaid { get; set; }

    public decimal? PrincipalBalance { get; set; }

    public decimal? PrincipalDue { get; set; }

    public decimal? PrincipalPaid { get; set; }

    public decimal? RedrawBalance { get; set; }
}
