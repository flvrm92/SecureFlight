using System.Collections.Generic;

namespace SecureFlight.Core.Entities;

public class Airport
{
  public Airport(string code, string name, string city, string country)
  {
    Code = code;
    Name = name;
    City = city;
    Country = country;        
  }

  public void Update(string name, string city, string country)
  {
    Name = name;
    City = city;
    Country = country;
  }

  public string Code { get; private set; }

  public string Name { get; private set; }

  public string City { get; private set; }

  public string Country { get; private set; }

  public ICollection<Flight> OriginFlights { get; private set; } = new HashSet<Flight>();

  public ICollection<Flight> DestinationFlights { get; private set; } = new HashSet<Flight>();
}