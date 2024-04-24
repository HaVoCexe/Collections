using HankCollections.DataManagers;

namespace HankCollections.Pages;

public partial class SettingsPage : ContentPage
{
	public SettingsPage()
	{
		InitializeComponent();
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
        string result = await DisplayPromptAsync("Name", "What's the collection name?");
        CollectionManager.CreateCollection(result);
    }
}