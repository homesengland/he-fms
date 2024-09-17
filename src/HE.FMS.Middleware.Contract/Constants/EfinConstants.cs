namespace HE.FMS.Middleware.Contract.Constants;
public static class EfinConstants
{
    public static class Default
    {
        public static class Claim
        {
            public const string SubLedger = "PL4";
            public const string User = "AH GRANTS";
            public const string Grouping = "C";
            public const string Amount = "0";

            // ReSharper disable once InconsistentNaming
            public const string UOM = "EA";
            public const string UnitQuantity = "1";
            public const string Volume = "1";
            public const string TransType = "PI";
            public const string TermsCode = "00";

            public static class CapitalPartnerType
            {
                // ReSharper disable once InconsistentNaming
                public const int ALBArmsLengthBodyofGovernment = 1552;

                // ReSharper disable once InconsistentNaming
                public const int ALMOArmsLengthManagementOrganisation = 1553;
                public const int Bank = 1551;
                public const int CombinedAuthority = 1553;
                public const int Consultant = 1551;
                public const int Education = 1552;
                public const int FinancialInstitution = 1551;
                public const int ForProfitRegisteredProvder = 1558;
                public const int GovernmentPolicyMaker = 1552;
                public const int HealthandSocialCare = 1552;
                public const int Insurer = 1551;
                public const int InvestmentManager = 1551;
                public const int Landowner = 1551;
                public const int LocalAuthority = 1553;
                public const int NonregisteredCharitableOrganisation = 1551;
                public const int NonBankLender = 1558;
                public const int NotForProfitRegisteredProvider = 1558;
                public const int Other = 1551;
                public const int OtherFinancialInstitutionorAdvisory = 1551;
                public const int PrivateContractor = 1551;
                public const int PrivateSectorHousebuilder = 1551;
                public const int PublicPrivatePartnership = 1552;
                public const int RegisteredCharitableOrganisation = 1558;
                public const int TradeAssociation = 1551;
                public const int UnregisteredHousingAssociation = 1558;

                public static readonly IReadOnlyDictionary<string, int> Lookup = new Dictionary<string, int>
                {
                    { nameof(ALBArmsLengthBodyofGovernment), ALBArmsLengthBodyofGovernment },
                    { nameof(ALMOArmsLengthManagementOrganisation), ALMOArmsLengthManagementOrganisation },
                    { nameof(Bank), Bank },
                    { nameof(CombinedAuthority), CombinedAuthority },
                    { nameof(Consultant), Consultant },
                    { nameof(Education), Education },
                    { nameof(FinancialInstitution), FinancialInstitution },
                    { nameof(ForProfitRegisteredProvder), ForProfitRegisteredProvder },
                    { nameof(GovernmentPolicyMaker), GovernmentPolicyMaker },
                    { nameof(HealthandSocialCare), HealthandSocialCare },
                    { nameof(Insurer), Insurer },
                    { nameof(InvestmentManager), Landowner },
                    { nameof(LocalAuthority), LocalAuthority },
                    { nameof(NonregisteredCharitableOrganisation), NonregisteredCharitableOrganisation },
                    { nameof(NonBankLender), NonBankLender },
                    { nameof(NotForProfitRegisteredProvider), NotForProfitRegisteredProvider },
                    { nameof(Other), Other },
                    { nameof(OtherFinancialInstitutionorAdvisory), OtherFinancialInstitutionorAdvisory },
                    { nameof(PrivateContractor), PrivateContractor },
                    { nameof(PrivateSectorHousebuilder), PrivateSectorHousebuilder },
                    { nameof(PublicPrivatePartnership), PublicPrivatePartnership },
                    { nameof(RegisteredCharitableOrganisation), RegisteredCharitableOrganisation },
                    { nameof(TradeAssociation), TradeAssociation },
                    { nameof(UnregisteredHousingAssociation), UnregisteredHousingAssociation },
                };
            }

