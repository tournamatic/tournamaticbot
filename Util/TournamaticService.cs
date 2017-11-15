using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace TournamaticBot.Util
{
    public static class TournamaticService
    {
        private const string _BaseUrl = "https://tournamatic.com/api";
        private const string _AgentKey = "User-Agent";
        private const string _AgentValue = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36";

        public static async Task<IDictionary<string, int>> GetSports()
        {
            var result = new Dictionary<string, int>();
            var url = $"{_BaseUrl}/categories";
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add(_AgentKey, _AgentValue);

                var response = httpClient.GetStringAsync(new Uri(url)).Result;

                var sports = JArray.Parse(response);
                foreach (var sport in sports)
                {
                    var category = sport.ToObject<Sport>();
                    result[category.Title] = category.CategoryId;
                }
            }
            return result;
        }


        public static async Task<IList<Tournament>> GetTournamentsBySport(int selectedSport)
        {
            var result = new List<Tournament>();
            var url = $"{_BaseUrl}/categories/{selectedSport}/tournaments";
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add(_AgentKey, _AgentValue);

                var response = httpClient.GetStringAsync(new Uri(url)).Result;

                var tournaments = JArray.Parse(response);
                foreach (var tournament in tournaments)
                {
                    var tourn = tournament.ToObject<Tournament>();
                    result.Add(tourn);
                }
            }
            return result;
        }

        public static async Task<IList<Tournament>> GetTournamentsByTitle(string title)
        {
            var result = new List<Tournament>();
            var url = $"{_BaseUrl}/tournaments/lookup/{title}";
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add(_AgentKey, _AgentValue);

                var response = httpClient.GetStringAsync(new Uri(url)).Result;

                var tournaments = JArray.Parse(response);
                foreach (var tournament in tournaments)
                {
                    var tourn = tournament.ToObject<Tournament>();
                    result.Add(tourn);
                }
            }
            return result;
        }



    }
}