using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using Microsoft.Extensions.OptionsModel;
using Tsc.Domain;

namespace Tsc.DataAccess
{
    public class MongoRestTscDataAccess : ITscDataAccess, IDisposable
    {
        private const string TeamsUrlPart = @"tsc/teams";
        private const string TournamentsUrlPart = @"tsc/tournaments";
        private readonly HttpClient _httpClient;

        public MongoRestTscDataAccess(IOptions<MongoRestTscDataAccessConfiguration> options)
            : this(options.Value)
        {
        }

        internal MongoRestTscDataAccess(MongoRestTscDataAccessConfiguration configuration)
        {
            _httpClient = new HttpClient {BaseAddress = new Uri(configuration.MongoDbRestUrl) };
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private static JsonMediaTypeFormatter GetFormatter()
        {
            var resolver = new PrivateSetterJsonDefaultContractResolver();
            var formatter = new JsonMediaTypeFormatter { SerializerSettings = { ContractResolver = resolver } };
            return formatter;
        }

        public void Save(Team team)
        {
            var existingId = GetIdMapForTeam(team.Id);
            if (existingId == null)
            {
                InsertTeam(team);
            }
            else
            {
                UpdateTeam(existingId.TechnicalId, team);
            }
        }

        private void UpdateTeam(string technicalId, Team team)
        {
            var result = _httpClient.PutAsJsonAsync(_httpClient.BaseAddress + TeamsUrlPart + "/" + technicalId, team);
            if (!result.Result.IsSuccessStatusCode)
            {
                throw new Exception(result.Result.StatusCode.ToString());
            }
        }

        private void InsertTeam(Team team)
        {
            var result = _httpClient.PostAsJsonAsync(_httpClient.BaseAddress + TeamsUrlPart, team);
            if (!result.Result.IsSuccessStatusCode)
            {
                throw new Exception(result.Result.StatusCode.ToString());
            }
        }

        public Team GetTeamById(Guid id)
        {
            var query = "?query=" + WebUtility.UrlEncode("{\"Id\":\"" + id + "\"}");
            var queryresult = _httpClient.GetAsync(_httpClient.BaseAddress + TeamsUrlPart + query);
            if (!queryresult.Result.IsSuccessStatusCode)
            {
                throw new Exception(queryresult.Result.StatusCode.ToString());
            }

            var teams = queryresult.Result.Content.ReadAsAsync<List<Team>>(new[] { GetFormatter() });
            return teams.Result.FirstOrDefault();
        }

        private IdMap GetIdMapForTeam(Guid id)
        {
            var query = "?query=" + WebUtility.UrlEncode("{\"Id\":\"" + id + "\"}");
            var queryresult = _httpClient.GetAsync(_httpClient.BaseAddress + TeamsUrlPart + query);
            if (!queryresult.Result.IsSuccessStatusCode)
            {
                throw new Exception(queryresult.Result.StatusCode.ToString());
            }

            var idMaps = queryresult.Result.Content.ReadAsAsync<List<IdMap>>(new[] { GetFormatter() });
            return idMaps.Result.FirstOrDefault();
        }

        public IEnumerable<Team> GetAllTeams()
        {
            var result = _httpClient.GetAsync(_httpClient.BaseAddress + TeamsUrlPart);
            if (!result.Result.IsSuccessStatusCode)
            {
                throw new Exception(result.Result.StatusCode.ToString());
            }

            var teams = result.Result.Content.ReadAsAsync<List<Team>>(new[] { GetFormatter() });
            return teams.Result;
        }

        public void Save(Tournament tournament)
        {
            var existingId = GetIdMapForTournament(tournament.Id);
            if (existingId == null)
            {
                InsertTournament(tournament);
            }
            else
            {
                UpdateTournament(existingId.TechnicalId, tournament);
            }
        }

        private void UpdateTournament(string technicalId, Tournament tournament)
        {
            var result = _httpClient.PutAsJsonAsync(_httpClient.BaseAddress + TournamentsUrlPart + "/" + technicalId, tournament);
            if (!result.Result.IsSuccessStatusCode)
            {
                throw new Exception(result.Result.StatusCode.ToString());
            }
        }

        private void InsertTournament(Tournament tournament)
        {
            var result = _httpClient.PostAsJsonAsync(_httpClient.BaseAddress + TournamentsUrlPart, tournament);
            if (!result.Result.IsSuccessStatusCode)
            {
                throw new Exception(result.Result.StatusCode.ToString());
            }
        }

        public IEnumerable<Tournament> GetAllTournaments()
        {
            var result = _httpClient.GetAsync(_httpClient.BaseAddress + TournamentsUrlPart);
            if (!result.Result.IsSuccessStatusCode)
            {
                throw new Exception(result.Result.StatusCode.ToString());
            }

            var tournaments = result.Result.Content.ReadAsAsync<List<Tournament>>(new[] { GetFormatter() });
            return tournaments.Result;
        }

        public Tournament GetTournament(Guid id)
        {
            var query = "?query=" + WebUtility.UrlEncode("{\"Id\":\"" + id + "\"}");
            var queryresult = _httpClient.GetAsync(_httpClient.BaseAddress + TournamentsUrlPart + query);
            if (!queryresult.Result.IsSuccessStatusCode)
            {
                throw new Exception(queryresult.Result.StatusCode.ToString());
            }

            var tournamnets = queryresult.Result.Content.ReadAsAsync<List<Tournament>>(new[] { GetFormatter() });
            return tournamnets.Result.FirstOrDefault();
        }

        private IdMap GetIdMapForTournament(Guid id)
        {
            var query = "?query=" + WebUtility.UrlEncode("{\"Id\":\"" + id + "\"}");
            var queryresult = _httpClient.GetAsync(_httpClient.BaseAddress + TournamentsUrlPart + query);
            if (!queryresult.Result.IsSuccessStatusCode)
            {
                throw new Exception(queryresult.Result.StatusCode.ToString());
            }

            var idMaps = queryresult.Result.Content.ReadAsAsync<List<IdMap>>(new[] { GetFormatter() });
            return idMaps.Result.FirstOrDefault();
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}