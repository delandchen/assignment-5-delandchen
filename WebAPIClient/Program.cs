using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WebAPIClient
{
    class Location
    {
        [JsonProperty("country")]
        public string? Country { get; set; }

        [JsonProperty("places")]
        public List<Place>? Places { get; set; }
    }

    public class Place
    {
        [JsonProperty("longitude")]
        public string? Longitude { get; set; }

        [JsonProperty("latitude")]
        public string? Latitude { get; set; }

        [JsonProperty("place name")]
        public string? Name { get; set; }
    }

    class Program
    {
        private static readonly HttpClient client = new HttpClient();

        static void Main(string[] args)
        {
            ProcessRepositories().Wait();
        }

        private static async Task ProcessRepositories()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Enter US Postal Code. Press Enter: ");

                    var postalCode = Console.ReadLine();

                    if (string.IsNullOrEmpty(postalCode))
                    {
                        break;
                    }

                    var result = await client.GetAsync("http://api.zippopotam.us/us/" + postalCode);
                    var resultRead = await result.Content.ReadAsStringAsync();

                    var location = JsonConvert.DeserializeObject<Location>(resultRead);

                    Console.WriteLine("Postal Country: " + location?.Country);
                    Console.WriteLine("Postal Place Name: " + location?.Places?[0]?.Name);
                    Console.WriteLine("Postal Longitude: " + location?.Places?[0]?.Longitude);
                    Console.WriteLine("Postal Latitude: " + location?.Places?[0]?.Latitude);
                }
                catch (Exception)
                {
                    Console.WriteLine("ERROR. Please enter a valid name");
                }

            }
        }
    }
}

