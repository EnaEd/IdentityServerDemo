using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsoleClientForIdentityServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Task.Run(async () =>
            {

                var client = new HttpClient();
                DiscoveryDocumentResponse disco = await client.GetDiscoveryDocumentAsync("http://localhost:5000");
                //var disco = await DiscoveryClient.GetAsync("http://localhost:5000");
                if (disco.IsError)
                {
                    Console.WriteLine(disco.Error);
                    return;
                }
                var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
                {
                    Address = disco.TokenEndpoint,

                    ClientId = "client",
                    ClientSecret = "secret",
                    Scope = "api1"
                });

                if (tokenResponse.IsError)
                {
                    Console.WriteLine(tokenResponse.Error);
                    return;
                }

                Console.WriteLine(tokenResponse.Json);

                var apiClient = new HttpClient();
                apiClient.SetBearerToken(tokenResponse.AccessToken);

                var response = await apiClient.GetAsync("https://localhost:44390/identity");
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine("-------------------------------------------\n not ok\n---------------------------");
                    Console.WriteLine(response.StatusCode);
                }
                else
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("-------------------------------------------\n Ok\n---------------------------");
                    Console.WriteLine(JArray.Parse(content));
                }



            }).Wait();





        }
    }
}
