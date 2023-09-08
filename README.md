# Book Inventory

Test task for one company when hiring. Probably not a project to be proud of, just saved for history.

## Requirements

Develop a simple web application for managing a library's book inventory targeting the .NET Framework 4.8. The application should allow users to view, add, edit, and delete books.

1. Create a database schema using Entity Framework Code First approach with the following entities:
   • Book: Contains fields for Title, Author, ISBN, Publication Year, and Quantity.
   • Category: Contains fields for Category Name and Description.
   • The Book entity should have a foreign key relationship with the Category entity.
2. Implement a data access layer using Entity Framework Code First to perform CRUD (Create, Read, Update, Delete) operations on the Book and Category entities. To get all books, create a stored procedure that supports pagination, full-text search, and sorting (asc, desc) by any of the existing column.
3. Create an ASP.NET WebForms application with the following pages:
   • Home Page: Display a list of books with their details (Title, Author, ISBN, Publication Year, Quantity) in a tabular format. NOTE: There is no need to implement pagination here, just display all the books.
   • Add Book Page: Form to add a new book to the database. Include fields for Title, Author, ISBN, Publication Year, Quantity, and a dropdown to select the book's category.
   • Edit Book Page: Form to edit an existing book's details.
   • Delete Book Page: Confirmation page to delete a book from the database.
4. Implement an ASP.NET Web API with the following endpoints:
   • GET api/books: Returns a JSON response containing a list of all books. NOTE: use
   pagination, full-text search, sorting.
   • GET api/books/{id}: Returns a JSON response containing the details of a specific book.
   • POST api/books: Accepts JSON data to create a new book.
   • PUT api/books/{id}: Accepts JSON data to update the details of a specific book.
   • DELETE api/books/{id}: Deletes a specific book.
5. Write unit tests using MSTest to test the functionality of the data access layer, API endpoints, and any other critical components.
6. Use MS SQL Server as the database.
7. Provide instructions how to run the project.



## Guidelines for launching the application:

1. Launch Visual Studio and open the project solution file (Meninx.BookInventory.sln).
2. Set Meninx.BookInventory.App as startup project.
3. Modify the connection string within the Meninx.BookInventory.App/web.config file (name="BookInventoryDbContext") to establish a Microsoft SQL Server database connection.
   Note: This instance must support full-text search feature. 'LocalDb' instance is not suitable.
4. Execute the build process to restore important NuGet packages and compile the application.
5. Initiate application deployment by pressing F5.
6. The program will automatically apply the migration scripts and immediately load into your default web browser.

## Feedback

Feedback is negative: 

*The infrastructure and basic functions are confusing. One time you have BookRepository, another time you use IRepository<Category>, IReadRepository<Category>. It took 8 lines to register all possible combinations of repositories. The stored procedure uses string concatenation to get all the books. In WebForms, an exception is thrown when adding/removing books (incorrect use of async/await). When the API returns the pagination result, there is no information about the total number of items that is important, otherwise you don’t know if you can load the next page or not.*