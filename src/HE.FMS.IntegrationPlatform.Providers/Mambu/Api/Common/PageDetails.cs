using HE.FMS.IntegrationPlatform.Providers.Mambu.Api.Common.Enums;

namespace HE.FMS.IntegrationPlatform.Providers.Mambu.Api.Common;

public sealed record PageDetails(
    int? Offset = null,
    int? Limit = null,
    PaginationDetails PaginationDetails = PaginationDetails.Off,
    DetailsLevel DetailsLevel = DetailsLevel.Basic);
