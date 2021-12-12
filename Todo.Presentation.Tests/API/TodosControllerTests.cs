using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NFluent;
using Tests.Helpers.Fake;
using Todo.Domain;
using Todo.Presentation.API;
using Xunit;

namespace Todo.Presentation.Tests.API
{
    public class TodosControllerTests
    {
        private readonly TodosController _controller;

        public TodosControllerTests()
        {
            var repository = new TodoRepositoryFake();
            var logger = new Mock<ILogger<TodosController>>();
            _controller = new TodosController(repository, logger.Object);
        }

        [Fact]
        public async Task Test_GetAllTodos_Should_Return_OkResult()
        {
            // Arrange
            
            // Act
            var result = await _controller.GetAllTodos();
            
            // Assert
            Check.That(result).IsInstanceOf<OkObjectResult>();
            var okResult = result as OkObjectResult;
            Check.That(okResult).IsNotNull();
            var resultValue = okResult.Value;
            Check.That(resultValue).IsInstanceOf<List<TodoItem>>();
            var items = resultValue as List<TodoItem>;

            Check.That(items).IsNotNull();
            Check.That(items).HasSize(2);

            var firstItem = items.ElementAt(0);
            Check.That(firstItem).IsNotNull();
            Check.That(firstItem.Id).IsEqualTo(0);
            Check.That(firstItem.Title).IsEqualTo("First test");
            Check.That(firstItem.Description).IsEqualTo("First test description");
            Check.That(firstItem.IsComplete).IsFalse();
            
            var secondItem = items.ElementAt(1);
            Check.That(secondItem).IsNotNull();
            Check.That(secondItem.Id).IsEqualTo(1);
            Check.That(secondItem.Title).IsEqualTo("Second test");
            Check.That(secondItem.Description).IsEqualTo("Second test description");
            Check.That(secondItem.IsComplete).IsTrue();
        }

        [Fact]
        public async Task Test_GetTodo_With_CorrectId_Should_Return_OkObjectResult()
        {
            // Arrange
            const int correctId = 0;
            
            // Act
            var result = await _controller.GetTodo(correctId);
            
            // Assert
            Check.That(result).IsInstanceOf<OkObjectResult>();
            var okResult = result as OkObjectResult;
            Check.That(okResult).IsNotNull();
            var resultValue = okResult.Value;
            Check.That(resultValue).IsInstanceOf<TodoItem>();
            
            var item = resultValue as TodoItem;
            Check.That(item).IsNotNull();
            Check.That(item.Id).IsEqualTo(0);
            Check.That(item.Title).IsEqualTo("First test");
            Check.That(item.Description).IsEqualTo("First test description");
            Check.That(item.IsComplete).IsFalse();
        }
        
        [Fact]
        public async Task Test_GetTodo_With_IncorrectId_Should_Return_NotFoundResult()
        {
            // Arrange
            const int incorrectId = -1;
            
            // Act
            var result = await _controller.GetTodo(incorrectId);
            
            // Assert
            Check.That(result).IsInstanceOf<NotFoundResult>();
        }

        [Fact]
        public async Task Test_ModifyTodo_With_CorrectItem_Should_Return_OkObjectResult()
        {
            // Arrange
            var modifyItem = new TodoItem
            {
                Id = 0,
                Title = "New title for first todo",
                Description = "New description for first todo",
                IsComplete = true
            };
            
            // Act
            var result = await _controller.ModifyTodo(modifyItem.Id, modifyItem);
            
            // Assert
            Check.That(result).IsInstanceOf<OkObjectResult>();
            var okResult = result as OkObjectResult;
            Check.That(okResult).IsNotNull();
            var resultValue = okResult.Value;
            Check.That(resultValue).IsInstanceOf<TodoItem>();
            
            var item = resultValue as TodoItem;
            Check.That(item).IsEqualTo(modifyItem);
        }
        
        [Fact]
        public async Task Test_ModifyTodo_With_IncorrectItem_Should_Return_BadRequestResult()
        {
            // Arrange
            var modifyItem = new TodoItem
            {
                Id = -1,
                Title = "Bad",
                Description = "I'm bad",
                IsComplete = false
            };
            
            // Act
            var result = await _controller.ModifyTodo(modifyItem.Id, modifyItem);
            
            // Assert
            Check.That(result).IsInstanceOf<BadRequestResult>();
        }

        [Fact]
        public async Task Test_AddTodo_With_NewTodo_Should_Return_OkObjectResult()
        {
            // Arrange
            var newItem = new TodoItem
            {
                Title = "New todo",
                Description = "New todo description",
                IsComplete = false
            };
            
            // Act
            var result = await _controller.AddTodo(newItem);
            
            // Assert
            Check.That(result).IsInstanceOf<OkObjectResult>();
            var okResult = result as OkObjectResult;
            Check.That(okResult).IsNotNull();
            var resultValue = okResult.Value;
            Check.That(resultValue).IsInstanceOf<TodoItem>();
            
            var item = resultValue as TodoItem;
            Check.That(item).IsNotNull();
            Check.That(item.Id).IsEqualTo(2);
            Check.That(item.Title).IsEqualTo("New todo");
            Check.That(item.Description).IsEqualTo("New todo description");
            Check.That(item.IsComplete).IsFalse();
        }
        
        [Fact]
        public async Task Test_AddTodo_With_IncorrectTodo_Should_Return_BadRequestObjectResult()
        {
            // Arrange
            var newItem = new TodoItem
            {
                Title = string.Empty,
                Description = "Bad",
                IsComplete = true
            };
            
            // Act
            var result = await _controller.AddTodo(newItem);
            
            // Assert
            Check.That(result).IsInstanceOf<BadRequestObjectResult>();
            var badRequestResult = result as BadRequestObjectResult;
            Check.That(badRequestResult).IsNotNull();
            var errorMessage = badRequestResult.Value as string;
            Check.That(errorMessage).IsEqualTo("The Todo title is required");
        }
    }
}