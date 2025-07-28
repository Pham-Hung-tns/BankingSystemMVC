namespace BankingSystem.Dtos
{
    public class InternalTransferRequestDto
    {
        public string FromAccount { get; set; }
        public string ToAccountOrPhone { get; set; }
        public decimal Amount { get; set; }
        public string Content { get; set; }
    }
}
