using System;
using System.Diagnostics;
using Byndyusoft.Metrics.Extensions;
using Byndyusoft.Metrics.Listeners;

namespace Byndyusoft.Metrics.ActivitySources
{
    public class TestActivitySource
    {
        private static readonly ActivitySource ActivitySource = new ActivitySource(typeof(TestActivitySource).FullName ?? string.Empty);

        public void TestOperationWithActivity()
        {
            using (ActivitySource.StartActivity(nameof(TestOperationWithActivity)))
                Console.WriteLine(nameof(TestOperationWithActivity));
        }

        public void TestOperationWithActivityWithTag(ActivityTags tags)
        {
            using (ActivitySource.StartActivity(nameof(TestOperationWithActivityWithTag), tags))
                Console.WriteLine(nameof(TestOperationWithActivityWithTag));
        }

        public void TestOperationWithoutActivity()
        {
            Console.WriteLine(nameof(TestOperationWithoutActivity));
        }
    }
}