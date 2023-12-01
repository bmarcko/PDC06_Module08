using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using System.Net.Http;
using static PDC06_Module08.SearchPage;

using System.Collections.ObjectModel;

namespace PDC06_Module08
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private const string url_login = "http://172.16.24.118/pdc6/api-login.php";
        private readonly HttpClient _client;
        public LoginPage()
        {
            InitializeComponent();
            _client = new HttpClient();
        }

        private async void OnLogin(object sender, EventArgs e)
        {
            string username = xUsername.Text;
            string password = xPassword.Text;

            if(string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                await DisplayAlert("Error", "Username and password are required", "OK");
                return;
            }
            try
            {
                var loginUrl = $"{url_login}?username={username}?password={password}";
                var content = await _client.GetStringAsync(loginUrl);

                var responseObject = JsonConvert.DeserializeObject<ResponseObject>(content);
                if (responseObject.status)
                {
                    await DisplayAlert("Success", "Login Succesful", "OK");
                    await Navigation.PushAsync(new MainPage());
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"An Error occured {ex.Message}", "OK");
            }
        }
    }
}