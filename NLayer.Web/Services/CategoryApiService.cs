namespace NLayer.Web.Services
{
    public class CategoryApiService
    {
        private readonly HttpClient _client;

        public CategoryApiService(HttpClient client)
        {
            _client = client;
        }
    }
}
