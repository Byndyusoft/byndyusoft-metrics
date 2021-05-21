using Byndyusoft.Metrics.HostedServices;
using Byndyusoft.Metrics.Listeners;
using Microsoft.Extensions.DependencyInjection;

namespace Byndyusoft.Metrics.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        ///     Регистрация фоновой службы для сбора метрик
        /// </summary>
        /// <returns></returns>
        public static IServiceCollection AddApplicationMetrics(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddHostedService<MetricsHostedService>();
        }

        /// <summary>
        ///     Добавление слушателя метрик, слушатетель начнет получать события при старте хоста
        /// </summary>
        /// <typeparam name="TListener">тип слушателя</typeparam>
        /// <returns></returns>
        public static IServiceCollection AddMetricListener<TListener>(this IServiceCollection services)
            where TListener : ActivityListenerBase
        {
            return services.AddSingleton<ActivityListenerBase, TListener>();
        }
    }
}