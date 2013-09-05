namespace Core.Cache.Settings
{
    public class RemoteCachePolicy
    {
        private string policyName;
        private string subGroup;
        private int absoluteExpirationTimeInSecond;
        private string cacheStoreName;

        public string PolicyName
        {
            get { return policyName; }
            set { policyName = value; }
        }

        public string SubGroup
        {
            get { return subGroup; }
            set { subGroup = value; }
        }

        public int AbsoluteExpirationTimeInSecond
        {
            get { return absoluteExpirationTimeInSecond; }
            set { absoluteExpirationTimeInSecond = value; }
        }

        public string CacheStoreName
        {
            get { return cacheStoreName; }
            set { cacheStoreName = value; }
        }
    }
}
