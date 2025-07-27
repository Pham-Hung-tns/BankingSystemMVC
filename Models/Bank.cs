using System.ComponentModel.DataAnnotations;

namespace BankingSystem.Models
{
    public class Bank
    {

        [Key]
        public int BankId { get; set; }

        [Required]
        public string BankName { get; set; }

        [Required]
        public string BankCode { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
