using HankCollections.DataManagers;
using HankCollections.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace HankCollections.Pages;

public partial class MainPage : ContentPage
{
    public ObservableCollection<CollectionModel> Collections { get; set; } = [];

    public MainPage()
	{
		InitializeComponent();
        Debug.WriteLine($"Paths\nCollections: {Constants.CollectionDir}\nItems: {Constants.ItemDir}\nPhotos: {Constants.ImageDir}");

        BindingContext = this;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        List<CollectionModel> collections = CollectionManager.ReadFromTxt();

        MainThread.BeginInvokeOnMainThread(() =>
        {
            Collections.Clear();
            foreach (CollectionModel collection in collections)
            {
                Collections.Add(collection);
            }
        });
    }

    private async void listView_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        await Navigation.PushAsync(new CollectionPage(e.ItemIndex + 1));
    }
}