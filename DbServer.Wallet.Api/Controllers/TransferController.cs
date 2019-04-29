using DbServer.Wallet.Api.ViewModels.Shared;
using DbServer.Wallet.Api.ViewModels.Transfer;
using DbServer.Wallet.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DbServer.Wallet.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TransferController : ControllerBase
    {
        private readonly WalletService _walletService;

        public TransferController(WalletService walletService)
        {
            _walletService = walletService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ErrorViewModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorViewModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Post([FromBody]PostTransferViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var (status, message) = await _walletService.CreateTransferAsync(User.GetAccountId(), model);

            if (status == StatusCodes.Status200OK)
                return Ok();

            return StatusCode(status, new ErrorViewModel(message));
        }
    }
}