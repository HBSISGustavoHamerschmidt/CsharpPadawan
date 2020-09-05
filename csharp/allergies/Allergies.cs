using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualBasic;

public enum Allergen
{
    Eggs = 1,
    Peanuts = 2,
    Shellfish = 3,
    Strawberries = 4,
    Tomatoes = 5,
    Chocolate = 6,
    Pollen = 7,
    Cats = 8
}

public class Allergies
{

    private int Mask { get; }


    public Allergies(int mask) => Mask = mask;

    public bool IsAllergicTo(Allergen allergen) => List().Contains(allergen);

    public List<Allergen> List()
    {

        List<Allergen> allergens = new List<Allergen>();
        foreach (var item in Enum.GetValues(typeof(Allergen)))
        {
            if ((Mask & (int)item) > 0)
            {
                allergens.Add((Allergen)item);
            }
        }
        return allergens.ToList();
    }
}