using System;
using System.Diagnostics;
using Byndyusoft.Metrics.Listeners;

namespace Byndyusoft.Metrics.ActivitySources
{
    public class TestActivitySource
    {
        private static readonly ActivitySource ActivitySource = new(typeof(TestActivitySource).FullName!);

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