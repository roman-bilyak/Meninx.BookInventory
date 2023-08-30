using Meninx.BookInventory.App.Controllers;
using Meninx.BookInventory.App.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace Meninx.BookInventory.Tests
{
    [TestClass]
    public class BooksControllerTests
    {
        private Mock<IRepository<Book>> _mockBookRepository;
        private Mock<IRepository<Category>> _mockCategoryRepository;
        private BooksController _controller;

        [TestInitialize]
        public void Initialize()
        {
            _mockBookRepository = new Mock<IRepository<Book>>();
            _mockCategoryRepository = new Mock<IRepository<Category>>();
            _controller = new BooksController(_mockBookRepository.Object, _mockCategoryRepository.Object);
        }

        [TestMethod]
        public async Task GetBooksAsync_WhenNoBooks_ReturnEmptyList()
        {
            // Arrange
            GetBooksRequest request = new GetBooksRequest
            {
                Query = "query",
                Limit = 10,
                Offset = 0,
                SortBy = "Title",
                SortOrder = "Asc"
            };

            List<Book> expectedBooks = new List<Book>();

            _mockBookRepository.Setup(x => x.ListAsync(It.IsAny<ISpecification<Book>>(), default))
                .Callback((ISpecification<Book> spec, CancellationToken cancellationToken) =>
                {
                    Assert.AreEqual(request.Query, spec.Query);
                    Assert.AreEqual(request.Limit, spec.Limit);
                    Assert.AreEqual(request.Offset, spec.Offset);
                    Assert.AreEqual(request.SortBy, spec.SortBy);
                    Assert.AreEqual(request.SortOrder, spec.SortOrder);
                })
                .ReturnsAsync(expectedBooks);

            // Act
            IHttpActionResult result = await _controller.GetBooksAsync(request);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType<JsonResult<IEnumerable<BookDto>>>(result);

            IEnumerable<BookDto> dtos = (result as JsonResult<IEnumerable<BookDto>>).Content;
            Assert.AreEqual(0, dtos.Count());
        }

        [TestMethod]
        public async Task GetBooksAsync_WhenNotEmpty_ReturnListOfDtos()
        {
            // Arrange
            var request = new GetBooksRequest
            {
                Query = "query",
                Limit = 10,
                Offset = 0,
                SortBy = "Title",
                SortOrder = "Asc"
            };

            List<Book> expectedBooks = new List<Book>
            {
                new Book
                {
                    Id = Guid.NewGuid(),
                    Title = "Book 1",
                    Author = "Author 1",
                    ISBN = "123456789",
                    PublicationYear = 2022,
                    Quantity = 5,
                    CategoryId = Guid.NewGuid()
                },
                new Book
                {
                    Id = Guid.NewGuid(),
                    Title = "Book 2",
                    Author = "Author 2",
                    ISBN = "987654321",
                    PublicationYear = 2021,
                    Quantity = 10,
                    CategoryId = Guid.NewGuid()
                }
            };

            _mockBookRepository.Setup(x => x.ListAsync(It.IsAny<ISpecification<Book>>(), default))
                .Callback((ISpecification<Book> spec, CancellationToken cancellationToken) =>
                {
                    Assert.AreEqual(request.Query, spec.Query);
                    Assert.AreEqual(request.Limit, spec.Limit);
                    Assert.AreEqual(request.Offset, spec.Offset);
                    Assert.AreEqual(request.SortBy, spec.SortBy);
                    Assert.AreEqual(request.SortOrder, spec.SortOrder);
                })
                .ReturnsAsync(expectedBooks);

            // Act
            IHttpActionResult result = await _controller.GetBooksAsync(request);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType<JsonResult<IEnumerable<BookDto>>>(result);

            IEnumerable<BookDto> dtos = (result as JsonResult<IEnumerable<BookDto>>).Content;
            Assert.AreEqual(2, dtos.Count());
        }

        [TestMethod]
        public async Task GetBooksAsync_WhenRepositoryFails_ReturnError()
        {
            // Arrange
            var request = new GetBooksRequest
            {
                Query = "some query",
                Limit = 10,
                Offset = 0,
                SortBy = "Title",
                SortOrder = "Asc"
            };

            _mockBookRepository.Setup(repo => repo.ListAsync(It.IsAny<ISpecification<Book>>(), default))
                             .ThrowsAsync(new Exception("Internal server exception"));

            // Act
            IHttpActionResult result = await _controller.GetBooksAsync(request);

            // Assert
            Assert.IsInstanceOfType<ExceptionResult>(result);
        }

        [TestMethod]
        public async Task GetBookAsync_WhenBookExists_ReturnDto()
        {
            // Arrange
            Guid bookId = Guid.NewGuid();
            Book expectedBook = new Book
            {
                Id = bookId,
                Title = "Book 1",
                Author = "Author 1",
                ISBN = "123456789",
                PublicationYear = 2023,
                Quantity = 5,
                CategoryId = Guid.NewGuid()
            };

            _mockBookRepository.Setup(x => x.SingleOrDefaultAsync(bookId, It.IsAny<CancellationToken>()))
                             .ReturnsAsync(expectedBook);

            // Act
            IHttpActionResult result = await _controller.GetBookAsync(bookId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType<JsonResult<BookDto>>(result);

            BookDto dto = (result as JsonResult<BookDto>).Content;
            Assert.AreEqual(expectedBook.Id, dto.Id);
            Assert.AreEqual(expectedBook.Title, dto.Title);
            Assert.AreEqual(expectedBook.Author, dto.Author);
            Assert.AreEqual(expectedBook.ISBN, dto.ISBN);
            Assert.AreEqual(expectedBook.PublicationYear, dto.PublicationYear);
            Assert.AreEqual(expectedBook.Quantity, dto.Quantity);
            Assert.AreEqual(expectedBook.CategoryId, dto.CategoryId);
        }

        [TestMethod]
        public async Task GetBookAsync_WhenBookDoesNotExist_ReturnNotFound()
        {
            // Arrange
            Guid nonExistentBookId = Guid.NewGuid();

            _mockBookRepository.Setup(repo => repo.SingleOrDefaultAsync(nonExistentBookId, default))
                             .ReturnsAsync((Book)null);

            // Act
            IHttpActionResult result = await _controller.GetBookAsync(nonExistentBookId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType<NotFoundResult>(result);
        }

        [TestMethod]
        public async Task GetBookAsync_WhenRepositoryFails_ReturnError()
        {
            // Arrange
            Guid bookId = Guid.NewGuid();

            _mockBookRepository.Setup(repo => repo.SingleOrDefaultAsync(bookId, default))
                             .ThrowsAsync(new Exception("Internal server exception"));

            // Act
            IHttpActionResult result = await _controller.GetBookAsync(bookId);

            // Assert
            Assert.IsInstanceOfType<ExceptionResult>(result);
        }

        [TestMethod]
        public async Task PostBookAsync_WithValidData_ReturnDto()
        {
            // Arrange
            BookCreateDto bookCreateDto = new BookCreateDto
            {
                Title = "New Book",
                Author = "New Author",
                ISBN = "123456789",
                PublicationYear = "2022",
                Quantity = "5",
                CategoryId = Guid.NewGuid()
            };

            Book expectedBook = new Book
            {
                Id = Guid.NewGuid(),
                Title = bookCreateDto.Title,
                Author = bookCreateDto.Author,
                ISBN = bookCreateDto.ISBN,
                PublicationYear = int.Parse(bookCreateDto.PublicationYear),
                Quantity = int.Parse(bookCreateDto.Quantity),
                CategoryId = bookCreateDto.CategoryId.GetValueOrDefault()
            };

            _mockCategoryRepository.Setup(repo => repo.SingleOrDefaultAsync(bookCreateDto.CategoryId.Value, It.IsAny<CancellationToken>()))
                                  .ReturnsAsync(new Category 
                                  { 
                                      Id = bookCreateDto.CategoryId.Value,
                                      Name = "Category Name",
                                      Description = "Category Description"
                                  });

            _mockBookRepository.Setup(repo => repo.AddAsync(It.IsAny<Book>(), It.IsAny<CancellationToken>()))
                             .Callback((Book book, CancellationToken cancellationToken) =>
                             {
                                 Assert.AreEqual(expectedBook.Title, book.Title);
                                 Assert.AreEqual(expectedBook.Author, book.Author);
                                 Assert.AreEqual(expectedBook.ISBN, book.ISBN);
                                 Assert.AreEqual(expectedBook.PublicationYear, book.PublicationYear);
                                 Assert.AreEqual(expectedBook.Quantity, book.Quantity);
                                 Assert.AreEqual(expectedBook.CategoryId, book.CategoryId);
                             })
                             .ReturnsAsync(expectedBook);

            // Act
            IHttpActionResult result = await _controller.PostBookAsync(bookCreateDto);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType<JsonResult<BookDto>>(result);

            BookDto dto = (result as JsonResult<BookDto>).Content;
            Assert.AreEqual(expectedBook.Id, dto.Id);
            Assert.AreEqual(expectedBook.Title, dto.Title);
            Assert.AreEqual(expectedBook.Author, dto.Author);
            Assert.AreEqual(expectedBook.ISBN, dto.ISBN);
            Assert.AreEqual(expectedBook.PublicationYear, dto.PublicationYear);
            Assert.AreEqual(expectedBook.Quantity, dto.Quantity);
            Assert.AreEqual(expectedBook.CategoryId, dto.CategoryId);
        }

        [TestMethod]
        public async Task PostBookAsync_WithInvalidCategory_ReturnBadRequest()
        {
            // Arrange
            BookCreateDto bookCreateDto = new BookCreateDto
            {
                Title = "New Book",
                Author = "New Author",
                ISBN = "123456789",
                PublicationYear = "2022",
                Quantity = "5",
                CategoryId = Guid.NewGuid()
            };

            _mockCategoryRepository.Setup(repo => repo.SingleOrDefaultAsync(bookCreateDto.CategoryId.Value, It.IsAny<CancellationToken>()))
                                  .ReturnsAsync((Category)null);

            // Act
            IHttpActionResult result = await _controller.PostBookAsync(bookCreateDto);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType<InvalidModelStateResult>(result);
        }

        [TestMethod]
        public async Task PostBookAsync_WhenRepositoryFails_ReturnError()
        {
            // Arrange
            BookCreateDto bookCreateDto = new BookCreateDto
            {
                Title = "New Book",
                Author = "New Author",
                ISBN = "123456789",
                PublicationYear = "2022",
                Quantity = "5",
                CategoryId = Guid.NewGuid()
            };

            _mockCategoryRepository.Setup(repo => repo.SingleOrDefaultAsync(bookCreateDto.CategoryId.Value, It.IsAny<CancellationToken>()))
                                  .ReturnsAsync(new Category
                                  {
                                      Id = bookCreateDto.CategoryId.Value,
                                      Name = "Category Name",
                                      Description = "Category Description"
                                  });

            _mockBookRepository.Setup(repo => repo.AddAsync(It.IsAny<Book>(), It.IsAny<CancellationToken>()))
                             .ThrowsAsync(new Exception("Internal server exception"));

            // Act
            IHttpActionResult result = await _controller.PostBookAsync(bookCreateDto);

            // Assert
            Assert.IsInstanceOfType<ExceptionResult>(result);
        }
    }
}