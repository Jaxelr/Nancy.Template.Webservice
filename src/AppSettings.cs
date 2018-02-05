namespace Api
{
    public class AppSettings
    {
        public Cache Cache { get; set; }
        public ErrorMessages ErrorMessages { get; set; }
    }

    public class Cache
    {
        public bool CacheEnabled { get; set; }
        public int CacheTimespan { get; set; }
    }

    public class ErrorMessages
    {
        public string ModelBindingFailure { get; set; }
    }
}
