using System.Collections.Generic;

namespace ChatLibrary.Validation
{
    public interface IValidation<T>
    {
        bool IsValid(T entity);

        IEnumerable<string> Errors(T entity);
    }
}