using System;
using System.Collections.Generic;
using System.Linq;

public class Robot
{
    private readonly Random _rnd = new Random();

    private static readonly List<string> List = new List<string>();

    public Robot() => Reset();

    public string Name { get; set; }

    public void Reset()
    {
        while (true)
        {
            string robotNameNew = string.Empty;
            for (int i = 0; i < 2; i++)
            {
                char randomChar = (char)_rnd.Next('A', 'Z');
                robotNameNew += randomChar;
            }
            robotNameNew += _rnd.Next(999);

            if (List.Any(q => q == robotNameNew)) continue;

            List.Add(robotNameNew);
            Name = robotNameNew;
            break;
        }

    }
}