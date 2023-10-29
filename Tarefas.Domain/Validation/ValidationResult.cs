namespace Tarefas.Domain.Validation
{
    public class ValidationResult<TEntity> where TEntity : class
    {
        public TEntity ObjectResult { get; private set; }
        public string Message { get; private set; }
        public bool IsValid { get; private set; }

        public ValidationResult(bool isValid, string message, TEntity objectResult = null)
        {
            IsValid = isValid;
            Message = message;
            ObjectResult = objectResult;
        }

    }
}
