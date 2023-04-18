# SatisfactoryPlanner

## How to Use

### Database Migrations
From the root folder, run `dotnet run --project src/Database/DatabaseMigrator/` to start the interactive migration app during development.

### Run Integration Tests
From the root folder, run `Nuke RunAllIntegrationTests`. You need to have Docker Desktop running for the database container to be created.

### Run Client App
From `src/UI/SatisfactoryPlanner.UI`, run `npm start`.

## Technology

### UI
- [React 18.1.0](https://reactjs.org/)
- [Typescript 4.6.4](https://www.typescriptlang.org/)
- [Font Awesome 6.1.1](https://fontawesome.com/icons) - Free icons--do not have a paid license
- [Tailwind CSS 3.0.24](https://tailwindcss.com/docs/installation) - Styling as an alternative to bootstrap
- [Formik 2.2.9](https://formik.org/docs/overview) - Handling forms
- [Yup 0.32.11](https://github.com/jquense/yup) - Form validation

Design methodology is to use task based UI.
- https://codeopinion.com/decomposing-crud-to-a-task-based-ui/

## Backend
- Using a Module Monolithic architecture where the application is broken down into separate independent modules but is all deployed in a single executable.
- The design methodology is Domain Driven Design.
- Each module is designed using clean architecture.

### Intent Based API
Want to use intent based API instead of the typical CRUD Rest API since my application is using more of a command query design. So intead of a generic edit factory method, you would have commands to edit specific pieces of information about that factory. 

- https://codeopinion.com/is-a-rest-api-with-cqrs-possible/
- https://www.thoughtworks.com/insights/blog/rest-api-design-resource-modeling
- https://techblog.pointsbet.com/a-structured-approach-to-designing-intent-based-apis-910ed1fc78f2

#### Authorization
All routes need to be decorated with either `NoPermissionRequired` or `HasPermission` attribute. `NoPermissionRequired` marks a route as one that is accessible to all roles, typically this is reserved for `@me` routes since you will always have permission to access your own data. `HasPermission` requires you to state the name of the permission. The user making the request must have a role with the given permission or they will get a `403 Forbidden` error.

#### GET
200, 204, or 404 when the item wasn't found? I researched this for hours and I'm going to make the final decision (for now) indicated below:
- If pioneer is found at `/pioneers/9`, return `200 Ok`.
- If pioneer is found at `/pioneers?id=9`, return `200 Ok`.
- If no pioneer is found at `/pioneers/9`, return `404 Not Found`.
- If no pioneer is found at `/pioneers?id=9`, return `204 No Content`.

Reasoning being that `/pioneers/9` is a resource and the resource does not exist. There is no such thing as pioneer 9. How did you even get that url? However, if you call the pioneers collection and you want to filter to only pioneers with id of 9, then the resource of pioneers does exist but your search yielded no results.

  - https://learn.microsoft.com/en-us/aspnet/core/web-api/action-return-types?view=aspnetcore-7.0#synchronous-action
  - https://stackoverflow.com/a/61049975
  - FYI dissenting opinion https://jsonapi.org/format/#fetching-resources-responses I think the docs are up to interpretation. On the surface it seems to say to always use `200 Ok` and return null. But I think reading it deeper it means more like return `200 Ok` if pioneer 9 exists but there is no data there.

# Domain Events

Domain events are added to entities whenever an action has been performed. Domain events are published at 2 different stages of the transaction. You can handle a domain event within a transaction so that you can ensure the entire transaction completes together, or you can handle it outside the transaction if it's not critical or dependent on some third party integration.

### Inside Transaction
To handle a domain event within the transaction, create a `XDomainEventHandler` that inherits from `INotificationHandler<XDomainEvent>`. Perform your changes to the domain and it will be saves together with the changes that initiated the domain event when the unit of work has finished.

### Outside Transaction
To handle a domain event outside a transaction, create a `XNotification` that inherits from `DomainNotificationBase<XDomainEvent>`. The notifications are registerd in the module startup class to be processed in the outbox of the module. They will be serialized and saved in the `outbox_messages` table in the module's schema. The quartz scheduler watches this table and runs the `ProcessOutboxCommandHandler` that reads unprocessed messages and executes any handlers that inherit from `INotificationHandler<XNotification>`.

## Internal Commands
From here you can execute a command, say if you want to publish an event from aggregate 1 and have aggregate 2 respond to it, then you would create a command that can be ran as an internal command. Internal commands are also ran through Quartz and get saved to the internal_commands table in the module schema.

## Integration Events
You can also trigger an integration event if you need other modules to be able to subscribe to this event. In this case you create a class like `XPublishEventHandler` that inherits from the same `INotificationHandler<XNotification>`. This handler would publish an integration event to the event bus. The event bus is an in memory message system where all modules can publish and subscribe to events. 

Modules can subscribe to events in their Startup EventBusModule. This creates handlers that will load the event into the modules inbox_messages table. There is a job running called `ProcessInboxCommandHandler` that reads unprocessed messages and executes any handlers for that messages.
