namespace HE.FMS.IntegrationPlatform.Providers.Mambu.Api.Common.Contract;

public sealed class FilterCriterionDto
{
    public string Field { get; set; }

    public string Operator { get; set; }

    public string? SecondValue { get; set; }

    public string? Value { get; set; }

    public IList<string>? Values { get; set; }
}