            public static class RevenuePartnerType
            {
                // ReSharper disable once InconsistentNaming
                public const int ALBArmsLengthBodyofGovernment = 1554;

                // ReSharper disable once InconsistentNaming
                public const int ALMOArmsLengthManagementOrganisation = 1555;
                public const int Bank = 1575;
                public const int CombinedAuthority = 1555;
                public const int Consultant = 1575;
                public const int Education = 1555;
                public const int FinancialInstitution = 1575;
                public const int ForProfitRegisteredProvder = 1559;
                public const int GovernmentPolicyMaker = 1554;
                public const int HealthandSocialCare = 1555;
                public const int Insurer = 1575;
                public const int InvestmentManager = 1575;
                public const int Landowner = 1575;
                public const int LocalAuthority = 1555;
                public const int NonregisteredCharitableOrganisation = 1575;
                public const int NonBankLender = 1559;
                public const int NotForProfitRegisteredProvider = 1559;
                public const int Other = 1575;
                public const int OtherFinancialInstitutionorAdvisory = 1575;
                public const int PrivateContractor = 1575;
                public const int PrivateSectorHousebuilder = 1575;
                public const int PublicPrivatePartnership = 1554;
                public const int RegisteredCharitableOrganisation = 1559;
                public const int TradeAssociation = 1575;
                public const int UnregisteredHousingAssociation = 1559;

                public static readonly IReadOnlyDictionary<string, int> Lookup = new Dictionary<string, int>
                {
                    { nameof(ALBArmsLengthBodyofGovernment), ALBArmsLengthBodyofGovernment },
                    { nameof(ALMOArmsLengthManagementOrganisation), ALMOArmsLengthManagementOrganisation },
                    { nameof(Bank), Bank },
                    { nameof(CombinedAuthority), CombinedAuthority },
                    { nameof(Consultant), Consultant },
                    { nameof(Education), Education },
                    { nameof(FinancialInstitution), FinancialInstitution },
                    { nameof(ForProfitRegisteredProvder), ForProfitRegisteredProvder },
                    { nameof(GovernmentPolicyMaker), GovernmentPolicyMaker },
                    { nameof(HealthandSocialCare), HealthandSocialCare },
                    { nameof(Insurer), Insurer },
                    { nameof(InvestmentManager), InvestmentManager },
                    { nameof(Landowner), Landowner },
                    { nameof(LocalAuthority), LocalAuthority },
                    { nameof(NonregisteredCharitableOrganisation), NonregisteredCharitableOrganisation },
                    { nameof(NonBankLender), NonBankLender },
                    { nameof(NotForProfitRegisteredProvider), NotForProfitRegisteredProvider },
                    { nameof(Other), Other },
                    { nameof(OtherFinancialInstitutionorAdvisory), OtherFinancialInstitutionorAdvisory },
                    { nameof(PrivateContractor), PrivateContractor },
                    { nameof(PrivateSectorHousebuilder), PrivateSectorHousebuilder },
                    { nameof(PublicPrivatePartnership), PublicPrivatePartnership },
                    { nameof(RegisteredCharitableOrganisation), RegisteredCharitableOrganisation },
                    { nameof(TradeAssociation), TradeAssociation },
                    { nameof(UnregisteredHousingAssociation), UnregisteredHousingAssociation },
                };
            }

            public static class FileNamePrefix
            {
                public const string ClclbBatch = "cpap_b";
                public const string ClaInvoiceAnalysis = "cpap_a";
                public const string CliInvoice = "cpap_i";
            }
        }

        public static class Reclaim
        {
            public const string SubLedger = "SL4";
            public const string Description = "AHP-RECLAIM";
            public const string User = "GRANTS";
            public const string Prefix = "H";
            public const string HeaderFooter = "H";
            public const string TransType = "I";
            public const string TermsCode = "00";
            public const string ItemSequence = "1";
            public const string PrintSequence = "1";
            public const string Product = "GRANT RECLAIM";
            public const string Line = "1";
            public const string Text = "HOUSING FOR RENT GRANT RECLAIM";

