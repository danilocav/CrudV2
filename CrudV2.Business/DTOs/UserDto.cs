namespace CrudV2.Business.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }

        // Construtor vazio para serialização/desserialização
        public UserDto() { }

        // Construtor para facilitar a criação do DTO a partir de uma entidade de domínio
        public UserDto(int id, string username, string email)
        {
            Id = id;
            Name = username;
            Email = email;
            // Atribua outras propriedades conforme necessário
        }
    }
}
