namespace Sample.Models
{
    public class SendDto
    {
        public long Id { get; set; }
        public ReceiveDto? Receive { get; set; }
        public List<ReceiveDto?> Receives { get; set; } = [];
        public UserDto? User { get; set; }
        public List<UserDto?> Users { get; set; } = [];
        public int Quantity { get; set; }
        public DateTime DateTimeDonation { get; set; } = DateTime.Now;
    }
}
