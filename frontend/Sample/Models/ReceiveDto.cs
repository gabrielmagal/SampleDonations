using System.Text.Json.Serialization;
using Sample.Service;

namespace Sample.Models
{
    public class ReceiveDto
    {
        public long Id { get; set; }
        public string? Name { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TypeOfDonationEnum TypeOfDonationEnum { get; set; }

        public int Quantity { get; set; }

        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime Validity { get; set; } = DateTime.Now;

        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime DateTimeReceipt { get; set; } = DateTime.Now;

        public UserDto? User { get; set; }
        public List<UserDto?> Users { get; set; } = [];
    }
}
