using System;

public class SpaceAge
{

    public double OiPaulo { get; set; }

    public double Years {get;}

    public SpaceAge(double seconds) => Years = seconds / 31557600
    ;
    public double OnEarth() => Years;

    public double OnMercury() => Years / 0.2408467;

    public double OnVenus() => Years / 0.61519726;

    public double OnMars() => Years / 1.8808158;

    public double OnJupiter() => Years / 11.862615;

    public double OnSaturn() => Years / 29.447498;

    public double OnUranus() => Years / 84.016846;

    public double OnNeptune() => Years / 164.79132;
}