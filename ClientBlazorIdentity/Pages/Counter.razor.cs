using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ClientBlazorIdentity.Pages
{
    public partial class Counter
    {
        [Inject]
        private IAccessTokenProvider _accessTokenProvider { get; set; }
        [Inject]
        private NavigationManager _navigationManager { get; set; }

        public string Token { get; set; }
        public string test { get; set; }

        protected override async Task OnInitializedAsync()
        {
            //var tokenResult = await _accessTokenProvider.RequestAccessToken();
            //if (tokenResult.TryGetToken(out var token))
            //{
            //    var httpClient = new HttpClient();
            //    httpClient.BaseAddress = new Uri("https://localhost:44390/");
            //    httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token.Value}");
            //    Token = token.Value;
            //    var response = await httpClient.GetFromJsonAsync<TestResponseModel>("identity/api/items");
            //}
        }
        protected override void OnInitialized()
        {
            Task.Run(async () =>
            {
                var tokenResult = await _accessTokenProvider.RequestAccessToken();
                if (tokenResult.TryGetToken(out var token))
                {
                    var httpClient = new HttpClient();
                    httpClient.BaseAddress = new Uri("https://localhost:44390/");
                    httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token.Value}");

                    Token = token.Value;
                    StateHasChanged();
                    //var response = await httpClient.GetFromJsonAsync<TestResponseModel>("identity/api/items");
                }
            });

        }

    }
}
