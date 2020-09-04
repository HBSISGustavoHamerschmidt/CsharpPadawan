using System;
using System.Collections.Generic;
using System.Linq;

public class Robot
{
    private readonly Random _rnd = new Random();

    private static readonly HashSet<string> AlreadyExistingRobotNames = new HashSet<string>();

    public Robot() => Reset();

    public string Name { get; set; }

    public void Reset()
    {
        string robotNameNew = string.Empty;
        do
        {
            
            for (int i = 0; i < 2; i++)
            {
                char randomChar = (char)_rnd.Next('A', 'Z');
                robotNameNew += randomChar;
            }
            robotNameNew += $"{_rnd.Next(999):000}";

        } while (AlreadyExistingRobotNames.Any(q => q == robotNameNew));
        
        AlreadyExistingRobotNames.Add(robotNameNew);
        Name = robotNameNew;
    }
}