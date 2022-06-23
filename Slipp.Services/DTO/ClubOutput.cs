using Slipp.Services.Models;
using System.Text.Json.Serialization;

namespace Slipp.Services.DTO;

public class ClubOutput
{
    [JsonPropertyName("id")] public Guid Id { get; set; }
    [JsonPropertyName("name")] public string Name { get; set; }
    [JsonPropertyName("address")] public string Address { get; set; }
    [JsonPropertyName("website")] public string Website { get; set; }
    [JsonPropertyName("longitude")] public double Longitude { get; set; }
    [JsonPropertyName("latitude")] public double Latitude { get; set; }
    [JsonPropertyName("description")] public string Description { get; set; }
    [JsonPropertyName("company")] public Company Company { get; set; }
    [JsonPropertyName("images")] public List<Image> Images { get; set; }


    [JsonPropertyName("tickets")] public List<Ticket>? Tickets { get; set; }
    [JsonPropertyName("auctions")] public List<Auction>? Auctions { get; set; }

    //public static ClubOutput Create(Club club)
    //{
    //    var outputClub = new ClubOutput
    //    {
    //        Name = club.Name,
    //        Address = club.Adress,
    //        Website = club.Website,
    //        Company = club.Company,
    //        Longitude = club.Longitude,
    //        Latitude = club.Latitude,
    //        Description = club.Description,

    //    };
    //    return outputClub;
    //}
}