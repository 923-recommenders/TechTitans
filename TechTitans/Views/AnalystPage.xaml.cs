using TechTitans.Models;
using TechTitans.Services;
using System.IO;
using System;
using Microsoft.Maui.Controls;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace TechTitans.Views;

public partial class AnalystPage : ContentPage
{

	private TopGenresController topGenresController;
	private Dictionary<String, int> genreCount = new Dictionary<String, int>();
	private Dictionary<String, int> subgenreCount = new Dictionary<String, int>();


    private readonly HttpClient _httpClient;
    private readonly string _baseUrl = "https://localhost:7025/SongDatabaseModel/TopGenres/top-genres/";
	
	public AnalystPage()
	{
		InitializeComponent();
        //topGenresController = new TopGenresController();
        _httpClient = new HttpClient();
    }

	public void ClearText()
	{
        Genre1Name.Text = "";
        Genre1Minutes.Text = "";
        Genre2Name.Text = "";
        Genre2Minutes.Text = "";
        Genre3Name.Text = "";
        Genre3Minutes.Text = "";
    }

	private void OnShowTop3Clicked(object sender, EventArgs e)
	{
		ClearText();
		int monthInt = 0;
		int yearInt = 0;
		var month = MonthPicker.SelectedItem;
		var year = YearPicker.SelectedItem;
		if (month != null && year != null)
		{
			try
			{
				// make them ints
				monthInt = Convert.ToInt32(month);
				yearInt = Convert.ToInt32(year);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}

            ///topGenresController.getTop3Genres(monthInt, yearInt,Genre1Name,Genre1Minutes,Percentage1,Genre2Name,Genre2Name,Percentage2,Genre3Name,Genre3Minutes,Percentage3);
            ///topGenresController.top3SubGenres(monthInt, yearInt,Subgenre1Name,Subgenre1Minutes,Subgenre1Percentage,Subgenre2Name,Subgenre2Minutes,Subgenre2Percentage,Subgenre3Name,Subgenre3Minutes,Subgenre3Percentage);
            var uriBuilder = new UriBuilder(_baseUrl + "login");
            uriBuilder.Query = $"month={monthInt}&year={yearInt}";
            DisplayAlert("Month", monthInt.ToString(), "OK");
            DisplayAlert("Year", yearInt.ToString(), "OK");
            var response = _httpClient.PostAsync(uriBuilder.Uri, null);
        }

    }

}