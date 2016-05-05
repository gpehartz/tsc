using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using Tsc.Domain;

namespace Tsc.DataAccess
{
    public class MongoRestTscDataAccess : ITscDataAccess
    {
        //private const string PersonsUrlPart = @"tsc/persons";
        private const string TeamsUrlPart = @"tsc/teams";
        private const string TournamentsUrlPart = @"tsc/tournaments";
        private readonly string _url;

        public MongoRestTscDataAccess(string url)
        {
            _url = url;
        }

        private HttpClient GetHttpClient()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(_url);

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }

        private static JsonMediaTypeFormatter GetFormatter()
        {
            var resolver = new PrivateSetterJsonDefaultContractResolver();
            var formatter = new JsonMediaTypeFormatter { SerializerSettings = { ContractResolver = resolver } };
            return formatter;
        }

        public void Save(Team team)
        {
            var storedTeam = GetTeamById(team.Id);
            if (storedTeam == null)
            {
                InsertTeam(team);
            }
            else
            {
                UpdateTeam(storedTeam.TechnicalId, team);
            }
        }

        private void UpdateTeam(string technicalId, Team team)
        {
            var client = GetHttpClient();

            var result = client.PutAsJsonAsync(client.BaseAddress + TeamsUrlPart + "/"+technicalId, team);
            if (!result.Result.IsSuccessStatusCode)
            {
                throw new Exception(result.Result.StatusCode.ToString());
            }
        }

        private void InsertTeam(Team team)
        {
            var client = GetHttpClient();

            var result = client.PostAsJsonAsync(client.BaseAddress + TeamsUrlPart, team);
            if (!result.Result.IsSuccessStatusCode)
            {
                throw new Exception(result.Result.StatusCode.ToString());
            }
        }

        public Team GetTeamById(Guid id)
        {
            var client = GetHttpClient();
            
            var query = "?query=" + WebUtility.UrlEncode("{\"Id\":\"" + id + "\"}");
            var queryresult = client.GetAsync(client.BaseAddress + TeamsUrlPart + query);
            if (!queryresult.Result.IsSuccessStatusCode)
            {
                throw new Exception(queryresult.Result.StatusCode.ToString());
            }

            var teams = queryresult.Result.Content.ReadAsAsync<List<Team>>(new[] { GetFormatter() });

            return teams.Result.FirstOrDefault();
        }

        public IEnumerable<Team> GetAllTeams()
        {
            var client = GetHttpClient();
            var result = client.GetAsync(client.BaseAddress + TeamsUrlPart);
            if (!result.Result.IsSuccessStatusCode)
            {
                throw new Exception(result.Result.StatusCode.ToString());
            }

            var teams = result.Result.Content.ReadAsAsync<List<Team>>(new[] { GetFormatter() });

            return teams.Result;
        }

        public void Save(Tournament tournament)
        {
            var storedTournamnet = GetTournament(tournament.Id);
            if (storedTournamnet == null)
            {
                InsertTournament(tournament);
            }
            else
            {
                UpdateTournament(tournament.TechnicalId, tournament);
            }
        }

        private void UpdateTournament(string technicalId, Tournament tournament)
        {
            var client = GetHttpClient();

            var result = client.PutAsJsonAsync(client.BaseAddress + TournamentsUrlPart + "/" + technicalId, tournament);
            if (!result.Result.IsSuccessStatusCode)
            {
                throw new Exception(result.Result.StatusCode.ToString());
            }
        }

        private void InsertTournament(Tournament tournament)
        {
            var client = GetHttpClient();

            var result = client.PostAsJsonAsync(client.BaseAddress + TournamentsUrlPart, tournament);
            if (!result.Result.IsSuccessStatusCode)
            {
                throw new Exception(result.Result.StatusCode.ToString());
            }
        }

        public IEnumerable<Tournament> GetAllTournaments()
        {
            var client = GetHttpClient();
            var result = client.GetAsync(client.BaseAddress + TournamentsUrlPart);
            if (!result.Result.IsSuccessStatusCode)
            {
                throw new Exception(result.Result.StatusCode.ToString());
            }

            var tournaments = result.Result.Content.ReadAsAsync<List<Tournament>>(new[] { GetFormatter() });

            return tournaments.Result;
        }

        public Tournament GetTournament(Guid id)
        {
            var client = GetHttpClient();

            var query = "?query=" + WebUtility.UrlEncode("{\"Id\":\"" + id + "\"}");
            var queryresult = client.GetAsync(client.BaseAddress + TournamentsUrlPart + query);
            if (!queryresult.Result.IsSuccessStatusCode)
            {
                throw new Exception(queryresult.Result.StatusCode.ToString());
            }

            var tournamnets = queryresult.Result.Content.ReadAsAsync<List<Tournament>>(new[] { GetFormatter() });

            return tournamnets.Result.FirstOrDefault();
        }

        //public void Save(Person person)
        //{
        //    var client = GetHttpClient();
        //    var result = client.PostAsJsonAsync(client.BaseAddress + PersonsUrlPart, person);
        //    if (!result.Result.IsSuccessStatusCode)
        //    {
        //        throw new Exception(result.Result.StatusCode.ToString());
        //    }
        //}

        //public IEnumerable<Person> GetAllPersons()
        //{
        //    var client = GetHttpClient();
        //    var result = client.GetAsync(client.BaseAddress + PersonsUrlPart);
        //    if (!result.Result.IsSuccessStatusCode)
        //    {
        //        throw new Exception(result.Result.StatusCode.ToString());
        //    }

        //    var teams = result.Result.Content.ReadAsAsync<List<Person>>(new[] { GetFormatter() });

        //    return teams.Result;
        //}

        

       
    }
}