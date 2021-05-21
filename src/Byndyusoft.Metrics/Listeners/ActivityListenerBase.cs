using System;
using System.Diagnostics;

namespace Byndyusoft.Metrics.Listeners
{
    /// <summary>
    ///     Базовый класс для подписывания на события приложения
    /// </summary>
    public abstract class ActivityListenerBase : IDisposable
    {
        private readonly ActivityListener _activityListener;

        protected ActivityListenerBase()
        {
            _activityListener = new ActivityListener
            {
                ShouldListenTo = ShouldListenTo,
                ActivityStarted = ActivityStarted,
                ActivityStopped = ActivityStopped,
                Sample = (ref ActivityCreationOptions<ActivityContext> options) => ActivitySamplingResult.AllData,
                SampleUsingParentId = (ref ActivityCreationOptions<string> options) => ActivitySamplingResult.AllData
            };
        }

        public void Dispose()
        {
            _activityListener.Dispose();
        }

        /// <summary>
        ///     Старт прослушивания метрик. Вызывается автоматически при старте хоста
        /// </summary>
        public void Start()
        {
            ActivitySource.AddActivityListener(_activityListener);
        }

        /// <summary>
        ///     Обработка начала активности приложения
        /// </summary>
        /// <param name="activity"></param>
        protected abstract void ActivityStarted(Activity activity);

        /// <summary>
        ///     Обработка остановки активности приложения
        /// </summary>
        /// <param name="activity"></param>
        protected abstract void ActivityStopped(Activity activity);

        /// <summary>
        ///     Определение, какие источники событий будут прослушываться
        /// </summary>
        /// <param name="activitySource"></param>
        /// <returns></returns>
        protected abstract bool ShouldListenTo(ActivitySource activitySource);
    }
}