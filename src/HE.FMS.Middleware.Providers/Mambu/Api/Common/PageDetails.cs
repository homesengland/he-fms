using HE.FMS.Middleware.Providers.Mambu.Api.Common.Enums;

namespace HE.FMS.Middleware.Providers.Mambu.Api.Common;

public sealed record PageDetails(
    int? Offset = null,
    int? Limit = null,
    PaginationDetails PaginationDetails = PaginationDetails.Off,
    DetailsLevel DetailsLevel = DetailsLevel.Basic);
