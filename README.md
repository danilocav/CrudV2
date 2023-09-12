# CrudV2

CrudV2 is a sample project showcasing an ASP.NET Core Blazor web application with a Web API for user management. The project demonstrates best practices for building a complete web application, including JWT authentication, interaction with a MySQL database, and a Blazor user interface.

## Installation

To run the project on your machine, follow these steps:

1. Clone this repository:

git clone https://github.com/your-username/CrudV2.git
cd CrudV2


2. Open the solution in your preferred IDE (Visual Studio, Visual Studio Code, etc.).

3. Configure the connection string in the `appsettings.json` file in `CrudV2.Api` to point to your MySQL database.

4. Run this script and migrations to create the database:

	CREATE DATABASE crud 

	CREATE TABLE crud.users 
	( 
		id INT NOT NULL, 
		name VARCHAR(100) NULL, 
		password VARCHAR(100) NULL, 
		email VARCHAR(100) NULL, 
		PRIMARY KEY (id)
	);

cd CrudV2.Api
dotnet ef database update

5. Build and run the application:

dotnet run

The application will be available at `https://localhost:7145` and `http://localhost:5170`.

## Usage

### User API

- `GET /api/user`: Returns all users.
- `GET /api/user/{id}`: Returns a user by ID.
- `POST /api/user`: Adds a new user.
- `PUT /api/user/{id}`: Updates a user by ID.
- `DELETE /api/user/{id}`: Removes a user by ID.

### Authentication

Authentication is done via JSON Web Tokens (JWT). To delete a user you need to make a request with credentials.

### Project Structure

- `CrudV2.Api`: The application's API layer.
- `CrudV2.Business`: Business logic and use cases.
- `CrudV2.Data`: Data access layer and repositories.
- `CrudV2.Client`: Blazor user interface.
- `CrudV2.Shared`: Shared types and DTOs.


## Contact

For more information, contact me at [danilo.cavi@gmail.com](mailto:danilo.cavi@gmail.com).
