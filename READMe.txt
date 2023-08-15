Task Management API
This is a simple task management API built using C# (ASP.NET Core) and MySQL database. It provides endpoints to manage tasks, including listing tasks, retrieving task details by ID, creating new tasks, updating task status, and deleting tasks.

Installation and Setup
1. Clone the repository from GitHub:
git clone <repo_url>
cd task-management-api

2. Install the necessary NuGet packages:
> dotnet restore

3. Create a MySQL database for the API. Modify the connection string in appsettings.json to point to your MySQL server and database.

4. Apply database migrations to create the necessary tables:
> dotnet ef database update

5. Build and run the API:
> dotnet run

The API should now be running at e.g. https://localhost:5001.

Endpoints
1. GET /api/Task - Retrieve a list of all tasks.

2. GET /api/Task/{id} - Retrieve a single task by its ID.

3. POST /api/Task - Create a new task. Payload should contain title and description in the request body.

4. PUT /api/Task/{id}/status - Update the status of a task. Payload should contain the new status in the request body.

5. DELETE /api/Task/{id} - Delete a task by its ID.

Logging
The API is integrated with Serilog to provide structured logging. The logs are written to both the console and a rolling log file in the Logs folder.

Test Cases
Test cases have been written using xUnit to demonstrate the functionality of the API. To run the tests, execute the following command:

> dotnet test

These test cases demonstrate how to test the API endpoints and their behavior under different scenarios.

Remember that these are just sample test cases, and you should write more comprehensive test cases based on your specific requirements.

The README provides installation instructions, endpoint details, logging information, and test case examples. With this, users and developers can understand how to set up and use the API effectively. Ensure to adapt the README and test cases based on your actual implementation and requirements
