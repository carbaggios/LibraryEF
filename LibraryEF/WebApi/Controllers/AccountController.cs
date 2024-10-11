using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dtos;
using WebApi.Interfaces;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AccountController : ControllerBase
    {
        private IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("RegisterReader")]
        public async Task<ActionResult<LoginDto>> RegisterReader([FromBody] RegisterDto registerDto, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _accountService.RegisterReader(registerDto, cancellationToken));
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("RegisterLibrarian")]
        public async Task<ActionResult<LoginDto>> RegisterLibrarian([FromBody] RegisterDto registerDto, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _accountService.RegisterLibrarian(registerDto, cancellationToken));
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginDto loginDto, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _accountService.Login(loginDto, cancellationToken));
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet("librarians")]
        public async Task<ActionResult> GetLibrarians(CancellationToken cancellationToken)
        {
            try
            {
                var accounts = await _accountService.GetLibrarians(cancellationToken);
                return Ok(accounts.Select(acc => new AccountDto(acc.Id, acc.Login, acc.Email)));
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet("readers")]
        public async Task<ActionResult> GetReaders(CancellationToken cancellationToken)
        {
            try
            {
                var accounts = await _accountService.GetReaders(cancellationToken);
                return Ok(accounts.Select(acc => new AccountDto(acc.Id, acc.Login, acc.Email)));
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
