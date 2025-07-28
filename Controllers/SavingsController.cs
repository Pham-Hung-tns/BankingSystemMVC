using BankingSystem.Dtos;
using BankingSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace YourNamespace.Controllers
{
    [ApiController]
    [Route("api/savings")]
    public class SavingsController : ControllerBase
    {
        private readonly BankingDbContext _context;

        public SavingsController(BankingDbContext context)
        {
            _context = context;
        }

        [HttpPost("open")]
        public async Task<IActionResult> OpenSaving([FromBody] OpenSavingRequest request)
        {
            // Các kỳ hạn hợp lệ
            int[] validTerms = { 1, 2, 3, 6, 9, 12, 18, 24, 36 };

            // Validate kỳ hạn
            if (!validTerms.Contains(request.TermMonths))
            {
                return BadRequest(new { status = "FAIL", message = "Kỳ hạn không hợp lệ." });
            }

            // Tìm người dùng
            var user = await _context.Users.FirstOrDefaultAsync(u => u.AccountNumber == request.AccountNumber);
            if (user == null)
            {
                return NotFound(new { status = "FAIL", message = "Không tìm thấy tài khoản." });
            }

            // Kiểm tra số dư
            if (user.Balance < request.Amount)
            {
                return BadRequest(new { status = "FAIL", message = "Không đủ số dư mở tiết kiệm." });
            }

            // Giả sử rằng cần giữ lại ít nhất 50,000 VND sau khi mở tiết kiệm
            if (user.Balance - request.Amount < 50000)
            {
                return BadRequest(new { status = "FAIL", message = "Số tiền sau mở tiết kiệm < 50000 đ." });
            }

            // Tìm gói tiết kiệm phù hợp
            var savingPackage = await _context.SavingPackages.FirstOrDefaultAsync(p => p.DurationInMonths == request.TermMonths);
            if (savingPackage == null)
            {
                return NotFound(new { status = "FAIL", message = "Không tìm thấy gói tiết kiệm phù hợp." });
            }

            // Tạo mã SavingId tự động (giả sử theo format SAVxxxx)
            var lastSaving = await _context.Savings.OrderByDescending(s => s.SavingId).FirstOrDefaultAsync();
            int nextId = 1;
            if (lastSaving != null && lastSaving.SavingId.StartsWith("SAV"))
            {
                int.TryParse(lastSaving.SavingId.Substring(3), out nextId);
                nextId += 1;
            }
            string newSavingId = $"SAV{nextId:D4}";

            var startDate = DateTime.Now;
            var maturityDate = startDate.AddMonths(request.TermMonths);

            // Tạo saving mới
            var saving = new Saving
            {
                SavingId = newSavingId,
                AccountNumber = request.AccountNumber,
                Amount = request.Amount,
                StartDate = startDate,
                MaturityDate = maturityDate,
                AutoRenew = request.AutoRenew,
                PackageId = savingPackage.PackageId
            };

            // Trừ tiền khỏi tài khoản
            user.Balance -= request.Amount;

            // Lưu dữ liệu
            _context.Savings.Add(saving);
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                status = "SUCCESS",
                savingId = newSavingId,
                termMonths = savingPackage.DurationInMonths,
                interestRate = savingPackage.InterestRate,
                startDate = startDate.ToString("yyyy-MM-dd"),
                maturityDate = maturityDate.ToString("yyyy-MM-dd")
            });
        }
        [HttpGet("rates")]
        public async Task<IActionResult> GetSavingRates()
        {
            var rates = await _context.SavingPackages
                .Select(sp => new {
                    termMonths = sp.DurationInMonths,
                    interestRate = sp.InterestRate
                })
                .OrderBy(sp => sp.termMonths)
                .ToListAsync();

            return Ok(rates);
        }

    }

    // DTO để nhận dữ liệu từ request
    //public class OpenSavingRequest
    //{
    //    public string AccountNumber { get; set; }
    //    public decimal Amount { get; set; }
    //    public int TermMonths { get; set; }
    //    public bool AutoRenew { get; set; }
    //}
}
