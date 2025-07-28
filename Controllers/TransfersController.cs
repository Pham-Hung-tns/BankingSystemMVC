using Microsoft.AspNetCore.Mvc;
using BankingSystem.Dtos;
using BankingSystem.Models;

namespace BankingSystem.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class TransfersController : ControllerBase
    {
        private readonly BankingDbContext _context;

        public TransfersController(BankingDbContext context)
        {
            _context = context;
        }

        [HttpPost("internal")]
        public IActionResult InternalTransfer([FromBody] InternalTransferRequestDto request)
        {
            var fromUser = _context.Users.FirstOrDefault(u => u.AccountNumber == request.FromAccount);
            if (fromUser == null)
                return NotFound("Người gửi không tồn tại");

            if (fromUser.Balance - request.Amount < 50000)
                return BadRequest("Số dư không đủ (tối thiểu 50.000 sau khi chuyển)");

            var toUser = _context.Users
                .FirstOrDefault(u => u.AccountNumber == request.ToAccountOrPhone || u.PhoneNumber == request.ToAccountOrPhone);
            if (toUser == null)
                return NotFound("Người nhận không tồn tại");

            fromUser.Balance -= request.Amount;
            toUser.Balance += request.Amount;

            // Ghi lịch sử giao dịch
            var transaction = new Transaction
            {
                TransactionId = Guid.NewGuid(),
                FromAccount = fromUser.AccountNumber,
                ToAccount = toUser.AccountNumber,
                Amount = request.Amount,
                TransactionTime = DateTime.Now,
                Content = request.Content ?? "Chuyển khoản nội bộ",
                BalanceAfter = fromUser.Balance
            };
            _context.Transactions.Add(transaction);

            _context.SaveChanges();

            return Ok(new
            {
                Message = "Chuyển khoản nội bộ thành công",
                From = fromUser.AccountNumber,
                To = toUser.AccountNumber,
                Amount = request.Amount,
                Content = request.Content
            });
        }

        [HttpPost("external")]
        public IActionResult ExternalTransfer([FromBody] ExternalTransferRequestDto request)
        {
            var fromUser = _context.Users.FirstOrDefault(u => u.AccountNumber == request.FromAccount);
            if (fromUser == null)
                return NotFound("Người gửi không tồn tại");

            if (fromUser.Balance - request.Amount < 50000)
                return BadRequest("Số dư không đủ (tối thiểu 50.000 sau khi chuyển)");

            // Giả lập xử lý chuyển khoản liên ngân hàng
            fromUser.Balance -= request.Amount;

            // Ghi lịch sử giao dịch
            var transaction = new Transaction
            {
                TransactionId = Guid.NewGuid(),
                FromAccount = fromUser.AccountNumber,
                ToAccount = request.ToAccount,
                Amount = request.Amount,
                TransactionTime = DateTime.Now,
                Content = request.Content ?? "Chuyển khoản liên ngân hàng",
                BalanceAfter = fromUser.Balance
            };
            _context.Transactions.Add(transaction);

            _context.SaveChanges();

            return Ok(new
            {
                Message = "Chuyển khoản liên ngân hàng thành công",
                From = fromUser.AccountNumber,
                To = request.ToAccount,
                BankCode = request.ToBankCode,
                Amount = request.Amount,
                Content = request.Content
            });
        }
    }

}
