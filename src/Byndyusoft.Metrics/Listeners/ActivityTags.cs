namespace Byndyusoft.Metrics.Listeners
{
    using System.Collections.Generic;

    public class ActivityTags
    {
        private readonly List<KeyValuePair<string, object?>> _tags;

        public ActivityTags()
        {
            _tags = new List<KeyValuePair<string, object?>>();
        }

        public ActivityTags AddTag(string key, object? value)
        {
            _tags.Add(new KeyValuePair<string, object?>(key, value));

            return this;
        }

        public IEnumerable<KeyValuePair<string, object?>> GetCollection()
        {
            return _tags;
        }
    }
}