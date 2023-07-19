namespace Store.Web.Constants
{
    public class AppConstants
    {
        public readonly TimeSpan _expireTime;

        public AppConstants()
        {
            _expireTime = new TimeSpan(5,0,0,0);
        }
    }
}
