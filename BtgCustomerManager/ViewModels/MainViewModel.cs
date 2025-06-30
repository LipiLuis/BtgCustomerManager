using System.Collections.ObjectModel;
using System.Windows.Input;
using BtgCustomerManager.Core.Interfaces;
using BtgCustomerManager.Core.Services;
using BtgCustomerManager.Models.Wrappers;
using BtgCustomerManager.Views;

namespace BtgCustomerManager.ViewModels;

public class MainViewModel : BaseViewModel 
{
    private readonly ICustomerService _customerService;
    private ObservableCollection<CustomerWrapper> _customers;
    private ObservableCollection<CustomerWrapper> _selectedCustomers = new();
    private int _selectedCount;
    private bool _isBusy;

    public ObservableCollection<CustomerWrapper> Customers
    {
        get => _customers;
        set
        {
            _customers = value;
            OnPropertyChanged();
        }
    }


    public bool IsBusy
    {
        get => _isBusy;
        set
        {
            _isBusy = value;
            OnPropertyChanged();
        }
    }

    public ICommand AddCustomerCommand { get; }
    public ICommand EditCustomerCommand { get; }
    public ICommand DeleteCustomersCommand { get; }
    public ICommand LoadCustomersCommand { get; }

    public MainViewModel(ICustomerService customerService)
    {
        _customerService = customerService;

        AddCustomerCommand = new Command(AddCustomer);
        EditCustomerCommand = new Command(EditSelectedCustomer);
        DeleteCustomersCommand = new Command(DeleteSelectedCustomers);
        LoadCustomersCommand = new Command(LoadCustomers);
        LoadCustomers();
    }
    
    private async void AddCustomer()
    {
        var tcs = new TaskCompletionSource<bool>();
        var modal = new CustomerDetailPage(
            new CustomerViewModel(_customerService, new CustomerValidator(), tcs)
        );
        
        await Shell.Current.Navigation.PushModalAsync(modal);
        var result = await tcs.Task;

        if (result)
        {
            await LoadCustomersAsync();
        }
    }
    
    private async void EditSelectedCustomer()
    {
        var selected = Customers.Where(c => c.IsSelected).ToList();
        if (selected.Count != 1)
        {
            await Shell.Current.DisplayAlert("Alerta", "Marque apenas um item de cada vez", "OK");
            return;
        }

        var customerToEdit = selected.First();
        var tcs = new TaskCompletionSource<bool>();
    
        var modal = new CustomerDetailPage(new CustomerViewModel(_customerService, new CustomerValidator(), tcs));
        ((CustomerViewModel)modal.BindingContext).Initialize(customerToEdit);
    
        await Shell.Current.Navigation.PushModalAsync(modal);
        if (await tcs.Task)
        {
            await LoadCustomersAsync();
        }
    }

    private async void DeleteSelectedCustomers()
    {
        var selected = Customers.Where(c => c.IsSelected).ToList();
        if (!selected.Any()) return;

        bool confirm = await Shell.Current.DisplayAlert(
            "Confirmar",
            $"Excluir {selected.Count} cliente(s) selecionado(s)?",
            "Sim", "Não");
        
        if (!confirm) return;

        IsBusy = true;
    
        try
        {
            foreach (var customer in selected)
            {
                _customerService.DeleteCustomer(customer.Id);
                Customers.Remove(customer);
            }
        }
        finally
        {
            IsBusy = false;
        }
    }
    private async Task LoadCustomersAsync()
    {
        IsBusy = true;
        
        try
        {
            var customerList = await Task.Run(() => _customerService.GetCustomers());
            Customers = new ObservableCollection<CustomerWrapper>(
                customerList.Select(c => new CustomerWrapper(c)));
        }
        finally
        {
            IsBusy = false;
        }
    }
    private void LoadCustomers() => LoadCustomersAsync().ConfigureAwait(false);
}