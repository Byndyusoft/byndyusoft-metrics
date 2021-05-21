using System.Diagnostics;
using Byndyusoft.Metrics.Listeners;

namespace Byndyusoft.Metrics.Extensions
{
    public static class ActivitySourceExtensions
    {
        /// <summary>
        ///     Creates a new <see cref="Activity" /> object if there is any listener to the Activity events, returns null
        ///     otherwise.
        /// </summary>
        /// <param name="activitySource"></param>
        /// <param name="name">The operation name of the Activity.</param>
        /// <param name="kind">The <see cref="ActivityKind" /></param>
        /// <param name="tags">The optional tags list to initialize the created Activity object with.</param>
        /// <returns>The created <see cref="Activity" /> object or null if there is no any listener.</returns>
        public static Activity? StartActivity(
            this ActivitySource activitySource,
            string name,
            ActivityTags? tags = null,
            ActivityKind kind = ActivityKind.Internal
        )
        {
            return activitySource.StartActivity(name, kind, default(ActivityContext), tags?.GetCollection());
        }
    }
}