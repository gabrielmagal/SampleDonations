using System.Text.Json.Serialization;

namespace SampleDonations.Models
{
    public class ReceiveDto
    {
        public long Id { get; set; }
        public string? Name { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TypeOfDonation TypeOfDonation { get; set; }

        public int Quantity { get; set; }
        public DateTime Validity { get; set; }
        public DateTime DateTimeReceipt { get; set; }
    }

    public enum TypeOfDonation
    {
        COMESTIVEL, LIMPEZA, ROUPA, COBERTOR, DINHEIRO
    }
}
