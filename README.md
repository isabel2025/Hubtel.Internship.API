# Todo API

A simple ASP.NET Core API I built during my software engineering internship at Hubtel.

The project uses PostgreSQL and Entity Framework Core to store and retrieve tasks for different users.

## What it does

- Add a task
- Get tasks for a user
- Store task status and creation time
- Return simple API responses
- Swagger support for testing the endpoints

## Tech used

- C#
- ASP.NET Core
- PostgreSQL
- Entity Framework Core
- Swagger

## Endpoints

### Add a task

`POST /api/todo-app/add-task`

Example request:

```json
{
  "title": "Finish report",
  "description": "Complete the weekly report",
  "userId": "user-1"
}
```

### Get a user's tasks

`GET /api/todo-app/tasks/{userId}`

## Running locally

1. Make sure PostgreSQL is running.
2. Update the connection string in `appsettings.json` with your local PostgreSQL details.
3. Run:

```bash
dotnet restore
dotnet run
```

The app creates the required table on first run. Swagger is available when the project is running in development mode.
