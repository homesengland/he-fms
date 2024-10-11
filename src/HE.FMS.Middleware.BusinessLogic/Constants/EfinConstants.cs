namespace HE.FMS.Middleware.BusinessLogic.Constants;
public static class EfinConstants
{
    public static class Lookups
    {
        public const string Claim = nameof(Claim);
        public const string Reclaim = nameof(Reclaim);

        public const string ClaimDefault = nameof(ClaimDefault);
        public const string ReclaimDefault = nameof(ReclaimDefault);
        public const string MilestoneLookup = nameof(MilestoneLookup);
        public const string MilestoneShortLookup = nameof(MilestoneShortLookup);
        public const string RegionLookup = nameof(RegionLookup);
        public const string TenureLookup = nameof(TenureLookup);
        public const string RevenueIndicatorLookup = nameof(RevenueIndicatorLookup);
        public const string PartnerTypeLookup = nameof(PartnerTypeLookup);
    }

    public static class FileNamePrefix
    {
        public static class Claim
        {
            public const string ClclbBatch = "cpap_b";
            public const string ClaInvoiceAnalysis = "cpap_a";
            public const string CliInvoice = "cpap_i";
        }

        public static class Reclaim
        {
            public const string CliIwBat = "cpiw_bt";
            public const string CliIwIlt = "cpiw_lt";
            public const string CliIwIna = "cpiw_ia";
            public const string CliIwInl = "cpiw_il";
            public const string CliIwInv = "cpiw_in";
            public const string CliIwItl = "cpiw_hf";
        }
    }
}
