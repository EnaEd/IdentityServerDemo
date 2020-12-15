using ClientBlazor.Wasm.Model;
using System.Threading.Tasks;

namespace ClientBlazor.Wasm.Pages.Auth
{
    public partial class SignIn
    {
        private SignInReuestModel _signinModel;


        protected override void OnInitialized()
        {
            _signinModel = new SignInReuestModel();
            base.OnInitialized();
        }

        private async Task OnSignInAsync()
        {

        }

    }
}
