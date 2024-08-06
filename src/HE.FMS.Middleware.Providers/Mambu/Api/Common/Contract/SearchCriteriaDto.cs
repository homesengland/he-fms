namespace HE.FMS.Middleware.Providers.Mambu.Api.Common.Contract;

public sealed class SearchCriteriaDto
{
    public IList<FilterCriterionDto> FilterCriteria { get; set; }

    public SortingCriterionDto SortingCriteriaDto { get; set; }
}
