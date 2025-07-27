namespace BankingSystem.Dtos
{
    public class TransactionDto
    {
       
            public DateTime TransactionDate { get; set; }
            public string FromAccount { get; set; }
            public string ToAccount { get; set; }
            public decimal Amount { get; set; }
            public string Content { get; set; }
            public string TransactionType { get; set; } // Internal hoặc External
        

    }
}
