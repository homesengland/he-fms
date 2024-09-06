namespace HE.FMS.Middleware.Contract.Mambu.Common.Contract;

public sealed class SearchCriteriaDto
{
    public IList<FilterCriterionDto> FilterCriteria { get; set; }

    public SortingCriterionDto SortingCriteriaDto { get; set; }
}
