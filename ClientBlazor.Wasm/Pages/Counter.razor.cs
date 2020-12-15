using ClientBlazor.Wasm.Model;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ClientBlazor.Wasm.Pages
{
    public partial class Counter
    {
        [Inject]
        private HttpClient _httpClient { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var test = await _httpClient.GetFromJsonAsync<TestResponseModel>("identity/api/itemsauth");
        }
    }
}
