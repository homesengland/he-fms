namespace HE.FMS.IntegrationPlatform.Providers.Mambu.Api.Common.Contract;

public sealed class SearchCriteriaDto
{
    public IList<FilterCriterionDto> FilterCriteria { get; set; }

    public SortingCriterionDto SortingCriteriaDto { get; set; }
}
