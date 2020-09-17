using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

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
    // Changed the name of the variables that were badly written.

    // To expression body
   public static LedgerEntry CreateEntry(string date, string desc, int change) => 
       new LedgerEntry(DateTime.Parse(date, CultureInfo.InvariantCulture), desc, change / 100.0m);
   

   private static (CultureInfo, string) CreateCulture(string currency, string locale)
   {
       string currencySymbol = null;
       int curNeg = 0;
       string datPat = null;
       string format = string.Empty;

       // Changed validation Ifs to a single if to reduce code redundancy.
       if ((currency != "USD" && currency != "EUR") || (locale != "nl-NL" && locale != "en-US"))
           throw new ArgumentException("Invalid currency");

#pragma warning disable 8509
       currencySymbol = currency switch
#pragma warning restore 8509
       {
           "USD" => "$",
           "EUR" => "€",
       };
        // Used String Interpolation and padding to format strings
       switch (locale)
       {
           case "en-US":
               datPat = "MM/dd/yyyy";
               format = $"{"Date"?.PadRight(11,' ')}| {"Description"?.PadRight(26, ' ')}| {"Change"?.PadRight(13, ' ')}";
               break;
           case "nl-NL":
               curNeg = 12;
               datPat = "dd/MM/yyyy";
               format = $"{"Datum"?.PadRight(11, ' ')}| {"Omschrijving"?.PadRight(26, ' ')}| {"Verandering"?.PadRight(13, ' ')}"; ;
                break;
       }

       var culture = new CultureInfo(locale);
       culture.NumberFormat.CurrencySymbol = currencySymbol;
       culture.NumberFormat.CurrencyNegativePattern = curNeg;
       culture.DateTimeFormat.ShortDatePattern = datPat;
       return (culture, format);
   }

   // Incorporated PrintHead onto CreateCulture
   
   private static string Date(IFormatProvider culture, DateTime date) => date.ToString("d", culture);

   private static string Description(string desc)
   {
       if (desc.Length > 25)
       {
           var trunc = desc.Substring(0, 22);
           trunc += "...";
           return trunc;
       }

       return desc;
   }

   private static string Change(IFormatProvider culture, decimal cgh)
   {
       return cgh < 0.0m ? cgh.ToString("C", culture) : cgh.ToString("C", culture) + " ";
   }

   private static string PrintEntry(IFormatProvider culture, LedgerEntry entry)
   {
       var formatted = "";
       var date = Date(culture, entry.Date);
       var description = Description(entry.Description);
       var change = Change(culture, entry.Change);

       formatted += date;
       formatted += " | ";
       formatted += string.Format("{0,-25}", description);
       formatted += " | ";
       formatted += string.Format("{0,13}", change);

       return formatted;
   }


   private static IEnumerable<LedgerEntry> sort(LedgerEntry[] entries)
   {
       var neg = entries.Where(e => e.Change < 0).OrderBy(x => x.Date + "@" + x.Description + "@" + x.Change);
       var post = entries.Where(e => e.Change >= 0).OrderBy(x => x.Date + "@" + x.Description + "@" + x.Change);

       var result = new List<LedgerEntry>();
       result.AddRange(neg);
       result.AddRange(post);

       return result;
   }

   public static string Format(string currency, string locale, LedgerEntry[] entries)
   {
       // var formatted = "";
       // formatted += PrintHead(locale);
       

       var culture = CreateCulture(currency, locale);

       if (entries.Length > 0)
       {
           var entriesForOutput = sort(entries);

           for (var i = 0; i < entriesForOutput.Count(); i++)
           {
               culture.Item2 += "\n" + PrintEntry(culture.Item1, entriesForOutput.Skip(i).First());
           }
       }

       return culture.Item2;
   }
}
