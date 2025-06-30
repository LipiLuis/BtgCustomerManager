namespace BtgCustomerManager.Core.Validation;

public class ValidationException : Exception
{
    public Dictionary<string, List<string>> Errors { get; }
    
    public ValidationException(Dictionary<string, List<string>> errors) 
        : base("Falha na validação do cliente")
    {
        Errors = errors;
    }
    
    public override string ToString()
    {
        return string.Join("\n", 
            Errors.SelectMany(e => 
                e.Value.Select(msg => $"{e.Key}: {msg}")));
    }
}