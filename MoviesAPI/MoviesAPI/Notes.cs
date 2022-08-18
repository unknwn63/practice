namespace MoviesAPI
{
    public class Notes
    {

        //                                                      PREPARING THE REPOSITORY

        //                                                      CONTROLERS AND ACTIONS

        // inherit from ControllerBase to use NotFound(); NoContent();

        //                                                          ROUTING RULES

        //                                                      RETURN TYPE FROM ACTION

        // ActionResult - class that represents all type of data that can be result of an action
        // ActionResult<T> allows us to return specific type T or Action result ActionResult<Genre> we can return Genre or ActionResult (NotFound, NoContent)
        // If we use IActionResult we can return any action result and any type that we set in the method: return Ok(T) in this case Ok(genre)

        //                                                             ASYNC

        // must return Task<T> in this case Task<List<Genre>>

        //                                                          MODEL BINDING

        // allows us to map data from HTTP request to an action
        // we can get data from Querystrings, Body or Header of the HTTP request
        // BindRequired - parameter is required 
        // BindNever - parameter will always be null

        // ModelState.IsValid is how we check the model binding

        // if (!Model.State.IsValid)
        //  {
        //      return BadRequest(ModelState);
        //  }

        // IMPORTANT IF WE JUST ADD [ApiController] ON A CLASS LEVEL WE DONT NEED TO IMPLEMENT THIS LOGIC

        //                                                           MODEL VALIDATION

        // set rules that model needs to follow (Required, StringLenght, Range, etc...)

        //                                                          CUSTOM VALIDATIONS

        // ATTRIBUTE VALIDATION

        // add them in separate folder and make validations in a class that inherits from ValidationAttribute
        // and override this method 
        // protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        // value - value of the property
        // validation context -

        // MODEL VALIDATION

        // we make it inside of the class of the object we want to implement these validations into
        // we inherit from IValidatableObject and implement its interface
        // model validations only occur after attribute validations

        //                                                          DEPENDENCY INJECTION

        //                                                              SERVICES

        // builder.Services.AddSingleton<IRepository, InMemoryRepository> 
        //      - AddSingleton same instance of class InMemoryRepository
        //      - AddScoped same instance of the class in the same HTTP request
        //      - AddTransient different instances of the class even in same HTTP request
        // so whenever class or method requests IRepository type it will get InMemoryRepository

        //                                                               CREATING AN OBJECT

        // when creating a resource we should return information that resource was created and the location from where we can access it
        // [HttpPost("{Id:int}, Name = "getGenre")]
        // return new CreatedAtRouteResult("getGenre", new { Id = genre.Id}, genre);
        //                                  routeName      routeValue         value
        // routeName - where we created it
        // routeValue - we need to insert Id in the route as we definded so we say that that Id is equal to new genre Id
        // value - what we created, in this case genre

        //                                                                  LOGGERS

        // if we want to use Loggers we need to inject it into any class GenresController(IRepository repository,ILogger<GenresController> logger)
        
        //                                                                 MIDDLEWARE

        //                                                                  FILTERS

        // Authorization
        // Resource
        // Action 
        // Exception
        // Result

        // with custom filters we need to implement IActionFilter and implement its interface
        // [ServiceFilter(typeof(nameOfOurFilter))]
        // register it as a service in program.cs

        // with Exception filters we need to implement ExceptionFilterAttribute interface and overrider  OnException method
        // if we want to apply it globaly we need to register it in Program.cs in AddControllers service

        //                                                              ADDING XML SUPPORT

        // all we need to do is include them in ther service where we added controllers
        // builder.Service.AddControllers().AddXmlDataContractSerializerFormatters(); and thats its
        // we apply it through header we add Accept key and set value to application/xml
        // we can specify 2 or more types but it will select the first format it can serve
        // we can both SEND and RECIEVE information with those formats

        //                                                     RECURRING BACGROUND TASKS WITH IHOSTEDSERVICE           

        // create new class that implements IHostedService and implement its interface
        // add it as a service




       








    }
}
