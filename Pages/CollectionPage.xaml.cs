using HankCollections.DataManagers;
using HankCollections.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace HankCollections.Pages;

public partial class CollectionPage : ContentPage
{
    public ObservableCollection<ItemModel> Items { get; set; } = [];
    int collectionId;

    public CollectionPage(int collectionId)
	{
		InitializeComponent();
        this.collectionId = collectionId;

        List<ItemModel> items = ItemManager.GetItemsByCollectionId(collectionId);
        items = items.OrderBy(x => x.IsSold).ToList();

        MainThread.BeginInvokeOnMainThread(() =>
        {
            Items.Clear();

            int i = 1;
            foreach (ItemModel item in items)
            {
                item.TempId = i;
                Items.Add(item);
                i++;
            }
        });

        BindingContext = this;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        List<ItemModel> items = ItemManager.GetItemsByCollectionId(collectionId);
        items = items.OrderBy(x => x.IsSold).ToList();

        MainThread.BeginInvokeOnMainThread(() =>
        {
            Items.Clear();

            int i = 1;
            foreach (ItemModel item in items)
            {
                item.TempId = i;
                Items.Add(item);
                i++;
            }
        });
    }

    private async void listView_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        ItemModel item = Items.First(x => x.TempId == e.ItemIndex + 1);

        await Shell.Current.GoToAsync(nameof(ItemCreatorPage), true, new Dictionary<string, object>
        {
            ["NewItem"] = item
        });
    }

    private async void NewItem_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(ItemCreatorPage), true, new Dictionary<string, object>
        {
            ["NewItem"] = new ItemModel() { CollectionId = collectionId },
        });
    }
}