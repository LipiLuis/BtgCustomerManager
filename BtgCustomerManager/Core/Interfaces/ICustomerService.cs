using BtgCustomerManager.Models;

namespace BtgCustomerManager.Core.Interfaces;
public interface ICustomerService
{
    List<Customer> GetCustomers();
    Customer? GetCustomer(Guid id);
    void AddCustomer(Customer customer);
    void UpdateCustomer(Customer customer);
    void DeleteCustomer(Guid id);
}