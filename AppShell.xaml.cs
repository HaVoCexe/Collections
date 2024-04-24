using HankCollections.Pages;

namespace HankCollections
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(ItemCreatorPage), typeof(ItemCreatorPage));
        }
    }
}
