using System.ComponentModel.DataAnnotations;

namespace BankingSystem.Models
{
    public class SavingPackages
    {
        [Key]
        public int PackageId { get; set; }

        [Required]
        public int DurationInMonths { get; set; }

        [Required]
        public decimal InterestRate { get; set; }  // Ví dụ: 3.5 nghĩa là 3.5%

        public ICollection<Saving> Savings { get; set; }
    }
}
