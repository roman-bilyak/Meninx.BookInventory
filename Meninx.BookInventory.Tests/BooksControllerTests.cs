using Meninx.BookInventory.App.Controllers;
using Meninx.BookInventory.App.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
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
            // Initialize the mock repository and the controller
            _mockBookRepository = new Mock<IRepository<Book>>();
            _mockCategoryRepository = new Mock<IRepository<Category>>();

            _controller = new BooksController(_mockBookRepository.Object, _mockCategoryRepository.Object);
        }

        [TestMethod]
        public async Task GetBookAsync_ExistingId_ReturnsOkResultWithBookDto()
        {
            // Arrange
            Guid existingId = Guid.NewGuid();
            Book existingBook = new Book { Id = existingId, Title = "Sample Book" };
            _mockBookRepository.Setup(x => x.SingleOrDefaultAsync(existingId, default))
                           .ReturnsAsync(existingBook);

            // Act
            IHttpActionResult actionResult = await _controller.GetBookAsync(existingId);
            JsonResult<BookDto> contentResult = actionResult as JsonResult<BookDto>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(existingId, contentResult.Content.Id);
            Assert.AreEqual(existingBook.Title, contentResult.Content.Title);
        }

        [TestMethod]
        public async Task GetBookAsync_NonExistingId_ReturnsNotFoundResult()
        {
            // Arrange
            Guid nonExistingId = Guid.NewGuid();
            _mockBookRepository.Setup(repo => repo.SingleOrDefaultAsync(nonExistingId, default))
                           .ReturnsAsync((Book)null);

            // Act
            IHttpActionResult actionResult = await _controller.GetBookAsync(nonExistingId);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }
    }
}
