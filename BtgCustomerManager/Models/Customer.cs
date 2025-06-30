namespace BtgCustomerManager.Models;

public class Customer
{
    public Guid Id { get;  set; }
    public string Name { get;  private set; }
    public string LastName { get; private set; }
    public int Age { get; private set; }
    public string Address { get; private set; }
    public DateTime LastModified { get;  set; }

    public Customer()
    {
            
    }
    public Customer(Guid id, string name, string lastName, int age, string address)
    {
        Id = id;
        Name = name;
        LastName = lastName;
        Age = age;
        Address = address;
    }
    
    public void UpdateName(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName))
            throw new ArgumentException("Nome não pode ser vazio");
        
        Name = newName;
        LastModified = DateTime.UtcNow;
    }

    public void UpdateLastName(string newLastName)
    {
        if (string.IsNullOrWhiteSpace(newLastName))
            throw new ArgumentException("Sobrenome não pode ser vazio");
        
        LastName = newLastName;
        LastModified = DateTime.UtcNow;
    }

    public void UpdateAge(int newAge)
    {
        if (newAge <= 0)
            throw new ArgumentException("Idade deve ser maior que zero");
        
        Age = newAge;
        LastModified = DateTime.UtcNow;
    }

    public void UpdateAddress(string newAddress)
    {
        if (string.IsNullOrWhiteSpace(newAddress))
            throw new ArgumentException("Endereço não pode ser vazio");
        
        Address = newAddress;
        LastModified = DateTime.UtcNow;
    }

    public void UpdateAll(Customer customer)
    {
        UpdateName(customer.Name);
        UpdateLastName(customer.LastName);
        UpdateAge(customer.Age);
        UpdateAddress(customer.Address);
    }
}