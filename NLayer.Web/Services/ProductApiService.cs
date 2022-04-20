namespace NLayer.Web.Services
{
    public class ProductApiService
    {
        private readonly HttpClient _client;

        public ProductApiService(HttpClient client)
        {
            _client = client;
        }
    }
}
