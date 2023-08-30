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

            _mockBookRepository.Setup(repo => repo.ListAsync(It.IsAny<ISpecification<Book>>(), default))
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
            Assert.IsInstanceOfType(result, typeof(JsonResult<IEnumerable<BookDto>>));

            var jsonResult = (JsonResult<IEnumerable<BookDto>>)result;
            Assert.AreEqual(0, jsonResult.Content.Count());
        }

        [TestMethod]
        public async Task GetBooksAsync_Should_ReturnListOfBooks()
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

            _mockBookRepository.Setup(repo => repo.ListAsync(It.IsAny<ISpecification<Book>>(), default))
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
            Assert.IsInstanceOfType(result, typeof(JsonResult<IEnumerable<BookDto>>));

            var jsonResult = (JsonResult<IEnumerable<BookDto>>)result;
            Assert.AreEqual(2, jsonResult.Content.Count());
        }
    }
}