# SatisfactoryPlanner

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

![image](https://user-images.githubusercontent.com/5383859/203396355-00f7ec37-6909-4afc-bc1a-5caf0f04443e.png)

- https://techblog.pointsbet.com/a-structured-approach-to-designing-intent-based-apis-910ed1fc78f2
- https://codeopinion.com/is-a-rest-api-with-cqrs-possible/
