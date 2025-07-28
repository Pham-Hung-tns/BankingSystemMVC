namespace BankingSystem.Dtos
{
    public class ExternalTransferRequestDto
    {
        public string FromAccount { get; set; }
        public string ToBankCode { get; set; }
        public string ToAccount { get; set; }
        public decimal Amount { get; set; }
        public string Content { get; set; }
    }
}
