using ClientBlazor.Wasm.Model;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ClientBlazor.Wasm.Pages
{
    public partial class FetchData
    {
        [Inject]
        private HttpClient _httpClient { get; set; }
        public List<TestResponseModel> Tests { get; set; } = new List<TestResponseModel>();

        protected override async Task OnInitializedAsync()
        {
            var test = await _httpClient.GetFromJsonAsync<TestResponseModel>("identity/api/items");
            Tests.Add(test);
        }
    }
}
