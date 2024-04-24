using HankCollections.DataManagers;

namespace HankCollections.Controls;

public partial class ItemControl : ContentView
{
    public static readonly BindableProperty ItemIdProperty = BindableProperty.Create(nameof(ItemId), typeof(int), typeof(ItemControl), 0);
    public static readonly BindableProperty ItemNameProperty = BindableProperty.Create(nameof(ItemName), typeof(string), typeof(ItemControl), string.Empty);
    public static readonly BindableProperty ItemsImageIdProperty = BindableProperty.Create(nameof(ItemsImageId), typeof(int), typeof(ItemControl), 0, propertyChanged: ImageChanged);
    public static readonly BindableProperty ItemStatusProperty = BindableProperty.Create(nameof(ItemStatus), typeof(Models.Status), typeof(ItemControl), Models.Status.Undefined);
    public static readonly BindableProperty ItemSatisfactionProperty = BindableProperty.Create(nameof(ItemSatisfaction), typeof(Models.Satisfaction), typeof(ItemControl), Models.Satisfaction.Undefined);
    public static readonly BindableProperty ItemCommentProperty = BindableProperty.Create(nameof(ItemComment), typeof(string), typeof(ItemControl), string.Empty);
    public static readonly BindableProperty ItemIsSoldProperty = BindableProperty.Create(nameof(ItemIsSold), typeof(bool), typeof(ItemControl), false, propertyChanged: SoldChanged);

    public int ItemId
    {
        get => (int)GetValue(ItemIdProperty);
        set => SetValue(ItemIdProperty, value);
    }
    public string ItemName
    {
        get => (string)GetValue(ItemNameProperty);
        set => SetValue(ItemNameProperty, value);
    }
    public int ItemsImageId
    {
        get => (int)GetValue(ItemsImageIdProperty);
        set => SetValue(ItemsImageIdProperty, value);
    }
    public Models.Status ItemStatus
    {
        get => (Models.Status)GetValue(ItemStatusProperty);
        set => SetValue(ItemStatusProperty, value);
    }
    public Models.Satisfaction ItemSatisfaction
    {
        get => (Models.Satisfaction)GetValue(ItemSatisfactionProperty);
        set => SetValue(ItemSatisfactionProperty, value);
    }
    public string ItemComment
    {
        get => (string)GetValue(ItemCommentProperty);
        set => SetValue(ItemCommentProperty, value);
    }
    public bool ItemIsSold
    {
        get => (bool)GetValue(ItemIsSoldProperty);
        set => SetValue(ItemIsSoldProperty, value);
    }

    private static void ImageChanged(BindableObject bindable, object oldValue, object newValue)
    {
        ItemControl view = (ItemControl)bindable;
        view.UpdateImage(newValue);
    }
    private void UpdateImage(object value)
    {
        int val = Convert.ToInt32(value);

        if (val == 0)
        {
            itemImg.Source = "warning.png";
            return;
        }

        Models.ImageModel image = ImageManager.GetImageById(val);
        itemImg.Source = ImageManager.Base64ToImageSource(image.Data);
    }

    private static void SoldChanged(BindableObject bindable, object oldValue, object newValue)
    {
        ItemControl view = (ItemControl)bindable;
        view.UpdateSold(newValue);
    }
    private void UpdateSold(object value)
    {
        bool val = Convert.ToBoolean(value);

        if (val == true)
        {
            itemIdLabel.TextDecorations = TextDecorations.Strikethrough;
            itemNameLabel.TextDecorations = TextDecorations.Strikethrough;
            itemStatusLabel.TextDecorations = TextDecorations.Strikethrough;
            itemSatisfactionLabel.TextDecorations = TextDecorations.Strikethrough;
            itemCommentLabel.TextDecorations = TextDecorations.Strikethrough;
            itemSoldLabel.TextDecorations = TextDecorations.Strikethrough;
        }
    }

    public ItemControl()
	{
		InitializeComponent();
	}
}