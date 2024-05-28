namespace TechTitans.Views;
using TechTitans.Views.Components.User;
using TechTitans.Views.Components;
using TechTitans.Models;
using TechTitans.Repositories;
using TechTitans.Services;
using Microsoft.Maui.Controls;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;


public partial class UserPage : ContentPage
    {
        UserService userService=new UserService();

        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://localhost:7025/SongDatabaseModel/";
    public class Song
    {
        public int SongId { get; set; }
        public string ArtistName { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public string Subgenre { get; set; }
        public string Language { get; set; }
        public string Country { get; set; }
        public bool IsExplicit { get; set; }
    }
    public List<Song> Songs { get; set; }

    public Song SelectedSong { get; set; }

    public UserPage()
        {

        InitializeComponent();
        BindingContext = this;
        _httpClient = new HttpClient();
        LoadSongs();
    }
    private async void LoadSongs()
    {
        try
        {
            // Make GET request to the API
            var response = await _httpClient.GetFromJsonAsync<List<Song>>(_baseUrl);

            if (response != null)
            {
                Songs = response;
                await DisplayAlert("New User Information", Newtonsoft.Json.JsonConvert.SerializeObject(response), "OK");
                OnPropertyChanged(nameof(Songs));
            }
            else
            {
                // Handle error response
                await DisplayAlert("Error", "Failed to fetch songs", "OK");
            }
        }
        catch (Exception ex)
        {
            // Handle exception
            await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
        }
    }

    private void songListView_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        if (e.Item is Song selectedSong)
        {
            SelectedSong = selectedSong;
            songDetailsLayout.IsVisible = true;
        }
    }

}

