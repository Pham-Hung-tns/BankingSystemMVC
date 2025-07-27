using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankingSystem.Models
{
    public class Transaction
    {
        [Key]
        public Guid TransactionId { get; set; }

        [Required]
        [ForeignKey("FromUser")]
        public string FromAccount { get; set; }
        public User FromUser { get; set; }

        [Required]
        [ForeignKey("ToUser")]
        public string ToAccount { get; set; }
        public User ToUser { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Amount { get; set; }

        [Required]
        public DateTime TransactionTime { get; set; } = DateTime.Now;

        public string Content { get; set; }

        public decimal BalanceAfter { get; set; }// Số dư sau giao dịch
    }
}
