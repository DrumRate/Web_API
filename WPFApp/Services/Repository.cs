using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using WPFApp.DBContext;
using WPFApp.Models;

namespace WPFApp.Services
{
    public class Repository : IRepository
    {
        private readonly IHttpClientFactory httpClient;
        public Repository(IHttpClientFactory _client)
        {
            httpClient = _client;
        }
        public async Task<IReadOnlyList<Event>> GetEvents(int j)
        {
            var eventsId = new List<int>();
            for (var i = 1; i <= 20; i++) eventsId.Add(i + 20 * j);
            var client = httpClient.CreateClient("EventClient");
            var response = await client.PostAsJsonAsync(string.Empty, eventsId);
            var result = await response.Content.ReadAsAsync<IReadOnlyList<Event>>();
            return result;

        }

    }
}
