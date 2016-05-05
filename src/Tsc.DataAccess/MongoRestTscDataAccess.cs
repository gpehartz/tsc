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
        private const string PersonsUrlPart = @"tsc/persons";
        private const string TeamsUrlPart = @"tsc/teams";
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
            throw new NotImplementedException();
        }

        public IEnumerable<Tournament> GetAllTournaments()
        {
            throw new NotImplementedException();
        }

        public Tournament GetTournament(Guid id)
        {
            throw new NotImplementedException();
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

        private static JsonMediaTypeFormatter GetFormatter()
        {
            var resolver = new PrivateSetterJsonDefaultContractResolver();
            var formatter = new JsonMediaTypeFormatter {SerializerSettings = {ContractResolver = resolver}};
            return formatter;
        }

       
    }
}