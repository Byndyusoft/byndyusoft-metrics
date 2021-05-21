using System.Diagnostics;
using FluentAssertions;

namespace Byndyusoft.Metrics.Listeners
{
    public class TestActivityWithTagsListener : ActivityListenerBase
    {
        private readonly ActivityTags _expectedTags;

        public TestActivityWithTagsListener(ActivityTags expectedTags)
        {
            _expectedTags = expectedTags;
        }

        public int ActivityStartedCounter { get; set; }
        public int ActivityStoppedCounter { get; set; }

        protected override void ActivityStarted(Activity activity)
        {
            activity.TagObjects.Should().BeEquivalentTo(_expectedTags.GetCollection());
            ActivityStartedCounter++;
        }

        protected override void ActivityStopped(Activity activity)
        {
            activity.TagObjects.Should().BeEquivalentTo(_expectedTags.GetCollection());
            ActivityStoppedCounter++;
        }

        protected override bool ShouldListenTo(ActivitySource activitySource)
        {
            return true;
        }
    }
}