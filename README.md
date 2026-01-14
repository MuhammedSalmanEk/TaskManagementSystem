
///// PROJECT ////

1.Project Name Task Management API

    A simple Task Management REST API built with ASP.NET Core Web API following Clean Architecture principles.
    This project demonstrates task CRUD operations with role-based access control using a header-based fake authentication mechanisms.


////// ARCHITECTURE /////

2. Architecture Overview(Clean Architecture)

    TaskManagement
    |
    |--API          -> Controller, Filters,Middleware,Rout,
    |
    |--Application  -> Contain DTO ,Interface
    |
    |--Infrastructure -> Contain Service ,EF core,DbContext,Business logic
    |
    |-- Domain       -> Entities
    |
    |--Tests         -> Unit & Integration Tests


///// PROJECT SETUP ////

3. Project Setup Instructions

    . .NET SDK (8.0)
    . UseInMemoryDatabase
    . VisualStudio

  3.1. check version

  3.2. clone repository
        git clone <repository>
        cd TaskManagement

  3.3 dotnet restore

  3.4 right click Api file and set as startup project Or Use commnd dotnet run --project API then Api availble at https://localhost:5001

  3.5  after run its load page and redirect to swagger doc


  /////// DOCUMENTATION ////////

4. API Documentation
    
  4.0 Authentication and Authorization

    This project uses a fake in-memory user store (for demo purposes only).

    public static class FakeUsers
    {
        public static readonly Dictionary<string, string> Users = new()
        {
            { "admin", "Admin" },
            { "user", "User" },
            { "salman", "User" }
        };
    }

    How Authorization work
    Role      Permission
    Admin	View all tasks, mark tasks as completed
    User	Create tasks, view own tasks only

    Test Credentials
    UserName   Role
    admin      Admin
    user       user
    salman     user




  4.1 API Usage Guide 

      1. Create Task
        POST /api/tasks
        Header :
        user:salman
        Request Body:
        {
          "title": "Learn Clean Architecture",
          "description": "Understand layered design",
          "dueDate": "2026-01-20"
        }

        Response:
        200 Ok



       2. Get Tasks
        Role - USER :
        GET /api/tasks
        Header :
        user:salman
        Response:
        {
          "id": 1,
          "title": "Task",
          "description": "test",
          "isCompleted": false,
          "createdAt": "2026-01-12 16:57:46",
          "dueDate": null,
          "userId": "salman"
        }


       2. Role - ADMIN :
        GET /api/tasks
        Header :
        user:admin  
        Response:
        {
          "id": 1,
          "title": "Task",
          "description": "test",
          "isCompleted": false,
          "createdAt": "2026-01-12 16:57:46",
          "dueDate": null,
          "userId": "salman"
        }

        {
          "id": 2,
          "title": "Learn Clean Architecture",
          "description": "Understand layered design",
          "dueDate": "2026-01-20"
          "isCompleted": false,
          "createdAt": "2026-01-12 16:57:46",
          "userId": "user"
        }


       3. Update Task
        PUT /api/tasks/{id}
        Header :
        user:salman
        Response:
        {
            204 No Content
        }

        Mark Task as Completed (Admin Only)
        PUT /api/tasks/{id}/complete
        Header :
        user:admin
        Response:
        {
            204 No Content
        }


       4. Erro Handling
        Scenario	Status Code
        Missing user header	401 Unauthorized
        Invalid user	401 Unauthorized
        Task not found	404 Not Found
        Server error	500 Internal Server Error

        Global exception handling is recommended using middleware for production.



 /////// UNIT TESTS ////

4. UnitTests

    Run All Tests

    cmd excute this command dotnet test

    Test Coverage Includes:

    Integration tests for API endpoints

    Authorization checks (Admin vs User)

    Missing header validation






  