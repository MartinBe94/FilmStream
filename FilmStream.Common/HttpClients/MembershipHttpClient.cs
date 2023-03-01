namespace FilmStream.Common.HttpClients
{
    public class MembershipHttpClient
    {
        public HttpClient Client { get; }

        public MembershipHttpClient(HttpClient client)
        {
            Client = client;
        }

        public void AddBearerToken(string token)
        {
            Client.DefaultRequestHeaders.Remove("Authorization");
            Client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
        }
    }
}
