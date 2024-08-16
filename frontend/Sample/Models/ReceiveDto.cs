using System.Text.Json.Serialization;

namespace Sample.Models
{
    public class ReceiveDto
    {
        public long Id { get; set; }
        public string? Name { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TypeOfDonationEnum TypeOfDonation { get; set; }

        public int Quantity { get; set; }
        public DateTime Validity { get; set; }
        public DateTime DateTimeReceipt { get; set; }
    }
}
