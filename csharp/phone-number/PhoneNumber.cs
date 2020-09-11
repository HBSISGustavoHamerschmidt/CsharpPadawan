using System;
using System.Text.RegularExpressions;

public class PhoneNumber
{
    public static string Clean(string phoneNumber)
    {
        var rgx = new Regex("^(?<discarte>\\+?1)?\\D*\\(?(?<areaCode>[2-9][0-9]{2})\\)?\\D*(?<exchangeNumber>[2-9][0-9]{2})\\D*(?<subscriberNumber>[0-9]{4})\\D*$");

        var match = rgx.Match(phoneNumber);

        return match.Success
            ? $"{match.Groups["areaCode"].Value}{match.Groups["exchangeNumber"].Value}{match.Groups["subscriberNumber"].Value}"
            : throw new ArgumentException();
    }
}