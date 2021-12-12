using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NFluent;
using Todo.Domain;
using Xunit;

namespace Todo.Infrastructure.Tests
{
    public class TodoMockRepositoryTests
    {
        private readonly TodoMockRepository _repository;

        public TodoMockRepositoryTests()
        {
            _repository = new TodoMockRepository();
        }

        [Fact]
        public async Task Test_GetAllItemsAsync_Should_Return_Items()
        {
            // Act
            var result = await _repository.GetAllItemsAsync();
            
            // Assert
            Check.That(result).IsNotNull();
            Check.That(result).IsInstanceOf<List<TodoItem>>();
            var items = result as List<TodoItem>;

            Check.That(items).HasSize(3);
            
            var firstItem = items.ElementAt(0);
            Check.That(firstItem).IsNotNull();
            Check.That(firstItem.Id).IsEqualTo(0);
            Check.That(firstItem.Title).IsEqualTo("My first Todo item");
            Check.That(firstItem.Description).IsEqualTo("Description of the first todo");
            Check.That(firstItem.IsComplete).IsFalse();
            
            var secondItem = items.ElementAt(1);
            Check.That(secondItem).IsNotNull();
            Check.That(secondItem.Id).IsEqualTo(1);
            Check.That(secondItem.Title).IsEqualTo("Task I should do but never will");
            Check.That(secondItem.Description).IsEqualTo("This is an impossible task");
            Check.That(secondItem.IsComplete).IsFalse();
            
            var thirdItem = items.ElementAt(2);
            Check.That(thirdItem).IsNotNull();
            Check.That(thirdItem.Id).IsEqualTo(2);
            Check.That(thirdItem.Title).IsEqualTo("Already completed task");
            Check.That(thirdItem.Description).IsEqualTo("I'm the best !");
            Check.That(thirdItem.IsComplete).IsTrue();
        }

        [Fact]
        public async Task Test_GetItemByIdAsync_With_CorrectId_Should_Return_Item()
        {
            // Arrange
            const int correctId = 0;
            
            // Act
            var result = await _repository.GetItemByIdAsync(correctId);
            
            // Assert
            Check.That(result).IsNotNull();
            Check.That(result.Id).IsEqualTo(0);
            Check.That(result.Title).IsEqualTo("My first Todo item");
            Check.That(result.Description).IsEqualTo("Description of the first todo");
            Check.That(result.IsComplete).IsFalse();
        }
        
        [Fact]
        public async Task Test_GetItemByIdAsync_With_IncorrectId_Should_Return_Item()
        {
            // Arrange
            const int incorrectId = -1;
            
            // Act
            var result = await _repository.GetItemByIdAsync(incorrectId);
            
            // Assert
            Check.That(result).IsNull();
        }

        [Fact]
        public async Task Test_AddItemAsync_Should_AddItem()
        {
            // Arrange
            var addedItem = new TodoItem
            {
                Title = "Added Todo",
                Description = "This is a cool todo",
                IsComplete = false
            };
            
            // Act
            await _repository.AddItemAsync(addedItem);
            
            // Assert
            var items = await _repository.GetAllItemsAsync();
            Check.That(items).HasSize(4);
            var newItemInItems = items.ElementAt(3);
            var newItemBySearch = await _repository.GetItemByIdAsync(3);

            Check.That(newItemInItems).IsEqualTo(newItemBySearch);
        }

        [Fact]
        public async Task Test_ModifyItemAsync_With_ExistingItem_Should_Return_ModifiedItem()
        {
            // Arrange
            var modifyItem = new TodoItem
            {
                Id = 0,
                Title = "New title",
                Description = "New description",
                IsComplete = true
            };
            
            // Act
            var result = await _repository.ModifyItemAsync(modifyItem);
            
            // Assert
            Check.That(result).IsNotNull();
            Check.That(result).IsEqualTo(modifyItem);

            var modifiedItem = await _repository.GetItemByIdAsync(0);
            Check.That(modifiedItem).IsEqualTo(modifyItem);
        }
        
        [Fact]
        public async Task Test_ModifyItemAsync_With_NonExistingItem_Should_Return_Null()
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
            var result = await _repository.ModifyItemAsync(modifyItem);
            
            // Assert
            Check.That(result).IsNull();
        }

        [Fact]
        public async Task Test_DeleteItemWithIdAsync_With_CorrectId_Should_Return_True()
        {
            // Arrange
            const int correctId = 0;
            
            // Act
            var result = await _repository.DeleteItemWithIdAsync(correctId);
            
            // Assert
            Check.That(result).IsTrue();
            
            var items = await _repository.GetAllItemsAsync();
            Check.That(items).HasSize(2);

            var deletedItem = await _repository.GetItemByIdAsync(0);
            Check.That(deletedItem).IsNull();
        }
        
        [Fact]
        public async Task Test_DeleteItemWithIdAsync_With_IncorrectId_Should_Return_False()
        {
            // Arrange
            const int incorrectId = -1;
            
            // Act
            var result = await _repository.DeleteItemWithIdAsync(incorrectId);
            
            // Assert
            Check.That(result).IsFalse();
            
            var items = await _repository.GetAllItemsAsync();
            Check.That(items).HasSize(3);
        }
    }
}