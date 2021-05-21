using System.Collections.Generic;
using System.Diagnostics;
using Byndyusoft.Metrics.ActivitySources;
using FluentAssertions;

namespace Byndyusoft.Metrics.Listeners
{
    public class TestActivitiesListener : ActivityListenerBase
    {
        private readonly Dictionary<string, int> _operationStartCounters = new();
        private readonly Dictionary<string, int> _operationStopCounters = new();

        protected override void ActivityStarted(Activity activity)
        {
            var operationName = nameof(TestActivitySource.TestOperationWithActivity);

            if (activity.OperationName == operationName)
            {
                if (_operationStartCounters.ContainsKey(operationName) == false)
                {
                    _operationStartCounters[operationName] = 0;
                    _operationStopCounters[operationName] = 0;
                }

                _operationStartCounters[operationName]++;
            }
        }

        protected override void ActivityStopped(Activity activity)
        {
            if (activity.OperationName == nameof(TestActivitySource.TestOperationWithActivity))
                _operationStopCounters[nameof(TestActivitySource.TestOperationWithActivity)]++;
        }

        protected override bool ShouldListenTo(ActivitySource activitySource)
        {
            return activitySource.Name == typeof(TestActivitySource).FullName;
        }

        public void VerifyOperationStart(string operationName, int counts)
        {
            VerifyOperation(operationName, counts, true);
        }

        public void VerifyOperationStop(string operationName, int counts)
        {
            VerifyOperation(operationName, counts, false);
        }

        private void VerifyOperation(string operationName, int counts, bool checkStart)
        {
            var counters = checkStart ? _operationStartCounters : _operationStopCounters;

            if (counts > 0)
            {
                counters.ContainsKey(operationName).Should().BeTrue("Ожидается наличик счетчика для операции");
                counters[operationName].Should().Be(counts);
            }
            else
            {
                counters.ContainsKey(operationName).Should().BeFalse("Для операции не должно быть счетчика");
            }
        }
    }
}