            // ReSharper disable once InconsistentNaming
            public const string UOM = "EA";
            public const string PrePay = "N";

            public static class AmountReclaim
            {
                // ReSharper disable once InconsistentNaming
                public const int ALBArmsLengthBodyofGovernment = 0028;

                // ReSharper disable once InconsistentNaming
                public const int ALMOArmsLengthManagementOrganisation = 0027;
                public const int Bank = 0029;
                public const int CombinedAuthority = 0027;
                public const int Consultant = 0029;
                public const int Education = 0028;
                public const int FinancialInstitution = 0029;
                public const int ForProfitRegisteredProvder = 0030;
                public const int GovernmentPolicyMaker = 0028;
                public const int HealthandSocialCare = 0028;
                public const int Insurer = 0029;
                public const int InvestmentManager = 0029;
                public const int Landowner = 0029;
                public const int LocalAuthority = 0027;
                public const int NonregisteredCharitableOrganisation = 0029;
                public const int NonBankLender = 0030;
                public const int NotForProfitRegisteredProvider = 0030;
                public const int Other = 0029;
                public const int OtherFinancialInstitutionorAdvisory = 0029;
                public const int PrivateContractor = 0029;
                public const int PrivateSectorHousebuilder = 0029;
                public const int PublicPrivatePartnership = 0028;
                public const int RegisteredCharitableOrganisation = 0030;
                public const int TradeAssociation = 0029;
                public const int UnregisteredHousingAssociation = 0030;

                public static readonly IReadOnlyDictionary<string, int> Lookup = new Dictionary<string, int>
                {
                    { nameof(ALBArmsLengthBodyofGovernment), ALBArmsLengthBodyofGovernment },
                    { nameof(ALMOArmsLengthManagementOrganisation), ALMOArmsLengthManagementOrganisation },
                    { nameof(Bank), Bank },
                    { nameof(CombinedAuthority), CombinedAuthority },
                    { nameof(Consultant), Consultant },
                    { nameof(Education), Education },
                    { nameof(FinancialInstitution), FinancialInstitution },
                    { nameof(ForProfitRegisteredProvder), ForProfitRegisteredProvder },
                    { nameof(GovernmentPolicyMaker), GovernmentPolicyMaker },
                    { nameof(HealthandSocialCare), HealthandSocialCare },
                    { nameof(Insurer), Insurer },
                    { nameof(InvestmentManager), InvestmentManager },
                    { nameof(Landowner), Landowner },
                    { nameof(LocalAuthority), LocalAuthority },
                    { nameof(NonregisteredCharitableOrganisation), NonregisteredCharitableOrganisation },
                    { nameof(NonBankLender), NonBankLender },
                    { nameof(NotForProfitRegisteredProvider), NotForProfitRegisteredProvider },
                    { nameof(Other), Other },
                    { nameof(OtherFinancialInstitutionorAdvisory), OtherFinancialInstitutionorAdvisory },
                    { nameof(PrivateContractor), PrivateContractor },
                    { nameof(PrivateSectorHousebuilder), PrivateSectorHousebuilder },
                    { nameof(PublicPrivatePartnership), PublicPrivatePartnership },
                    { nameof(RegisteredCharitableOrganisation), RegisteredCharitableOrganisation },
                    { nameof(TradeAssociation), TradeAssociation },
                    { nameof(UnregisteredHousingAssociation), UnregisteredHousingAssociation },
                };
            }

            public static class InterestAmount
            {
                // ReSharper disable once InconsistentNaming
                public const int ALBArmsLengthBodyofGovernment = 0067;

