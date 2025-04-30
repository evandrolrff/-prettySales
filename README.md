## Pretty Sales
### Description
This is the first version of the project. In this release, you'll find basic functionality for managing users, products, sales, and payments, along with a connection to an SQLite database.

## Features
- **User CRUD**: Create, Read, Update, and Delete operations for managing user records.
- **Product CRUD**: Create, Read, Update, and Delete operations for managing product records.
- **Sales CRUD**: Manage sales transactions with full Create, Read, Update, and Delete functionality.
- **Payments CRUD**: Record and manage payment information for each sale.
- **SQLite Connection**: Integration with SQLite for database storage and management.


## Getting Started
To get started with this project, follow the instructions below:

### Prerequisites
Make sure you have the following installed on your machine:

- [.NET SDK](https://dotnet.microsoft.com/download) (version 8.0 or higher)
- SQLite or an SQLite client (for database management)
- Visual Studio or your preferred C# development environment.

### Installation
1. Clone the repository:
```bash
git clone https://github.com/yourusername/project-name.git
```
2. Navigate to the project folder:
```bash
cd project-name
```
3.Restore the required packages:
```bash
dotnet restore
```
4. Build the project:
```bash
dotnet build
```
5. Run the application:
```bash
dotnet run
```
### Database Setup
This project uses an SQLite database to store data for the user and product records. The database will be automatically created when the application runs for the first time. If you wish to configure it manually, you can use the following connection string:
```txt
Data Source=your_database.db;
```
You can also manage the database schema and records directly using an SQLite client.

### Usage
Once the application is running, you can perform CRUD operations for both Users and Products through the provided user interface.

### User Operations:
- **Create User**: Add new users to the system.
- **Read User**: View existing users.
- **Update User**: Modify user details.
- **Delete User**: Remove users from the system.
### Product Operations:
- **Create Product**: Add new products to the system.
- **Read Product**: View existing products.
- **Update Product**: Modify product details.
- **Delete Product**: Remove products from the system.
### Sales Operations:
- **Create Sale**: Register a new sale transaction.
- **Read Sale**: View existing sales.
- **Update Sale**: Edit details of a sale.
- **Delete Sale**: Remove a sale record.
### Payment Operations:
- **Create Payment**: Record a payment for a sale.
- **Read Payment**: View payment history.
- **Update Payment**: Modify payment details.
- **Delete Payment**: Remove a payment record.

### Preview
![App demo](docs/Animação.gif)

### Technologies Used
- .NET 8.0
- C#
- SQLite
- Entity Framework Core (se for o caso)

### Contributing
Contributions are welcome! To contribute:
1. Fork the repository
2. Create a new branch (`git checkout -b feature/your-feature`)
3. Commit your changes (`git commit -m 'Add your message'`)
4. Push to the branch (`git push origin feature/your-feature`)
5. Open a Pull Request

### License
This project is licensed under the MIT License - see the LICENSE file for details.

![.NET](https://img.shields.io/badge/.NET-8.0-blue)
![License: Apache](https://img.shields.io/badge/License-Apache-yellow.svg)
