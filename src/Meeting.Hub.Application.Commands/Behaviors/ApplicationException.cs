namespace Meeting.Hub.Application.Commands.Behaviors;

public class ApplicationException : ValidationException
{
    public IReadOnlyList<ValidationResult> ValidationResults { get; }

    public ApplicationException(IEnumerable<ValidationResult> validationResults)
        : base("Ocorreram erros de validação.")
    {
        ValidationResults = validationResults.ToList();
    }

}