                // ReSharper disable once InconsistentNaming
                public const int ALMOArmsLengthManagementOrganisation = 0067;
                public const int Bank = 0067;
                public const int CombinedAuthority = 0067;
                public const int Consultant = 0067;
                public const int Education = 0067;
                public const int FinancialInstitution = 0067;
                public const int ForProfitRegisteredProvder = 0067;
                public const int GovernmentPolicyMaker = 0067;
                public const int HealthandSocialCare = 0067;
                public const int Insurer = 0067;
                public const int InvestmentManager = 0067;
                public const int Landowner = 0067;
                public const int LocalAuthority = 0067;
                public const int NonregisteredCharitableOrganisation = 0067;
                public const int NonBankLender = 0067;
                public const int NotForProfitRegisteredProvider = 0067;
                public const int Other = 0067;
                public const int OtherFinancialInstitutionorAdvisory = 0067;
                public const int PrivateContractor = 0067;
                public const int PrivateSectorHousebuilder = 0067;
                public const int PublicPrivatePartnership = 0067;
                public const int RegisteredCharitableOrganisation = 0067;
                public const int TradeAssociation = 0067;
                public const int UnregisteredHousingAssociation = 0067;

                public static readonly IReadOnlyDictionary<string, int> Lookup = new Dictionary<string, int>
                {
                    { nameof(ALBArmsLengthBodyofGovernment), ALBArmsLengthBodyofGovernment },
                    { nameof(ALMOArmsLengthManagementOrganisation), ALMOArmsLengthManagementOrganisation },
                    { nameof(Bank), Bank },
                    { nameof(CombinedAuthority), CombinedAuthority },
                    { nameof(Consultant), Consultant },
                    { nameof(Education), Education },
                    { nameof(FinancialInstitution), FinancialInstitution },
                    { nameof(ForProfitRegisteredProvder), ForProfitRegisteredProvder },
                    { nameof(GovernmentPolicyMaker), GovernmentPolicyMaker },
                    { nameof(HealthandSocialCare), HealthandSocialCare },
                    { nameof(Insurer), Insurer },
                    { nameof(InvestmentManager), InvestmentManager },
                    { nameof(Landowner), Landowner },
                    { nameof(LocalAuthority), LocalAuthority },
                    { nameof(NonregisteredCharitableOrganisation), NonregisteredCharitableOrganisation },
                    { nameof(NonBankLender), NonBankLender },
                    { nameof(NotForProfitRegisteredProvider), NotForProfitRegisteredProvider },
                    { nameof(Other), Other },
                    { nameof(OtherFinancialInstitutionorAdvisory), OtherFinancialInstitutionorAdvisory },
                    { nameof(PrivateContractor), PrivateContractor },
                    { nameof(PrivateSectorHousebuilder), PrivateSectorHousebuilder },
                    { nameof(PublicPrivatePartnership), PublicPrivatePartnership },
                    { nameof(RegisteredCharitableOrganisation), RegisteredCharitableOrganisation },
                    { nameof(TradeAssociation), TradeAssociation },
                    { nameof(UnregisteredHousingAssociation), UnregisteredHousingAssociation },
                };
            }

            public static class FileNamePrefix
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

    public static class Milestone
    {
        public const string Acquisition = "ACQ";
        public const string StartOnSite = "SOS";
        public const string PracticalCompletion = "PC";
    }

    public static class Region
    {
        public const string North = "ASWRN";
        public const string South = "ASWRS";
        public const string Midlands = "ASWRM";
        public const string London = "ASWRL";
    }

    public static class RevenueIndicator
    {
        public const string Revenue = "Revenue";
        public const string Capital = "Capital";
    }

    public static class Tenure
    {
        public const int AffordableRent = 7572;
        public const int SocialRent = 7667;
        public const int SharedOwnership = 7666;
        public const int RentToBuy = 7655;

        // ReSharper disable InconsistentNaming
        public const int HOLD = 7666;
        public const int OPSO = 7666;

        // ReSharper restore InconsistentNaming
    }
}
