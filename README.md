# SatisfactoryPlanner

## How to Use

### Database Migrations
From the root folder, run `dotnet run --project src/Database/DatabaseMigrator/ [debug|release] [masterConnectionString] [connectionString]` to start the interactive migration app during development.

e.g. `dotnet run --project src/Database/DatabaseMigrator/ debug "Server=127.0.0.1;User Id=myusername;Password=mypassword" "Server=127.0.0.1;Database=satisfactory-planner;User Id=myusername;Password=mypassword"`

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
- [craco 7.1.0](https://www.npmjs.com/package/@craco/craco) - Path mapping

Design methodology is to use task based UI.
- https://codeopinion.com/decomposing-crud-to-a-task-based-ui/

## Backend
- Using a Module Monolithic architecture where the application is broken down into separate independent modules but is all deployed in a single executable.
- The design methodology is Domain Driven Design.
- Each module is designed using clean architecture.

### Intent Based API
Using intent based API instead of the typical CRUD Rest API since this application is using CQRS architecture. So intead of a generic edit factory method, you would have commands to edit specific pieces of information about that factory. 

Because of this, most API will be either a GET or POST request. There's a good argument that endpoints such as `worlds/worldId/nodes/nodeId/increase-extraction-rate` could be a PATCH because it's just updating a small piece of a world node, but the thinking here is that increase-extraction-rate is the resource and the resource is a command. We're not exposing the resource so that it can be Created, Updated, or Deleted, we're providing explicit actions that can be performed. If the client wants to change information about the worldNode, we'll provide specific end points of all the available actions instead of leaving it up to the client to figure out what combination of fields can be changed and when. 

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

#### POST
`200 Ok` - When the request completes successfully and there is data to be returned. The data will be returned in the body of the response.
`204 No Content` - When the request completes successfully and there is no data to be returned.

## Validation

I use 3 levels of validation in this order: UI, Request, and Domain.

### UI
TODO

### Request
These are validations performed on Commands and Queries before the Handler will execute. These validations are very simple and do not involve any system data, other than what is provided in the request. For example, checking that an Id it not empty.

### Domain
TODO

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

## Domain

### Entities

#### Identity
All entities have strongly typed ids following the advice in https://andrewlock.net/using-strongly-typed-entity-ids-to-avoid-primitive-obsession-part-1/. Ids are generated by the entity on creation. This approach allows the ownership of the id to stay within the entity and ignores whatever method of persistance is used. Ids are always get only properties.

This extra overhead provides us with the benefit of compile type protection that we don't pass the wrong type of id into a method. If you had a method that accepted 2 ids from different entities and both ids were GUIDs then you could mistakenly pass the parameters in the wrong order and the compiler wouldn't know the difference. You would hopefully get test failures, or runtime failures but that's a lot later in the cycle. If you instead created WorldId and NodeId and used those instead of passing GUIDs around the domain you would get a compiler error when you try to pass in a NodeId for a WorldId. 

### Nodes

I've gone back and forth probably 5 times now on how this should be implemented. Ideally, you would expect to have a Node object that's your aggregate root and you would Tap, IncreaseExtractionRate etc. However the Node is also an entity with a specific location in the world. This Node is the same for all pioneers so the Node is more like a Node reference. When a new World is created, it should have all the Nodes as untapped. You can think of it as every world has its own copy of the nodes. I ended up with 2 options for implementing this:

 1. When a world is created, create clones of all the nodes in the world called WorldNode that default to untapped. This would be the main queried table and the one that the UI works with. WorldNode would have a NodeId and WorldId. All actions will be done to the WorldNode entity.
   
     **Pros** 
     - Simplifies the mental model because you would do all work on the WorldNode and it matches the domain.
     - I can see a list of all the WorldNodes and tell how many are tapped and what at what rate because all Nodes have been pre-populated.
     
     **Cons** 
     - For every world I'm going to add around 350 WorldNodes even if they never get used. (=> Is this me being too concerned with storage at expense of domain?)
     - Whenever a Node is added/removed the WorldNodes all need to be updated of this change.
     
 2. When you tap a node it creates a new entity called a TappedNode and you then make all node changes to that object.
   
    **Cons** 
    - The mental model is disjointed. Consumers need to switch from using the Node resource to the TappedNode resource. 
   
    **Pros** 
    - No upfront work to configure new worlds.
    
 Honestly, after writing all this out, it seems like option 1 is the best option. My fear of wasting database storage, or how to keep it synced ruined the domain model. How do I handle new nodes being added/removed in updates? Just make it part of my script.
 
 Would I call it Node and WorldNode? The api makes so much more sense to be /api/world/{worldId}/nodes/{nodeId}/tap, /api/world/{worldId}/resources. I think forcing myself to split the routes by bounded context also ruins the api experience. Discord has channel id in their routes, GitHub has repo id in the route.

 ## Extractors

 Currently there are 4 extractors. They come from the game data files and there would never be new or different ones unless there is a game update. Initially I chose to add the extractor as an entity in my database, with its own id and with the game code as a column that I can use in future updates to perform updates by cross checking the game data files. After working with this on more complicated code, I'm now starting to see the flaws:

   1. My entity can be any kind of extractor, so when I'm testing and I want to write a test using the Miner Mk.2 I have to create a fake testing extractor. I don't need to have an infinite number of test cases, I just want to test against the extractor that I know exists and I want to do it easily.
   2. Since this is an entity and not owned by the WorldNode, it only has a reference to the ExtractorId and anytime I need to get information from the extractor (like the max extraction rate, or what kind of resources are allowed) I need to get the whole extractor to give to my domain model.

My first thought was can I just reference the entire extractor on the WorldNode? Then I started to think that the WorldNode doesn't own the Extractor. If the WorldNode has an Extractor then I'd have to make sure it's completely immutable so that when I save my WorldNode it doesn't update the Extractor in the database for everyone. I think that this breaks some good domain modeling practices and gives off a bit of a smell.

So I like the idea of referencing the entire extractor on the WorldNode, but not as an entity. What if it was a value object? What's the difference between this and what I did with the NodePurity? Does this need to be in the database at all? What if I had the 4 extractors in memory. I could have a MinerMk1, MinerMk2, MinerMk3 and OilExtractor extractor classes that has all the same information that was in my database. This means that instead of my tests need a factory class to create the MinerMk1, I can use the production code factory to create it. My WorldNode would no longer just have reference to ExtractorId, but it would have the Extractor itself since I would make it a value object.

* Need to refactor Extractor to use a Code for it's primary key instead of Id for easier reference
* Update extractor_allowed_resources to use code
* Update world_nodes to use code
* Update references to use code instead of id
* Change Extractor from an entity to a value object with the 4 subclasses
* Replace all access to extractors from the database table to use the ExtractorFactory

## Frontend

### Path Mapping
Using Typescript and craco to simplify my paths. This allows me to turn `import { Button } from "../../../components/Elements/Button";` into `import { Button } from "@/components/Elements/Button";` which improves the readability and maintability of the code.

The setup involved in this starts in `tsconfig.json` by setting up the "paths" value to set up the initial path aliases that I want to use. This allows me to work with the new alias in the IDE, but with just this I'll get compilation errors that it can't figure out how to load the modules. To fix this issue I'm using [craco](https://www.npmjs.com/package/@craco/craco). In `package.json`, I changed my scripts to run craco instead of react-scriptsm. And I added `craco.config.js` which sets up the @ alias for webpack so it understands the file paths when creating the bundle.

https://blog.logrocket.com/using-path-aliases-cleaner-react-typescript-imports/
