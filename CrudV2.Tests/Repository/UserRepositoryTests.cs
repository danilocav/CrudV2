using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CrudV2.Core.Entities;
using CrudV2.Data.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace CrudV2.Tests.Repository
{
    public class UserRepositoryTests : IDisposable
    {
        private readonly UserRepository _userRepository;

        public UserRepositoryTests()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json") // Certifique-se de que o arquivo appsettings.json existe com sua configuração.
                .Build();

            var serviceProvider = new ServiceCollection()
                .AddSingleton<IConfiguration>(configuration)
                .AddScoped<UserRepository>()
                .BuildServiceProvider();

            _userRepository = serviceProvider.GetRequiredService<UserRepository>();
        }

        [Fact]
        public async Task AddUserAsync_Should_AddUserToDatabase()
        {
            // Arrange
            var user = new User
            {
                Name = "Danilo Cavichioli",
                Password = "admin123",
                Email = "danilo.cavi@gmail.com"
            };

            // Act
            var newUserId = await _userRepository.AddUserAsync(user);

            // Assert
            Assert.True(newUserId > 0);
        }

        [Fact]
        public async Task GetAllUsersAsync_Should_ReturnUsers()
        {
            // Act
            var users = await _userRepository.GetAllUsersAsync();

            // Assert
            Assert.NotNull(users);
            Assert.NotEmpty(users);
        }

        public void Dispose() { }
    }
}
