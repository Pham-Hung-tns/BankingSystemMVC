namespace BankingSystem.Dtos
{
    public class OpenSavingRequest
    {
        public string AccountNumber { get; set; }
        public decimal Amount { get; set; }
        public int TermMonths { get; set; }
        public bool AutoRenew { get; set; }
    }
}
