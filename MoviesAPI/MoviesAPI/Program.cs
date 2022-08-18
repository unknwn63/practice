using Microsoft.AspNetCore.Authentication.JwtBearer;
using MoviesAPI.Filters;
using MoviesAPI.Services;


namespace MoviesAPI
{
    public class Program
    {

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.

            builder.Services.AddControllers(options =>
            {
                options.Filters.Add(typeof(MyExceptionFilter));
            }).AddXmlDataContractSerializerFormatters();
            builder.Services.AddResponseCaching();                                  // introducing AddResponseCaching so we can use it in pipeline
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
            builder.Services.AddSingleton<IRepository, InMemoryRepository>();       //every time class requests IRepository it will get InMemoryRepository instance
            builder.Services.AddTransient<MyActionFilter>();
            builder.Services.AddTransient<Microsoft.Extensions.Hosting.IHostedService, WriteToFileHostedService>();
            // AddSingleton same instance of class InMemoryRepository
            // AddScoped same instance of the class in the same HTTP request
            // AddTransient different instances of the class even in same HTTP request
            
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.Use(async (context, next) =>                                        // need to use async here
            {
                using (var swapStream = new MemoryStream())                         // MemoryStream allows us to use in memory data as streams
                {                                                                   // Stream is an object used to transfer data

                    var originalResponseBody = context.Response.Body;               // originalResponseBody = body from HTTP response
                    context.Response.Body = swapStream;                             // we are giving those values to swapStream

                    await next.Invoke();                                            // continuing the execusion of the pipeline and we wait for the response to return to us

                    swapStream.Seek(0, SeekOrigin.Begin);                           //  setting the position to the beginning of the stream
                    string responseBody = new StreamReader(swapStream).ReadToEnd(); // capturing the response, ReadToEnd() to read the full response
                    swapStream.Seek(0, SeekOrigin.Begin);                           //  setting the position to the beginning of the stream         

                    await swapStream.CopyToAsync(originalResponseBody);             // we are copying the response we got to originalReponseBody
                    context.Response.Body = originalResponseBody;                   // setting oRB to be body fromm HTTP response (needed to do this because we can not read the first body)

                    app.Logger.LogInformation(responseBody);                        // logging every body from HTTP body response
                    
                }
            });

            app.Map("/map1", (app) =>
            {
                app.Run(async context =>
                {
                    await context.Response.WriteAsync("I'm short circuiting the pipeline");
                });

            });
            

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseResponseCaching();                                              // reading from cache, doesn't work if Authorization header is present in HTTP requst

            app.UseAuthentication();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();


        }
      
    }
}