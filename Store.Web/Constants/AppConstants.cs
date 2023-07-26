namespace Store.Web.Constants
{
    public static class AppConstants
    {
        public static readonly TimeSpan _expireTime;

        static AppConstants()
        {
            _expireTime = new TimeSpan(5,0,0,0);
        }
    }
}
