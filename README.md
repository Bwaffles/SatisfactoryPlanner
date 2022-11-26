# SatisfactoryPlanner

## How to Use

### Database Migrations
From the root folder, run `dotnet run --project src/Database/DatabaseMigrator/` to start the interactive migration app during development.


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

#### GET ####
200, 204, or 404 when the item wasn't found? I researched this for hours and I'm going to make the final decision (for now) indicated below:
- If pioneer is found at `/pioneers/9`, return `200 Ok`.
- If pioneer is found at `/pioneers?id=9`, return `200 Ok`.
- If no pioneer is found at `/pioneers/9`, return `404 Not Found`.
- If no pioneer is found at `/pioneers?id=9`, return `204 No Content`.

Reasoning being that `/pioneers/9` is a resource and the resource does not exist. There is no such thing as pioneer 9. However, if you call pioneers and you want to filter to only pioneers with id of 9, then the resource of pioneers does exist but your search yielded no results.

  - https://learn.microsoft.com/en-us/aspnet/core/web-api/action-return-types?view=aspnetcore-7.0#synchronous-action
  - https://stackoverflow.com/a/61049975
  - FYI dissenting opinion https://jsonapi.org/format/#fetching-resources-responses I think the docs are up to interpretation. On the surface it seems to say to always use `200 Ok` and return null. But I think reading it deeper it means more like return `200 Ok` if pioneer 9 exists but there is no data there.
