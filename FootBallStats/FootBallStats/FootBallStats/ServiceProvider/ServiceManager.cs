
using FootBallStats.Data.Competitions;
using FootBallStats.Data.CompetitionTeams;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FootBallStats.ServiceProvider
{
    public class ServiceManager
    {
        string Uri = "https://api.football-data.org";

        public async Task<HttpClient> GetClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            return client;
        }

        public async Task<IList<RootObject>> Competition()
        {
            var client = await GetClient();
            var input = $"{Uri}/v1/competitions";
            var result = await client.GetStringAsync(input);
            return JsonConvert.DeserializeObject<IList<RootObject>>((result));
        }

        public async Task<CompetitionTeams> CompetitionTeams(int id)
        {
            var client = await GetClient();
            var input = $"{Uri}/v1/competitions/{id}/teams";
            var result = await client.GetStringAsync(input);
            return JsonConvert.DeserializeObject<CompetitionTeams>((result));
        }

        public async Task<FootBallStats.Data.CompetitionLeagueTable.LeagueTable> CompetitionLeagueTable(int id)
        {
            var client = await GetClient();
            var input = $"{Uri}/v1/competitions/{id}/leagueTable";
            var result = await client.GetStringAsync(input);
            return JsonConvert.DeserializeObject<FootBallStats.Data.CompetitionLeagueTable.LeagueTable>((result));
        }
    }
}
