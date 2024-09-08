using HE.FMS.Middleware.Contract.Mambu.Common.Enums;

namespace HE.FMS.Middleware.Contract.Mambu.Common;

public sealed record PageDetails(
    int? Offset = null,
    int? Limit = null,
    PaginationDetails PaginationDetails = PaginationDetails.Off,
    DetailsLevel DetailsLevel = DetailsLevel.Basic);
