using Xunit;
using Microsoft.EntityFrameworkCore;
using ContactManagerAPI.Controllers;
using ContactManagerAPI.Data;
using ContactManagerAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ContactManagerAPI.Tests
{
    public class ContactsControllerTests
    {
        [Fact]
        public void Get_ReturnsAllContacts()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ContactContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;

            using var context = new ContactContext(options);

            var controller = new ContactsController(context);

            // Act
            var result = controller.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var contacts = Assert.IsAssignableFrom<System.Collections.Generic.IEnumerable<Contact>>(okResult.Value);
            Assert.Single(contacts);  // Since we added only one contact
        }

        [Fact]
        public void Post_CreatesContact()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ContactContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;

            using var context = new ContactContext(options);
            var controller = new ContactsController(context);

            // Act
            var newContact = new Contact { Name = "Bob", Email = "bob@example.com" };
            var result = controller.Post(newContact);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal("Get", createdResult.ActionName);
        }

        [Fact]
        public void Delete_RemoveContact()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ContactContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;

            using var context = new ContactContext(options);
            var controller = new ContactsController(context);

            // Act
            var contacts = controller.Get();
            var okResult = Assert.IsType<OkObjectResult>(contacts.Result);
            var result = controller.Delete(Assert.IsAssignableFrom<System.Collections.Generic.IEnumerable<Contact>>(okResult.Value).Single().Id);

            // Assert
            var createdResult = Assert.IsType<NoContentResult>(result);
            Assert.True(createdResult.StatusCode == 204);
        }
    }
}
