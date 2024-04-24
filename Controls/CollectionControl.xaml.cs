using HankCollections.DataManagers;

namespace HankCollections.Controls;

public partial class CollectionControl : ContentView
{
    public static readonly BindableProperty CollectionIdProperty = BindableProperty.Create(nameof(CollectionId), typeof(int), typeof(CollectionControl), 0, propertyChanged: IdChanged);
    public static readonly BindableProperty CollectionNameProperty = BindableProperty.Create(nameof(CollectionName), typeof(string), typeof(CollectionControl), string.Empty);
    public static readonly BindableProperty ElementCountProperty = BindableProperty.Create(nameof(ElementCount), typeof(string), typeof(CollectionControl), string.Empty);

    public int CollectionId
    {
        get => (int)GetValue(CollectionIdProperty);
        set => SetValue(CollectionIdProperty, value);
    }
    public string CollectionName
    {
        get => (string)GetValue(CollectionNameProperty);
        set => SetValue(CollectionNameProperty, value);
    }
    public string ElementCount
    {
        get => (string)GetValue(ElementCountProperty);
        set => SetValue(ElementCountProperty, value);
    }

    private static void IdChanged(BindableObject bindable, object oldValue, object newValue)
    {
        CollectionControl view = (CollectionControl)bindable;
        view.UpdateCount(newValue);
    }
    private void UpdateCount(object value)
    {
        int val = Convert.ToInt32(value);

        int count = ItemManager.GetItemsByCollectionId(val).Count();
        ElementCount = count + (count == 1 ? " element" : " elements");
    }
    public CollectionControl()
	{
		InitializeComponent();
	}
}