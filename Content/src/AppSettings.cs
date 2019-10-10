namespace Nancy.Template.WebService.Properties
{
    public class AppSettings
    {
        public Cache Cache { get; set; }
        public ErrorMessages ErrorMessages { get; set; }
        public Metadata Metadata { get; set; }
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

    public class Metadata
    {
        public string Title { get; set; }
        public string Version { get; set; }
        public string DocsPath { get; set; }
        public Host Host { get; set; }
    }

    public class Host
    {
        public string Description { get; set; }
        public string Url { get; set; }
    }
}
