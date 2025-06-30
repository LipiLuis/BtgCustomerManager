using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BtgCustomerManager.Models;
using BtgCustomerManager.ViewModels;

namespace BtgCustomerManager.Views;

public partial class CustomerDetailPage : ContentPage
{
    public CustomerDetailPage(CustomerViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
    
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await ModalContent.ScaleTo(0.95, 50);
        await ModalContent.ScaleTo(1, 100, Easing.SpringOut);
    }
}