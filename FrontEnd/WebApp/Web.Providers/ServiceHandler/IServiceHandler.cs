namespace Web.Providers
{
    public interface IServiceHandler
    {
        Task<T> Get<T>(string url);
        Task<T> Post<T>(string url, string payload);
    }
}

