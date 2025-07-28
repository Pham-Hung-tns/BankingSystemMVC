using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BankingSystem.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;

namespace BankingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly BankingDbContext _context;

        public TransactionController(BankingDbContext context)
        {
            _context = context;
        }

        // GET: api/Transaction?accountNumber=1234567890&fromDate=2024-01-01&toDate=2024-12-31
        [HttpGet]
        public async Task<IActionResult> GetTransactionHistory([FromQuery] string accountNumber, [FromQuery] DateTime fromDate, [FromQuery] DateTime toDate)
        {
            Debug.WriteLine(fromDate + " " + toDate);
            if (string.IsNullOrEmpty(accountNumber))
                return BadRequest("Account number is required.");

            if (fromDate > toDate)
                return BadRequest("FromDate must be earlier than ToDate.");

            var transactions = await _context.Transactions
                .Where(t =>
                    (t.FromAccount == accountNumber || t.ToAccount == accountNumber) &&
                    t.TransactionTime >= fromDate &&
                    t.TransactionTime <= toDate)
                .OrderByDescending(t => t.TransactionTime)
                .ToListAsync();

            return Ok(transactions);
        }
    }
}
