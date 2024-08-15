using System.Text.Json.Serialization;

namespace SampleDonations.Models
{
    public class UserDto
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? Cpf { get; set; }
        public string? Photo { get; set; }
        public DateTime DateOfBirth { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public UserPermissionTypeEnum UserPermissionType { get; set; }
    }

    public enum UserPermissionTypeEnum
    {
        USUARIO, ADMINISTRADOR
    }
}
