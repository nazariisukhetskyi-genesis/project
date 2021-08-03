using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project_CSharp.Repositories.Interfaces;
using System.Threading.Tasks;

namespace Project_CSharp.Controllers
{
    [Route("api/crypto")]
    [ApiController]
    [Authorize]
    public class CryptoController : ControllerBase
    {
        private readonly ICryptoRepository repository;

        public CryptoController(ICryptoRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet("/btcRate")]
        public async Task<IActionResult> GetBtcRate()
        {
            return Ok(await repository.GetBtcRateAsync());
        }
    }
}
