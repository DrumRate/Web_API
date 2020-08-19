using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using NLog;

using CRUD.Repository;

namespace CRUD.Services
{
    public class TankService : IHostedService
    {
        private readonly ILogger logger = LogManager.GetCurrentClassLogger();
        private readonly IServiceProvider _services;
        private readonly Random _random = new Random();
        private Timer timer;

        public TankService(IServiceProvider services)
        {
            this._services = services;
        }
        private async void UpdateVolume(object state)
        {
            using (var scope = _services.CreateScope())
            {
                var repository = scope.ServiceProvider.GetRequiredService<IFactoryRepository<Tank>>();
                var TankList = await repository.GetAll();
                foreach (var tank in TankList)
                {
                    var deviation = tank.Volume * 0.1;
                    var low = -deviation;
                    var high = deviation;
                    var delta = _random.NextDouble() * (high - low) + low;
                    if (tank.Volume + delta > tank.MaxVolume)
                    {
                        logger.Info("Объем превышен!");
                        tank.Volume = (float)(tank.Volume - delta);
                    }
                    else
                    {
                        tank.Volume = (tank.Volume + (int)delta) >= 0 ? tank.Volume + (int)delta : 0;
                        logger.Info($"Объем резервуара {tank.Name}. Текущий объем - {tank.Volume}");
                        await repository.Update(tank.Id, tank);
                    }
                }
            }
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            timer = new Timer(UpdateVolume, null, TimeSpan.Zero, TimeSpan.FromSeconds(1));
            return Task.CompletedTask;
        }
        public Task StopAsync(CancellationToken cancellationToken)
        {
            timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }
    }
}
