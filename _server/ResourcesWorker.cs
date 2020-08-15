using AOTA_Server.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AOTA_Server
{
    public class ResourcesWorker : IHostedService
    {
        private Timer _timer;
        private readonly IServiceScopeFactory scopeFactory;

        Models.BuildingContext _buildingContext;
        public ResourcesWorker(IServiceScopeFactory scopeFactory)
        {
            this.scopeFactory = scopeFactory;
            _buildingContext= scopeFactory.CreateScope().ServiceProvider.GetRequiredService<BuildingContext>();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                    TimeSpan.FromSeconds(900));
            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
          
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }
    }
}
