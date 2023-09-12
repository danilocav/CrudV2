using CrudV2.Business.DTOs;
using System.Text;
using System.Text.Json;

namespace CrudV2.Client.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://localhost:5001/api/user"; // Substitua pela URL correta da sua API

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<UserDto>>("api/users");
        }

        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            var response = await _httpClient.GetStreamAsync($"{BaseUrl}/{id}");
            return await JsonSerializer.DeserializeAsync<UserDto>(response);
        }

        public async Task<int> AddUserAsync(UserDto userDto)
        {
            var userJson = JsonSerializer.Serialize(userDto);
            var content = new StringContent(userJson, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{BaseUrl}", content);

            response.EnsureSuccessStatusCode(); // Verifica se a resposta é bem-sucedida

            // Retorna o ID do novo usuário criado
            return int.Parse(await response.Content.ReadAsStringAsync());
        }

        public async Task<bool> UpdateUserAsync(UserDto userDto)
        {
            var userJson = JsonSerializer.Serialize(userDto);
            var content = new StringContent(userJson, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{BaseUrl}/{userDto.Id}", content);

            return response.IsSuccessStatusCode; // Retorna true se a atualização for bem-sucedida
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/{id}");

            return response.IsSuccessStatusCode; // Retorna true se a exclusão for bem-sucedida
        }
    }
}
