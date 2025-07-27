using BankingSystem.Dtos;
using BankingSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankingSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly BankingDbContext _context;

        public AccountsController(BankingDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterAccountDto dto)
        {
            // Kiểm tra trùng số tài khoản
            if (await _context.Users.AnyAsync(u => u.AccountNumber == dto.AccountNumber))
            {
                return BadRequest(new
                {
                    status = "FAIL",
                    message = "Số tài khoản đã tồn tại"
                });
            }
            // Kiểm tra CCCD trùng
            if (await _context.Users.AnyAsync(u => u.NationalId == dto.NationalId))
            {
                return BadRequest(new
                {
                    status = "FAIL",
                    message = "Căn cước công dân đã tồn tại"
                });
            }
            // Kiểm tra số dư tối thiểu (ở đây là 10.000 VND, có thể chỉnh nếu bạn cần >= 50.000)
            if (dto.Balance < 10000)
            {
                return BadRequest(new
                {
                    status = "FAIL",
                    message = "Số dư ban đầu phải từ 10.000 VND trở lên"
                });
            }

            // Kiểm tra ngày hết hạn CCCD
            if (dto.NationalExpiry <= DateTime.Today)
            {
                return BadRequest(new
                {
                    status = "FAIL",
                    message = "Căn cước công dân đã hết hạn"
                });
            }

            // Giả sử bạn mặc định người dùng thuộc ngân hàng nội bộ (BankId = 1)
            var newUser = new User
            {
                AccountNumber = dto.AccountNumber,
                FullName = dto.FullName,
                PhoneNumber = dto.PhoneNumber,
                NationalId = dto.NationalId,
                NationalIdExpiry = dto.NationalExpiry,
                Balance = dto.Balance,
                BankId = 1
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                status = "SUCCESS",
                AccountId = newUser.AccountNumber,
                message = "Tạo tài khoản thành công"
            });
        }


        [HttpGet("{accountNumber}")]
        public async Task<IActionResult> GetAccountInfo(string accountNumber)
        {
            var account = await _context.Users.FirstOrDefaultAsync(u => u.AccountNumber == accountNumber);

            if (account == null)
            {
                return NotFound(new
                {
                    status = "FAIL",
                    message = "Tài khoản không tồn tại"
                });
            }

            var response = new InforAccount
            {
                AccountNumber = account.AccountNumber,
                FullName = account.FullName,
                PhoneNumber = account.PhoneNumber,
                NationalId = account.NationalId,
                NationalExpiry = account.NationalIdExpiry,
                Balance = account.Balance
            };

            return Ok(response);
        }

    }
}
