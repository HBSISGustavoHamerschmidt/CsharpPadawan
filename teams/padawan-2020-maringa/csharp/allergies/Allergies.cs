using System;
using System.Collections.Generic;

public class Allergies
{
    public Allergies(int mask) => Mask = mask;
    private int Mask { get; }

    public char[] Binary
    {
        get
        {
            var oi = Convert.ToString(Mask, 2).ToCharArray();
            Array.Reverse(oi);
            return oi;
        }
    }

    public bool IsAllergicTo(Allergen allergen) => List().Contains(allergen);

    public List<Allergen> List()
    {
        List<Allergen> allergens = new List<Allergen>();
        for (var i = 0; i < Binary.Length; i++)
        {
            if (Binary[i] != '0' && (Allergen)(i + 1) < (Allergen) 9) 
            {
                allergens.Add((Allergen)(i + 1));
            }
        }
        return allergens;
    }
}
