using BtgCustomerManager.ViewModels;

namespace BtgCustomerManager;

public partial class MainPage : ContentPage
{

    public MainPage(MainViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}