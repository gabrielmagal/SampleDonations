using System.Text.Json.Serialization;

namespace Sample.Models
{
    public class UserDto
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? Cpf { get; set; }
        public string? Photo { get; set; }
        public DateOnly DateOfBirth { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public UserPermissionTypeEnum? UserPermissionTypeEnum { get; set; } = null;
    }
}
