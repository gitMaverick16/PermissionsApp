# PermissionApp

PermissionApp is an application that implements the CQRS (Command Query Responsibility Segregation) pattern with a clean architecture. The application is split into two backend microservices: one for handling write operations and another for handling read operations. The write database is SQL Server, while the read database is managed through Elasticsearch. Kafka is used to sync data between both databases.

The frontend is built using **React** and communicates with the backend microservices developed in **.NET 9.0**.

## Architecture

### Backend

The backend architecture follows the CQRS pattern and Clean Architecture, divided into the following microservices:

- **Command API** (Write):
  - Responsible for handling write operations to the SQL Server database.
  - Communicates with Kafka to sync data with the read database (Elasticsearch).
  
- **Query API** (Read):
  - Handles queries to the Elasticsearch database.
  - Uses Kafka to receive updates from the write database.

### Frontend

- **React**:
  - Frontend application that communicates with the backend microservices.
  - Consumes RESTful APIs exposed by the backend microservices to interact with data.

### Technologies Used

- **.NET 9.0**: Framework used to build the backend application.
- **React**: JavaScript library for building the user interface (UI).
- **SQL Server**: Write database for data storage.
- **Elasticsearch**: Read database for fast queries.
- **Kafka**: Messaging system for syncing data between the write and read databases.
- **Docker**: Containerization of the application and related services.

## Project Structure


## Prerequisites

- Docker and Docker Compose installed.
- .NET SDK 9.0
- Node.js and npm

## Running the Project

To run the project, use Docker Compose. Make sure Docker is running.

### Step 1: Clone the Repository

```bash
git clone https://github.com/gitMaverick16/PermissionsApp.git 
```
### Step 2: Move to the directory

```bash
cd PermissionsApp
```

### Step 3: Build and Run the Containers(Kafka, ElasticSearch, SqlServer, etc.)
```bash
docker-compose up
```

### Step 4: Apply the migrations in the Sql Server database
#### Move to the command api
```bash
cd backend\PermissionsApp\src\Command
```
#### Apply the migration
```bash
dotnet ef database update -p PermissionsApp.Command.Infrastructure -s PermissionsApp.Command.Api
```

### Step 5: Run the backend
#### Open a new terminal at the root of the project
#### Run Command microservice
```bash
cd backend\PermissionsApp\src\Command\PermissionsApp.Command.Api
dotnet run --urls "http://localhost:5112"
```
#### Open a new terminal at the root of the project
#### Run Query microservice
```bash
cd backend\PermissionsApp\src\Query\PermissionsApp.Query.Api
dotnet run --urls "http://localhost:5043"
```
### Step 6: Run the frontend
#### Open a new terminal at the root of the project
#### Move to the frontend and execute
```bash
cd frontend\permission-crud
npm i
npm start
```

## Decisions Taken

- **CQRS Architecture**: I chose the CQRS (Command Query Responsibility Segregation) pattern for better separation of concerns. This pattern helps to scale read and write operations independently and optimize both.
  
- **Microservices**: The backend is divided into two microservices: one for writing data (Command) and another for reading data (Query). This division improves scalability and allows specialized optimization for each service.

- **Database Choice**: 
  - **SQL Server** is used for write operations (Command service) as it provides reliable transactional support.
  - **Elasticsearch** is used for read operations (Query service) as it allows fast, full-text search and is optimized for search-heavy workloads.

- **Kafka for Synchronization**: Apache Kafka is used for synchronizing data between the write (SQL Server) and read (Elasticsearch) databases, ensuring consistency across services without tight coupling between them.

- **Clean Architecture**: I followed Clean Architecture to structure the code in a way that makes it easy to test, scale, and maintain. It also enforces a clear separation of concerns between different layers (UI, application, domain, infrastructure).

## Things to Improve

- **Pagination**
- **API Documentation**
- **Database Migrations**
- **Docker for the applications**
- **CI CD Pipelines**
