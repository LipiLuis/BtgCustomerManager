using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BtgCustomerManager.Models.Wrappers;

public class CustomerWrapper : INotifyPropertyChanged
{
    private bool _isSelected;
    private string _name;
    private string _lastName;
    private int _age;
    private string _address;

    public Guid Id { get; } // Adicionado ID

    public bool IsSelected
    {
        get => _isSelected;
        set
        {
            if (_isSelected != value)
            {
                _isSelected = value;
                OnPropertyChanged();
            }
        }
    }

    public string Name
    {
        get => _name;
        set => SetField(ref _name, value);
    }

    public string LastName
    {
        get => _lastName;
        set => SetField(ref _lastName, value);
    }

    public int Age
    {
        get => _age;
        set => SetField(ref _age, value);
    }

    public string Address
    {
        get => _address;
        set => SetField(ref _address, value);
    }

    public CustomerWrapper(Customer customer)
    {
        Id = customer.Id;
        _name = customer.Name;
        _lastName = customer.LastName;
        _age = customer.Age;
        _address = customer.Address;
        IsSelected = false;
    }

    public string FullName => $"{Name} {LastName}";

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        
        if (propertyName is nameof(Name) or nameof(LastName))
        {
            OnPropertyChanged(nameof(FullName));
        }
        
        return true;
    }

    public Customer ToCustomer() => new(Id, Name, LastName, Age, Address);
}