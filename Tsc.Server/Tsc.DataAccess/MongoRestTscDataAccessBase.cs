using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using Microsoft.Extensions.Options;
using Tsc.Domain;

namespace Tsc.DataAccess
{
    public class MongoRestTscDataAccessBase : IDisposable
    {
        private readonly HttpClient _httpClient;

        protected MongoRestTscDataAccessBase(IOptions<MongoRestTscDataAccessConfiguration> options)
            : this(options.Value)
        {
        }

        protected MongoRestTscDataAccessBase(MongoRestTscDataAccessConfiguration configuration)
        {
            _httpClient = new HttpClient { BaseAddress = new Uri(configuration.MongoDbRestUrl) };
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private static JsonMediaTypeFormatter GetFormatter()
        {
            var resolver = new PrivateSetterJsonDefaultContractResolver();
            var formatter = new JsonMediaTypeFormatter { SerializerSettings = { ContractResolver = resolver } };
            return formatter;
        }

        protected IdMap GetIdMap(Guid id, string urlPart)
        {
            var query = "?query=" + WebUtility.UrlEncode("{\"Id\":\"" + id + "\"}");
            var queryresult = _httpClient.GetAsync(_httpClient.BaseAddress + urlPart + query);
            if (!queryresult.Result.IsSuccessStatusCode)
            {
                throw new Exception(queryresult.Result.StatusCode.ToString());
            }

            var idMaps = queryresult.Result.Content.ReadAsAsync<List<IdMap>>(new[] { GetFormatter() });
            return idMaps.Result.FirstOrDefault();
        }

        protected IEnumerable<T> GetItems<T>(string urlPart)
        {
            var result = _httpClient.GetAsync(_httpClient.BaseAddress + urlPart);
            if (!result.Result.IsSuccessStatusCode)
            {
                throw new Exception(result.Result.StatusCode.ToString());
            }

            var items = result.Result.Content.ReadAsAsync<List<T>>(new[] { GetFormatter() });
            return items.Result;
        }

        protected T GetItem<T>(Guid id, string urlPart)
        {
            var query = "?query=" + WebUtility.UrlEncode("{\"Id\":\"" + id + "\"}");
            var queryresult = _httpClient.GetAsync(_httpClient.BaseAddress + urlPart + query);
            if (!queryresult.Result.IsSuccessStatusCode)
            {
                throw new Exception(queryresult.Result.StatusCode.ToString());
            }

            var items = queryresult.Result.Content.ReadAsAsync<List<T>>(new[] { GetFormatter() });
            return items.Result.FirstOrDefault();
        }
        
        protected void InsertItem<T>(T item, string urlPart)
        {
            var result = _httpClient.PostAsJsonAsync(_httpClient.BaseAddress + urlPart, item);
            if (!result.Result.IsSuccessStatusCode)
            {
                throw new Exception(result.Result.StatusCode.ToString());
            }
        }

        protected void UpdateItem<T>(string technicalId, T item, string urlPart)
        {
            var result = _httpClient.PutAsJsonAsync(_httpClient.BaseAddress + urlPart + "/" + technicalId, item);
            if (!result.Result.IsSuccessStatusCode)
            {
                throw new Exception(result.Result.StatusCode.ToString());
            }
        }

        public void SaveItem<T>(T item, string urlPart) where T : IIdentifiable
        {
            var existingId = GetIdMap(item.Id, urlPart);
            if (existingId == null)
            {
                InsertItem(item, urlPart);
            }
            else
            {
                UpdateItem(existingId.TechnicalId, item, urlPart);
            }
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}