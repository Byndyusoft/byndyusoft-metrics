using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Byndyusoft.Metrics.Extensions
{
    public static class ActivityExtensions
    {
        private const string ErrorTag = "error";

        /// <summary>
        ///     Помечает активность ошибочной
        /// </summary>
        /// <param name="activity"></param>
        public static void SetError(this Activity activity)
        {
            activity.AddBaggage(ErrorTag, true.ToString());
        }

        /// <summary>
        ///     Достает флаг, была ли активность помечена ошибочной
        /// </summary>
        /// <param name="activity"></param>
        /// <returns></returns>
        public static bool HasError(this Activity activity)
        {
            var baggageItem = activity.GetBaggageItem(ErrorTag);

            return baggageItem == true.ToString();
        }

        /// <summary>
        ///     Достает значение тега, переданного при создании активности
        /// </summary>
        /// <param name="activity"></param>
        /// <param name="key">ключ тега</param>
        /// <returns></returns>
        public static object? GetTagObject(this Activity activity, string key)
        {
            var tag = activity.TagObjects.SingleOrDefault(x => x.Key == key);

            if (EqualityComparer<KeyValuePair<string, object?>>.Default.Equals(tag, default))
                return null;

            return tag.Value;
        }
    }
}