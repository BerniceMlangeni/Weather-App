using System.Security.Cryptography.X509Certificates;
using Weather_App.Services;

namespace Weather_App;

public partial class WeatherPage : ContentPage
{
    public List<Models.List> WeatherList;
    private double Latitude;
    private double Longitude;
	public WeatherPage()
	{
		InitializeComponent();
        WeatherList = new List<Models.List>();
	}

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await GetLocation();
        await GetWeatherByLocation(Latitude, Longitude);

    }

    public async Task GetLocation()
    {
        var location =  await Geolocation.GetLocationAsync();
        Latitude = location.Latitude;
        Longitude = location.Longitude;
    }

    private async void Taplocation_Tapped(object sender, EventArgs e)
    {
        await GetLocation();
        await GetWeatherByLocation(Latitude, Longitude);
    }

    public async Task GetWeatherByLocation(double latitude, double longitude)
    {
        var result = await ApiService.GetWeather(Latitude, Longitude);
        UpdatedUI(result);


    }
    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
       var response = await DisplayPromptAsync(title: "",message:"",placeholder:"Search weather by city", accept:"Search",cancel:"Cancel");
        if (response != null)
        {
           await GetWeatherByCity(response);
        }
    }

    public async Task GetWeatherByCity(string city)
    {
        var result = await ApiService.GetWeatherByCity(city);
        UpdatedUI(result);

    }

    public void UpdatedUI(dynamic result)
    {
        foreach (var item in result.list)
        {
            WeatherList.Add(item);
        }
        CvWeather.ItemsSource = WeatherList;


        LblCity.Text = result.city.name;
        LblWeatherDescription.Text = result.list[0].weather[0].description;
        LblTemperature.Text = result.list[0].main.temperature + "�C";
        LblHumidity.Text = result.list[0].main.humidity + "%";
        LblWind.Text = result.list[0].wind.speed + "km/h";
        ImgWeatherIcon.Source = result.list[0].weather[0].CustomIcon;
    }
}