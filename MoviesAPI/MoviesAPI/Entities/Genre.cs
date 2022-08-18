using MoviesAPI.Validations;
using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Entities
{
    public class Genre : IValidatableObject
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "The field with name {0} is required")] //validating models 
        [StringLength(40)]                                              // string lenght cant be over (x)
       // [FirstLetterUppercase]                                        // this is how you apply attribute validation after creating it in FirstLetterUpperCaseAttribute
        public string Name { get; set; }
        //[Range(18, 120)]                                              //range from (x, y)
        //public int Age { get; set; }
        //[CreditCard]                                                  // sets the creditcard template
        //public string CreditCard { get; set; }
        //[Url]                                                         // sets the url template
        //public string Url { get; set; }

        // model validations only occure after atribute validations (it will catch StringLenght validation before FirstLetter validation)
        // model validations only work in the class in this case Genre and can only be used for its properties
        // use model validations when you need validations only for properties in this class and attribute validations when you have multiple properties across
        // multiple classes that have same validation process
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) 
        {
           if (!string.IsNullOrEmpty(Name))
            {
                var firstletter = Name[0].ToString();
                if (firstletter != firstletter.ToUpper())
                {
                    yield return new ValidationResult("First letter should be uppercase", new string[] {nameof(Name)});
                    //2nd argument is member name that has the error which is Name
                }
            }
        }
    }
}
