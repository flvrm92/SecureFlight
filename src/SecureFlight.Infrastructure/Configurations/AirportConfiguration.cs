using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SecureFlight.Core.Entities;

namespace SecureFlight.Infrastructure.Configurations;

public class AirportConfiguration : IEntityTypeConfiguration<Airport>
{
    public void Configure(EntityTypeBuilder<Airport> builder)
    {
        builder.HasKey(x => x.Code);

        builder.HasMany(x => x.OriginFlights)
            .WithOne(f => f.From)
            .HasForeignKey(f => f.OriginAirport);

        builder.HasMany(x => x.DestinationFlights)
            .WithOne(f => f.To)
            .HasForeignKey(f => f.DestinationAirport);

        builder.HasData(new List<Airport>
        {
            new Airport("AAQ", "Anapa Vityazevo", "Anapa", "Russia"),            
            new Airport("JFK", "John F Kennedy International", "New York", "USA"),
            new Airport("ABD", "Abadan", "Abadan", "Iran"),
            new Airport("ABQ", "Albuquerque International Sunport", "Albuquerque", "USA")
        });
    }
}