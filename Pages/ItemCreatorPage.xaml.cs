using HankCollections.DataManagers;
using HankCollections.Models;

namespace HankCollections.Pages;

[QueryProperty("NewItem", "NewItem")]
public partial class ItemCreatorPage : ContentPage
{
    bool isEditing = false;

    public ItemModel NewItem
    {
        get => BindingContext as ItemModel;
        set => BindingContext = value;
    }

    public ItemCreatorPage()
	{
		InitializeComponent();

        statusPicker.ItemsSource = Enum.GetNames(typeof(Status));
        satisfactionPicker.ItemsSource = Enum.GetNames(typeof(Satisfaction));
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        statusPicker.SelectedIndex = (int)NewItem.Status;
        satisfactionPicker.SelectedIndex = (int)NewItem.Satisfaction;
        soldSwitch.IsToggled = NewItem.IsSold;

        if (NewItem.Name is not null)
        {
            isEditing = true;
        }

    }

    private async void AddImage_Clicked(object sender, EventArgs e)
    {
        var filePicker = FilePicker.Default;
        var options = new PickOptions
        {
            PickerTitle = "Select an image"
        };

        var file = await filePicker.PickAsync(options);

        int Id = ImageManager.AddImage(file.FullPath);
        NewItem.ImageId = Id;
    }

    private async void Save_Clicked(object sender, EventArgs e)
    {
        int status = statusPicker.SelectedIndex;
        int satisfaction = satisfactionPicker.SelectedIndex;
        bool isSold = soldSwitch.IsToggled;

        NewItem.Status = (Status)status;
        NewItem.Satisfaction = (Satisfaction)satisfaction;
        NewItem.IsSold = isSold;

        bool duplicate = ItemManager.IsDuplicate(NewItem.Name);
        if (isEditing == true || duplicate == false)
        {
            ItemManager.AddItem(NewItem);
            await Shell.Current.GoToAsync("..");
            return;
        }

        bool answer = await DisplayAlert("Duplicate", "Do you want to add it again?", "Yes", "No");

        if (answer == false)
        {
            return;
        }
        ItemManager.AddItem(NewItem);

        await Shell.Current.GoToAsync("..");
    }

    private async void Cancel_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }

    private async void Remove_Clicked(object sender, EventArgs e)
    {
        if (NewItem.Id > 0)
        {
            ItemManager.RemoveItem(NewItem.Id);
            await Shell.Current.GoToAsync("..");
        }
        await DisplayAlert("Warning", "You can't delete an item that doesn't exist!", "OK");
    }
}