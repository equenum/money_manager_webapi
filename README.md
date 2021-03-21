# money_manager_webapi
A RESTful API for managing personal budgets.

# Description:

This application has the following architecture:

- Presentation layer: Web API (C#, ASP.NET Core WebAPI);
- Domain layer: Class Library (C#, .NET Core);
- Infrastructure layer: Class Library (C#, .NET Core);

There are some additional project details (architecture, technologies/patterns used, etc.):
- RESTful API, OpenAPI Spec (Swagger);
- SwaggerUI (Swashbuckle);
- Authentication (JWT);
- API versioning;
- Entity Framework (Code First), MS Server;
- Dependency injection (.NET Core dependency injection);
- JSON parsing (Newtonsoft.Json);
- Asynchronous programming;
- DTO pattern (AutoMapper for object mappings);
- CQRS pattern (MediatR);
- Repository pattern;
- UnitOfWork pattern;
- Decorator pattern;
- Integration-testing (xUnit, Fluent Assertions);
- Unit-testing (xUnit, Moq, Fluent Assertions).

Tools used:
- Postman;
- LINQ Pad 5;
- JWT.io.

# Demo:

![](demo.gif)