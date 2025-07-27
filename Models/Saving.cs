using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankingSystem.Models
{
    public class Saving
    {
        [Key]
        public Guid SavingId { get; set; }

        [Required]
        [ForeignKey("User")]
        public string AccountNumber { get; set; }
        public User User { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public DateTime StartDate { get; set; } = DateTime.Now;

        [ForeignKey("SavingPackage")]
        public int PackageId { get; set; }
        public SavingPackages SavingPackages { get; set; }
    }
}
