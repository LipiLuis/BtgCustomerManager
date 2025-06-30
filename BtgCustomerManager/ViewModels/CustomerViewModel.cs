using System.Collections.ObjectModel;
using System.Windows.Input;
using BtgCustomerManager.Core.Interfaces;
using BtgCustomerManager.Models;
using BtgCustomerManager.Models.Wrappers;

namespace BtgCustomerManager.ViewModels;

public class CustomerViewModel : BaseViewModel
{
    private readonly ICustomerService _customerService;
    private readonly ICustomerValidator _validator;
    private CustomerWrapper _customerWrapper;
    private string _errorMessage;
    private bool _hasError;
    private string _title;
    private readonly TaskCompletionSource<bool> _tcs;

    public CustomerWrapper CustomerWrapper
    {
        get => _customerWrapper;
        set
        {
            _customerWrapper = value;
            OnPropertyChanged();
        }
    }

    public string Title
    {
        get => _title;
        set
        {
            _title = value;
            OnPropertyChanged();
        }
    }

    public string ErrorMessage
    {
        get => _errorMessage;
        set
        {
            _errorMessage = value;
            OnPropertyChanged();
        }
    }

    public bool HasError
    {
        get => _hasError;
        set
        {
            _hasError = value;
            OnPropertyChanged();
        }
    }

    public ICommand SaveCommand { get; }
    public ICommand CancelCommand { get; }

    public CustomerViewModel(
        ICustomerService customerService,
        ICustomerValidator validator,
        TaskCompletionSource<bool> tcs)
    {
        _customerService = customerService;
        _validator = validator;
        _tcs = tcs;
        
        CustomerWrapper = new CustomerWrapper(new Customer());
        Title = "Novo Cliente";
        
        SaveCommand = new Command(async () => await Save());
        CancelCommand = new Command(async () => await Cancel());
    }

    public void Initialize(CustomerWrapper customerWrapper = null)
    {
        if (customerWrapper != null)
        {
            CustomerWrapper = new CustomerWrapper(new Customer(customerWrapper.Id, customerWrapper.Name, customerWrapper.LastName, customerWrapper.Age, customerWrapper.Address) { Id = customerWrapper.Id });
            Title = "Editar Cliente";
        }
    }

    private async Task Save()
    {
        try
        {
            Customer newCustomer = new Customer(CustomerWrapper.Id, CustomerWrapper.Name, CustomerWrapper.LastName, CustomerWrapper.Age, CustomerWrapper.Address);
            var validationResult = _validator.Validate(newCustomer);
            if (!validationResult.IsValid)
            {
                ErrorMessage = string.Join("\n",
                    validationResult.Errors.SelectMany(e =>
                        e.Value.Select(msg => $"{msg}\n")));
                HasError = true;

                await Shell.Current.DisplayAlert("Erro", ErrorMessage, "OK");
                return;
            }

            if (newCustomer.Id == Guid.Empty)
            {
                 newCustomer.Id = Guid.NewGuid();
                _customerService.AddCustomer(newCustomer);
                await Shell.Current.DisplayAlert("Sucesso", "Salvo com Sucesso!", "OK");
            }
            else
            {
                _customerService.UpdateCustomer(newCustomer);
                await Shell.Current.DisplayAlert("Sucesso", "Atualizado com Sucesso!", "OK");
            }

            _tcs.SetResult(true);
            await CloseModal();
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
            HasError = true;
            await Shell.Current.DisplayAlert("Erro", ErrorMessage, "OK");
        }
    }

    private async Task Cancel()
    {
        _tcs.SetResult(false);
        await CloseModal();
    }

    private async Task CloseModal()
    {
        await Shell.Current.Navigation.PopModalAsync();
    }
}