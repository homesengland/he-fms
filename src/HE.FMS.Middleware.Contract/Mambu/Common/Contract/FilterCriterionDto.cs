namespace HE.FMS.Middleware.Contract.Mambu.Common.Contract;

public sealed class FilterCriterionDto
{
    public string Field { get; set; }

    public string Operator { get; set; }

    public string? SecondValue { get; set; }

    public string? Value { get; set; }

    public IList<string>? Values { get; set; }

    public static FilterCriterionDto Equals(string field, string value)
    {
        return new FilterCriterionDto { Field = field, Operator = "EQUALS", Value = value };
    }
}
