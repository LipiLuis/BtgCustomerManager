namespace BtgCustomerManager.Core.Validation;


public class ValidationResult
{
    public Dictionary<string, List<string>> Errors { get; } = new();
    
    public bool IsValid => !Errors.Any();
    
    public void AddError(string propertyName, string errorMessage)
    {
        if (!Errors.ContainsKey(propertyName))
            Errors[propertyName] = new List<string>();
            
        Errors[propertyName].Add(errorMessage);
    }
    
    public void Merge(ValidationResult other)
    {
        foreach (var error in other.Errors)
        {
            foreach (var message in error.Value)
            {
                AddError(error.Key, message);
            }
        }
    }
}