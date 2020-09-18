using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

public class LedgerEntry
{
    public LedgerEntry(DateTime date, string description, decimal change)
    {
        Date = date;
        Description = description;
        Change = change;
    }

    public DateTime Date { get; }
    public string Description { get; }
    public decimal Change { get; }
}

public static class Ledger
{ 
    public static LedgerEntry CreateEntry(string date, string desc, int change) =>
        new LedgerEntry(DateTime.Parse(date, CultureInfo.InvariantCulture), desc, change / 100.0m);

    private static (CultureInfo, string) CreateCulture(string currency, string locale)
    {
        int curNeg;
        string datPat;
        string format;
        string currencySymbol = currency switch
        {
            "USD" => "$",
            "EUR" => "€",
            _ => throw new ArgumentException("Invalid currency")
        };
        switch (locale)
        {
            case "en-US":
                curNeg = 0;
                datPat = "MM/dd/yyyy";
                format = $"{"Date", -11}| {"Description", -26}| {"Change", -13}";
                break;
            case "nl-NL":
                curNeg = 12;
                datPat = "dd/MM/yyyy";
                format = $"{"Datum",-11}| {"Omschrijving",-26}| {"Verandering",-13}";
                break;
            default:
                throw new ArgumentException("Invalid locale");
        }

        var culture = new CultureInfo(locale)
        {
            NumberFormat = { CurrencySymbol = currencySymbol, CurrencyNegativePattern = curNeg },
            DateTimeFormat = { ShortDatePattern = datPat }
        };
        return (culture, format);
    }

    private static string PrintEntry(IFormatProvider culture, LedgerEntry entry)
    {
        var date = entry.Date.ToString("d", culture);

        var description = entry.Description.Length > 25 ?
            $"{entry.Description.Substring(0, 22)}..." :
            entry.Description;

        var change = entry.Change < 0.0m ?
            entry.Change.ToString("C", culture) :
            $"{entry.Change.ToString("C", culture)} ";

        return string.Format("{0} | {1, -25} | {2, 13}", date, description, change);
    }

    private static IEnumerable<LedgerEntry> Sort(LedgerEntry[] entries)
    {
        var result = new List<LedgerEntry>();
        result.AddRange(entries.Where(e => e.Change < 0)
            .OrderBy(x => $"{x.Date}@{x.Description}@{x.Change}"));

        result.AddRange(entries.Where(e => e.Change >= 0)
            .OrderBy(x => $"{x.Date}@{x.Description}@{x.Change}"));

        return result;
    }

    public static string Format(string currency, string locale, LedgerEntry[] entries)
    {
        var (item1, item2) = CreateCulture(currency, locale);

        var stringBuilder = new StringBuilder();
        stringBuilder.Append(item2);

        if (entries.Length <= 0)
            return stringBuilder.ToString();
        
        var ledgerEntries = Sort(entries).ToList();

        for (var i = 0; i < ledgerEntries.Count(); i++)
            stringBuilder.Append("\n").Append(PrintEntry(item1, ledgerEntries.Skip(i).First()));

        return stringBuilder.ToString();
    }
}
