using HE.FMS.Middleware.BusinessLogic.Constants;
using HE.FMS.Middleware.BusinessLogic.Efin;
using HE.FMS.Middleware.Contract.Efin.CosmosDb;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace HE.FMS.Middleware.BusinessLogic.Tests.Fakes;
public sealed class FakeEfinLookupService : IEfinLookupCacheService
{
    private const string LookUpSeedFolder = "LookupSeeds";

    public async Task<Dictionary<string, string>> GetValue(string key)
    {
        return key switch
        {
            var k when k.Equals(EfinConstants.Lookups.ClaimDefault, StringComparison.OrdinalIgnoreCase)
                => await GetDictionaryFromFile(EfinConstants.Lookups.ClaimDefault),

            var k when k.Equals(EfinConstants.Lookups.ReclaimDefault, StringComparison.OrdinalIgnoreCase)
                => await GetDictionaryFromFile(EfinConstants.Lookups.ReclaimDefault),

            var k when k.Equals(EfinConstants.Lookups.RegionLookup, StringComparison.OrdinalIgnoreCase)
                => await GetDictionaryFromFile(EfinConstants.Lookups.RegionLookup),

            var k when k.Equals(EfinConstants.Lookups.MilestoneLookup, StringComparison.OrdinalIgnoreCase)
                => await GetDictionaryFromFile(EfinConstants.Lookups.MilestoneLookup),

            var k when k.Equals(EfinConstants.Lookups.MilestoneShortLookup, StringComparison.OrdinalIgnoreCase)
                => await GetDictionaryFromFile(EfinConstants.Lookups.MilestoneShortLookup),

            var k when k.Equals(EfinConstants.Lookups.TenureLookup, StringComparison.OrdinalIgnoreCase)
                => await GetDictionaryFromFile(EfinConstants.Lookups.TenureLookup),

            var k when k.Equals(EfinConstants.Lookups.RevenueIndicatorLookup, StringComparison.OrdinalIgnoreCase)
                => await GetDictionaryFromFile(EfinConstants.Lookups.RevenueIndicatorLookup),

            var k when k.Equals(EfinConstants.Lookups.PartnerTypeLookup, StringComparison.OrdinalIgnoreCase)
                => await GetDictionaryFromFile(EfinConstants.Lookups.PartnerTypeLookup),

            _ => new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase),
        };
    }

    public void InvalidateKey(string key)
    {
        throw new NotImplementedException();
    }

    private async Task<Dictionary<string, string>> GetDictionaryFromFile(string fileName)
    {
        var item = await DeserializeJsonFileAsync(LookUpSeedFolder, $"{fileName}.json");
        var json = (JObject)item.Value;
        var dictionary = json.ToObject<Dictionary<string, string>>() ?? throw new InvalidCastException();

        return new Dictionary<string, string>(dictionary, StringComparer.OrdinalIgnoreCase);
    }

#pragma warning disable S2325
    private async Task<EfinLookupItem> DeserializeJsonFileAsync(string directoryPath, string fileName)
    {
        var solutionDirectory = TestContext.CurrentContext.TestDirectory;
        var relativePathToDataFiles = Path.Combine(solutionDirectory, @"../../../../" + directoryPath);

        var filePath = Path.Combine(relativePathToDataFiles, fileName);

        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"The file at {filePath} was not found.");
        }

        var json = await File.ReadAllTextAsync(filePath);
        return JsonConvert.DeserializeObject<EfinLookupItem>(json)!;
    }
#pragma warning restore S2325
}
