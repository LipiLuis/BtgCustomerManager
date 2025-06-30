using BtgCustomerManager.Core.Validation;
using BtgCustomerManager.Models;

namespace BtgCustomerManager.Core.Interfaces;

public interface ICustomerValidator
{
    ValidationResult Validate(Customer customer);
    ValidationResult ValidateName(string name);
    ValidationResult ValidateLastName(string lastName);
    ValidationResult ValidateAddress(string address);
    ValidationResult ValidateAge(int age);
}