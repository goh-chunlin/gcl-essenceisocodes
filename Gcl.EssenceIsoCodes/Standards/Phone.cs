using Gcl.EssenceIsoCodes.Utils;

namespace Gcl.EssenceIsoCodes.Standards;

/// <summary>
/// Class Phone follows International Telecommunication Union Telecommunication Standardization Sector
/// ITU-T E.164.<br />
/// Reference:
/// <see href="https://en.wikipedia.org/wiki/List_of_country_calling_codes" /><br />
/// </summary>
public class Phone
{
    /// <summary>
    /// Country code.
    /// </summary>
    public required string CountryCode { get; init; }

    /// <summary>
    /// Phone code.
    /// </summary>
    public required string PhoneCode { get; init; }

    /// <summary>
    /// This method is used to get all phone codes.
    /// </summary>
    /// <returns>
    /// Phones with the country code and phone code of countries.
    /// </returns>
    public static async Task<HashSet<Phone>> GetPhonesAsync()
    {
        var lines = await FileManagement.ReadDataFileContentAsync("phonecodes.csv");

        return lines.Skip(1).Select(line =>
            line.Split(',').ToArray()).Select(
            fields => new Phone
            {
                CountryCode = fields[0].Trim(), 
                PhoneCode = fields[1].Trim()
            }).ToHashSet();
    }
    
    public override bool Equals(object? obj)
    {
        if (obj is Phone phone)
        {
            return PhoneCode == phone.PhoneCode;
        }
        
        return false;
    }

    public override int GetHashCode()
    {
        return PhoneCode.GetHashCode();
    }
}