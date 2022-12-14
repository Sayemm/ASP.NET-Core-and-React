# WEB API

## Controller and Action
01. The fundamental idea of having a web api is that we will have clients that will make HTTP requests to our web api. <br/>
02. An action is a method of a controller that is executed in response to an HTTP request made to our web api <br/>
03. A controller is a class that groups a set of actions. <br/>

## Routing Rules
01. The routing rules are what allow us to map a URL with an action.  <br/>
02. The Route attribute allows us to indicate the base of the endpoint of the controller actions. <br/>

## ActionResult
01. Class that represents all type of data from an action.
02. The ActionResult class is a base class of all result classes, so it can be the return type of action
method that returns any result.

## IActionResult
01. Return any kind of ActionResult.
02.  `return Ok(genre);` 200 OK - genre as part of the body of the response

## Asynchronous Programming
01. Action can be synchronous or asynchronous.
02. While function is executing, our web server - instead of waiting for it to end, better do other tasks in the meantime.
03. There is performance cost when using asynchronous functions.
04. They are ideal for when we **use a database** or do **operations with other web services**.
```c#
public async Task<List<Genre>> GetAllGenres() //In future this method will return a Task -> List<Genre>
{
    await Task.Delay(3000); //Web server will handle any request in the meantime and after 3s it will come back here and continue the execution.
    return _genres;
}
```

## Model Binding
01. It allows us to map data form an HTTP request to parameters of an action.
02. The value indicated in the routing rule is an example of a route value.
03. Query Strings: `api/genres?id=5&name=sayem` -> Route values: `public void Get(int Id, string name)`.
04. Form Values: `public void Post([Frombody] Genre value)`.
05. Model Binder Configurations: BindRequired, BindNever.

## Model Validation
01. Required
02. StringLength
03. Range
04. CreditCard
05. Compare
06. Phone
07. RegularExpressoin
08. Url
09. BindRequired


## Custom Validation
01. **Attribure Validation:** Attribute validation has the advantage of being able to be reused in different models and properties, but its goal is to validate a property, and not a complete model.
02. **Model Validation:** Has the advantage of being able to access all the properties of a model to perform complex validation rules. But cannot be reused with other models.

## Dependency Injection
* When a class A uses class B, we say class B is a dependency of class A.
* Tight coupling means little flexible dependence on other classes.
```c#
//My Services
//Whenever we ask IRepository service, the DI system serves an InMemoryRepository instance
builder.Services.AddSingleton<IRepository, InMemoryRepository>();
```
* Services may have different lifetimes. That is the time serves by the instance of a class.
    * **services.AddTransient**: Wheneven a service is requested, a new instance of the class will be served.
    * **services.AddScope**: Created one per same HTTP request.
    * **services.AddSingleton**: Always same instance.

## Loggers
* Logging means displaying messages in the console or saving the messages to a database.
* ILogger service allows us to centralize the configuration of logging logic of our application.
* 6 level of logging (Critical, Error, Warning, Information, Debug, Trace)

## Middleware
* A HTTP request arrives at our web api and goes through an HTTP request pipeline.
* A **pipe** is a chain of processes connected in such a way that the output of each element of the chain is the input of the next.
* The request pipeline is a set of connected proecesses, which receives a HTTP request and processces to give some kind of result.
* One of the process is MVC process where the controllers and actions are handled.
* But this is not the only process. **We call each process middlewire**.
* MVC is a middleware of the many there are.
* The order of process is important. Such the authorization middleware must come before the MVC middleware.
* Process goes from first one to last one and then returns from last one to first one.

## Filters
* Filters help us run code at certain times in the life cycle of processing an HTTP request.
* Filters are useful when we need to execute logic in several acion of several controllers and we want to avoid having repeat code.
* One of the most used filters is the authorization filter. This user allows us to block access to a resource when a user is not logged in.
* This logic is not specific to a single action but multiple actions of a multiple controllers.
* Filter Types
    * **Authorization Filters**
    * **Resource Filters**: Executed after authorization stage.
    * **Action Filters**: Executed just before and after the execution of an Action. They can be used to manipulate the arguments sent into an action and of the information returned by them.
    * **Exception Filters**: Executed when there was an exception not caught in a try catch during the execution of an action.
    * **Result Filters**: Executed before and after the execution of an action result.
* Filters can also be applied at the controller level in such a way that it applies to all the actions of controller. These filters are placed as attribures either in the actions of in the corresponding controller.
* We can also apply filters at the level of the entire API. These are called global filters.
* Global filters are applied to all the actions of all controllers of our project.
* An example of a filter is cache response which serves to cache the response of an action.