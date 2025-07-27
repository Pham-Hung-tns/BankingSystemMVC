namespace BankingSystem.Dtos
{
    public class RegisterAccountDto
    {
        public string AccountNumber { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string NationalId { get; set; }
        public DateTime NationalExpiry { get; set; }
        public decimal Balance { get; set; }
    }
}
