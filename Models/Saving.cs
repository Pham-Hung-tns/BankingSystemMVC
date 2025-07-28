using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BankingSystem.Models
{
    public class Saving
    {
        public string SavingId { get; set; }

        [Required]
        [ForeignKey("User")]
        public string AccountNumber { get; set; }
        public User User { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime MaturityDate { get; set; }

        public bool AutoRenew { get; set; }

        [ForeignKey("SavingPackages")]
        public int PackageId { get; set; }
        public SavingPackages SavingPackages { get; set; }
    }

}

