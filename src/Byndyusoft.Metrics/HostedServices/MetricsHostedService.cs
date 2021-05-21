using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Byndyusoft.Metrics.Listeners;
using Microsoft.Extensions.Hosting;

namespace Byndyusoft.Metrics.HostedServices
{
    public class MetricsHostedService : IHostedService
    {
        private readonly IEnumerable<ActivityListenerBase> _metricsListeners;

        public MetricsHostedService(IEnumerable<ActivityListenerBase> metricsListeners)
        {
            _metricsListeners = metricsListeners;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            foreach (var listener in _metricsListeners)
                listener.Start();

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            foreach (var listener in _metricsListeners)
                listener.Dispose();

            return Task.CompletedTask;
        }
    }
}