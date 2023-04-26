# Microservices Architecture in .NET6 for Products, Customers, and Orders

This project is a microservices-based architecture using .NET6 for Products, Customers, and Orders. It utilizes three different databases for each microservice, including Postgres, MS-SQL Server, and MongoDB. An API gateway using Ocelot is also implemented for handling requests and routing to the appropriate microservice.

## Architecture Overview

The architecture consists of three separate microservices, each responsible for handling different aspects of the application:

- **Products Microservice**: Responsible for handling product-related operations, such as adding, retrieving, updating, and deleting products.
- **Customers Microservice**: Responsible for handling customer-related operations, such as adding, retrieving, updating, and deleting customer information.
- **Orders Microservice**: Responsible for handling order-related operations, such as placing, retrieving, and updating orders.

Each microservice utilizes its own database, including:

- **Products Microservice**: Postgres
- **Customers Microservice**: MS-SQL Server
- **Orders Microservice**: MongoDB

An API gateway using Ocelot is implemented to handle incoming requests and route them to the appropriate microservice.

## Setup

To set up and run the project, follow these steps:

1. Clone the repository
2. Set up the three different databases (Postgres, MS-SQL Server, and MongoDB) for each microservice
3. Run each microservice individually
4. Start the API gateway using Ocelot
5. Test the application by making requests through the API gateway

## Running the Application

To run the application, navigate to the root directory and execute the following command:


This will start the API gateway and all three microservices. You can then make requests to the API gateway to interact with the application.

## Technologies Used

- .NET6
- Postgres
- MS-SQL Server
- MongoDB
- Ocelot
- CQRS(MediatR)
- FluentValidation
