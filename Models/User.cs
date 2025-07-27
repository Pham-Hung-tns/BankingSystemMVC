using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankingSystem.Models
{
    public class User
    {
        [Key]
        [Required]
        public string AccountNumber { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string NationalId { get; set; }

        [Required]
        public DateTime NationalIdExpiry { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Balance { get; set; }

        [ForeignKey("Bank")]
        public int BankId { get; set; }
        public Bank Bank { get; set; }

        // Quan hệ 1-n với transaction (sender & receiver)
        public ICollection<Transaction> TransactionsFrom { get; set; }
        public ICollection<Transaction> TransactionsTo { get; set; }

        public ICollection<Saving> Savings { get; set; }
    }
}
