using Byndyusoft.Metrics.ActivitySources;
using Byndyusoft.Metrics.Listeners;
using FluentAssertions;
using Xunit;

namespace Byndyusoft.Metrics
{
    public class ListenerTests
    {
        private readonly TestActivitiesListener _listener;
        private readonly TestActivitySource _testActivitySource;

        public ListenerTests()
        {
            _listener = new TestActivitiesListener();
            _testActivitySource = new TestActivitySource();
        }

        [Fact]
        public void TestOperationWithActivity_ListenerStarted_ShouldReceiveOperationStart()
        {
            //Act
            _listener.Start();
            _testActivitySource.TestOperationWithActivity();

            //Assert
            _listener.VerifyOperationStart(nameof(TestActivitySource.TestOperationWithActivity), 1);
        }

        [Fact]
        public void TestOperationWithActivity_ListenerStarted_ShouldReceiveOperationStop()
        {
            //Act
            _listener.Start();
            _testActivitySource.TestOperationWithActivity();

            //Assert
            _listener.VerifyOperationStop(nameof(TestActivitySource.TestOperationWithActivity), 1);
        }

        [Fact]
        public void TestOperationWithoutActivity_ListenerStarted_NothingHappenOnOperationStart()
        {
            //Act
            _listener.Start();
            _testActivitySource.TestOperationWithoutActivity();

            //Assert
            _listener.VerifyOperationStart(nameof(TestActivitySource.TestOperationWithoutActivity), 0);
        }

        [Fact]
        public void TestOperationWithoutActivity_ListenerStarted_NothingHappenOnOperationStop()
        {
            //Act
            _listener.Start();
            _testActivitySource.TestOperationWithoutActivity();

            //Assert
            _listener.VerifyOperationStop(nameof(TestActivitySource.TestOperationWithoutActivity), 0);
        }

        [Fact]
        public void TestOperationWithActivity_ListenerNotStarted_NoException()
        {
            //Act
            _testActivitySource.TestOperationWithActivity();
        }

        [Fact]
        public void TestOperationWithActivityWithTag_AnyTagsPassedToActivity_ListenerGetTagsOnStartAndStop()
        {
            //Arrange
            var activityTags = new ActivityTags().AddTag("TestTag", 123)
                .AddTag("TestType", "SomeType");

            var listener = new TestActivityWithTagsListener(activityTags);

            //Act
            listener.Start();
            _testActivitySource.TestOperationWithActivityWithTag(activityTags);

            //Assert
            listener.ActivityStartedCounter.Should().Be(1);
            listener.ActivityStoppedCounter.Should().Be(1);
        }
    }
}