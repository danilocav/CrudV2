using CrudV2.Business.DTOs;
using CrudV2.Business.Interfaces;
using CrudV2.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CrudV2.Tests.Api
{
    public class UserRepositoryTests
    {
        [Fact]
        public async Task GetAllUsers_ReturnsOkResult()
        {
            // Arrange
            var userUseCasesMock = new Mock<IUserUseCases>();
            var controller = new UserController(userUseCasesMock.Object);

            // Defina aqui a lista de usuários de exemplo que você espera que seja retornada
            var expectedUsers = new List<UserDto>
            {
                new UserDto { Id = 1, Name = "User 1", Email = "danilo@gmail.com" },
                new UserDto { Id = 2, Name = "User 2", Email = "danilo2@gmail.com" }
            };

            // Configure o comportamento do mock para retornar a lista de usuários
            userUseCasesMock.Setup(u => u.GetAllUsersAsync()).ReturnsAsync(expectedUsers);

            // Act
            var result = await controller.GetAllUsers();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualUsers = Assert.IsAssignableFrom<IEnumerable<UserDto>>(okResult.Value);
            Assert.Equal(expectedUsers.Count, actualUsers.Count());

            // Verifique se os detalhes dos usuários correspondem ao esperado
            // Isso depende da sua implementação específica do método GetAllUsersAsync
            // Você pode adicionar mais verificações conforme necessário
        }

        [Fact]
        public async Task UpdateUser_ValidId_ReturnsNoContent()
        {
            // Arrange
            var userDto = new UserDto { Id = 1, Name = "Danilo", Password = "danilo123", Email = "danilo@gmail.com" };
            var userUseCasesMock = new Mock<IUserUseCases>();
            userUseCasesMock.Setup(x => x.UpdateUserAsync(userDto)).ReturnsAsync(true);
            var controller = new UserController(userUseCasesMock.Object);

            // Act
            var result = await controller.UpdateUser(1, userDto);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdateUser_InvalidId_ReturnsBadRequest()
        {
            // Arrange
            var userDto = new UserDto { Id = 1, Name = "Danilo", Password = "danilo123", Email = "danilo@gmail.com" };
            var userUseCasesMock = new Mock<IUserUseCases>();
            var controller = new UserController(userUseCasesMock.Object);

            // Act
            var result = await controller.UpdateUser(2, userDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("O ID do usuário na URL não corresponde ao ID do usuário no corpo da solicitação.", badRequestResult.Value);
        }

        [Fact]
        public async Task DeleteUser_ValidId_ReturnsNoContent()
        {
            // Arrange
            var userUseCasesMock = new Mock<IUserUseCases>();
            userUseCasesMock.Setup(x => x.DeleteUserAsync(1)).ReturnsAsync(true);
            var controller = new UserController(userUseCasesMock.Object);

            // Act
            var result = await controller.DeleteUser(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteUser_InvalidId_ReturnsNotFound()
        {
            // Arrange
            var userUseCasesMock = new Mock<IUserUseCases>();
            var controller = new UserController(userUseCasesMock.Object);

            // Act
            var result = await controller.DeleteUser(2);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

    }
}
