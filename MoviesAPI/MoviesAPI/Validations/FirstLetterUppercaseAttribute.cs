using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Validations
{
    public class FirstLetterUppercaseAttribute : ValidationAttribute // need to inherit this class so we can use ValidataionContext,ValidataionResult...
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext) // value = value of the propert
                                                                                                         // context in which validation is running
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
                return ValidationResult.Success; // makes no sense to check if null or empty has first letter uppercase so we return Success

            var firstLetter = value.ToString()[0].ToString();

            if (firstLetter != firstLetter.ToUpper())

            {
                return new ValidationResult("First letter should be uppercase");
            }
            return ValidationResult.Success;
        }
    }
}
