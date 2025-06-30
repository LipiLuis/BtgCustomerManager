using BtgCustomerManager.Core.Interfaces;
using BtgCustomerManager.Core.Validation;
using BtgCustomerManager.Models;

namespace BtgCustomerManager.Core.Services;

public class CustomerService : ICustomerService
{
    private readonly List<Customer> _customers = new();
    private readonly ICustomerValidator _validator;

    public CustomerService(ICustomerValidator validator)
    {
        _validator = validator;
    }
    
    public List<Customer> GetCustomers() => _customers;
    
    public Customer? GetCustomer(Guid id) => 
                    _customers.FirstOrDefault(c => c.Id == id);

    public void AddCustomer(Customer customer)
    {
        var validationResult = _validator.Validate(customer);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        _customers.Add(customer);
    }
    
    public void UpdateCustomer(Customer customer)
    {
        var validationResult = _validator.Validate(customer);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        var existing = _customers.FirstOrDefault(c => c.Id == customer.Id);
        if (existing != null)
        {
            existing.UpdateAll(customer);
        }
    }
    
    public void DeleteCustomer(Guid id)
    {
        var customer = _customers.FirstOrDefault(c => c.Id == id);
        if (customer != null)
        {
            _customers.Remove(customer);
        }
    }
}