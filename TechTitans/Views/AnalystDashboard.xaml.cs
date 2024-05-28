using System.Net.Http.Json;
using TechTitans.Models;
using TechTitans.Services;
//using System.IO;
using Microsoft.Maui.Controls;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
namespace TechTitans.Views;

public class Song
{
    public string Name { get; set; }
    public string Minutes { get; set; }
}

public partial class AnalystDashboard : ContentPage

{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl = "https://localhost:7025/SongDatabaseModel/";

    public List<Song> Songs { get; set; }
    public Song SelectedSong { get; set; }

    public AnalystDashboard()
	{
		InitializeComponent();
        FullDetailsOnSongController fullDetailsOnSongController = new FullDetailsOnSongController();
        FullDetailsOnSong FullDetails = fullDetailsOnSongController.GetFullDetailsOnSong(201);
        FullDetailsOnSong CurrentMonth = fullDetailsOnSongController.GetCurrentMonthDetails(201);
        this.BindingContext = CurrentMonth;
        _httpClient = new HttpClient();
        GetAnalyst();

    }

    private async void GetAnalyst()
    {
            // Make GET request to the API
            var response = await _httpClient.GetFromJsonAsync<List<Song>>(_baseUrl);

            if (response != null)
            {
                Songs = response;
                //await DisplayAlert("New User Information", Newtonsoft.Json.JsonConvert.SerializeObject(response), "OK");
                OnPropertyChanged(nameof(Songs));
            }
            else
            {
                // Handle error response
                await DisplayAlert("Error", "Failed to fetch songs", "OK");
            }
    }
}