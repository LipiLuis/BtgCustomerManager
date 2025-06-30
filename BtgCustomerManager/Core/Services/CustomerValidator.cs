using BtgCustomerManager.Core.Interfaces;
using BtgCustomerManager.Core.Validation;
using BtgCustomerManager.Models;

namespace BtgCustomerManager.Core.Services;

public class CustomerValidator : ICustomerValidator
{
    public ValidationResult Validate(Customer customer)
    {
        var result = new ValidationResult();
        
        result.Merge(ValidateName(customer.Name));
        result.Merge(ValidateLastName(customer.LastName));
        result.Merge(ValidateAddress(customer.Address));
        result.Merge(ValidateAge(customer.Age));
        
        return result;
    }

    public ValidationResult ValidateName(string name)
    {
        var result = new ValidationResult();
        
        if (string.IsNullOrWhiteSpace(name))
            result.AddError(nameof(Customer.Name), "Nome é obrigatório");
        
        return result;
    }

    public ValidationResult ValidateLastName(string lastName)
    {
        var result = new ValidationResult();
        
        if (string.IsNullOrWhiteSpace(lastName))
            result.AddError(nameof(Customer.LastName), "Sobrenome é obrigatório");
        
        return result;
    }
    
    public ValidationResult ValidateAddress(string address)
    {
        var result = new ValidationResult();
        
        if (string.IsNullOrWhiteSpace(address))
            result.AddError(nameof(Customer.Address), "Endereço é obrigatório");
        
        return result;
    }
    public ValidationResult ValidateAge(int age)
    {
        var result = new ValidationResult();
        
        if (age < 18)
            result.AddError(nameof(Customer.Age), "Cliente deve ser maior de idade");
        
        if (age > 122)
            result.AddError(nameof(Customer.Age), "Idade máxima validada é 122 anos.");
        return result;
    }
}