using BtgCustomerManager.Views;

namespace BtgCustomerManager;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(CustomerDetailPage), typeof(CustomerDetailPage));

    }
}