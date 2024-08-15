namespace SampleDonations.Models
{
    public class SendDto
    {
        public long Id { get; set; }
        public long IdProduct { get; set; }
        public long IdUser { get; set; }
        public int Quantity { get; set; }
        public DateTime DateTimeDonation { get; set; }
    }
}
