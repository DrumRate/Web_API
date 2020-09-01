using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using EFCore.BulkExtensions;
using CRUD.Models;
using CRUD.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using CRUD.Models.FactoryDbContext;
using System.Net.Http;
using Newtonsoft.Json;
using CRUD.DTO;
using AutoMapper;

namespace CRUD.Services
{
    public class EventSyncService : IHostedService
    {
        private UnitOptions _options;
        private IServiceProvider Services { get; }
        private Timer _timer;
        private bool syncFinished = true;
        private readonly ILogger<EventSyncService> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public EventSyncService(IOptionsMonitor<UnitOptions> options, IServiceProvider services, ILogger<EventSyncService> logger, IHttpClientFactory httpClientFactory)
        {
            _options = options.CurrentValue;
            options.OnChange(OnSettingsChange);
            Services = services;
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        private void OnSettingsChange(UnitOptions options)
        {
            _logger.LogDebug("Настройки изменены");
            _options = options;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            var timerInterval = _options.SyncIntervalMinutes;
            _logger.LogInformation("Сервис запущен");
            var units = _options.UnitsToTrackEvents;
            _timer = new Timer(SyncAll, null, TimeSpan.Zero, TimeSpan.FromMinutes(timerInterval));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Сервис остановлен");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

        public async void SyncAll(object state)
        {
            if (!syncFinished)
            {
                _logger.LogError("Предыдущая синхронизация еще не завершена. Можете изменить интервал в настройках");
                return;
            }
            syncFinished = false;
            var stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();
            _logger.LogWarning("Новая итерация началась");
            var unitIds = _options.UnitsToTrackEvents;
            var eventSyncTasks = new List<Task>();

            await SyncActiveEvents();

            foreach (var id in unitIds)
            {
                eventSyncTasks.Add(Task.Run(() => SyncNewUnitEvents(id)));
            }

            Task.WaitAll(eventSyncTasks.ToArray());
            stopwatch.Stop();
            syncFinished = true;
            _logger.LogInformation("Синхронизация завершена за {dd\\:hh\\:mm\\:ss}", stopwatch.Elapsed);

        }

        private async Task SyncActiveEvents()
        {
            using var scope = Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<FactoryDbContext>();
            var takeCount = _options.TakeCount;
            var skipCount = 0;
            context.Database.SetCommandTimeout(300);

            var eventIds = await context.Events.Where(e => e.IsActive)
                .Select(e => e.Id)
                .Take(takeCount)
                .ToArrayAsync();
            var res = await GetEvents(eventIds);

            while (res.Any())
            {
                await context.BulkUpdateAsync<Event>(res);
                skipCount += res.Count();
                eventIds = await context.Events.Where(e => e.IsActive)
                    .Select(e => e.Id)
                    .Skip(skipCount)
                    .Take(takeCount)
                    .ToArrayAsync();
                res = await GetEvents(eventIds);
            }
            _logger.LogDebug("Все активные ивенты синхронизированы");
        }
        private async Task SyncNewUnitEvents(int id)
        {
            try
            {
                using var scope = Services.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<FactoryDbContext>();
                var takeCount = _options.TakeCount;
                var skipCount = await context.Events.MaxAsync(e => (int?)e.Id) ?? 0;


                var eventIds = await GetEventIds(id, takeCount, skipCount);

                while (eventIds.Any())
                {
                    var events = await GetEvents(eventIds);

                    await context.BulkInsertAsync<Event>(events);
                    _logger.LogDebug($"Записано {events.Count()} новых ивентов в БД");

                    _logger.LogDebug($"Пачка ивентов длв установки {id} записана в БД.");

                    skipCount += eventIds.Count();
                    eventIds = await GetEventIds(id, takeCount, skipCount);

                }
                _logger.LogInformation("Все новые ивенты были записаны");
            }
            catch (TaskCanceledException ex)
            {

                Console.WriteLine("Таска отменилась" + ex.Message);
            }
            
        }

        private async Task<IList<int>> GetEventIds(int id, int take, int skip)
        {
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["unitId"] = id.ToString();
            queryString["take"] = take.ToString();
            queryString["skip"] = skip.ToString();

            var client = _httpClientFactory.CreateClient("events");

            var res = await client.GetAsync("events/keys?" + queryString.ToString());
            if (!res.IsSuccessStatusCode)
            {
                _logger.LogError("Не смог получить ID");
                return Array.Empty<int>();
            }
            var eventIds = await res.Content.ReadAsAsync<IList<int>>();
            _logger.LogInformation($"Получил {eventIds.Count()} ID ивентов для установки {id}");
            return eventIds;

        }

        private async Task<IList<Event>> GetEvents(IEnumerable<int> eventIds)
        {
            var client = _httpClientFactory.CreateClient("events");

            var res = await client.PostAsJsonAsync(string.Empty, eventIds);

            if (!res.IsSuccessStatusCode) return Array.Empty<Event>();
            var events = await res.Content.ReadAsAsync<IList<Event>>();
            _logger.LogInformation($"Получил {events.Count()} ивентов");
            return events;
        }
    }
}