## Pretty Sales
### Description
This is the first version of the project. In this release, you'll find basic functionality for managing users and products, along with a connection to an SQLite database.

## Features
- **User CRUD**: Create, Read, Update, and Delete operations for managing user records.
- **Product CRUD**: Create, Read, Update, and Delete operations for managing product records.
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

### Contributing
Feel free to fork the repository, submit issues, or send pull requests for any improvements or bug fixes.

### License
This project is licensed under the MIT License - see the LICENSE file for details.
