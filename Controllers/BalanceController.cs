using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Wallet.API.Domain.Models;
using Wallet.API.Domain.Models.Queries;
using Wallet.API.Domain.Services;
using Wallet.API.Resources;

namespace Wallet.API.Controllers
{
    [Route("/api/balance")]
    [Produces("application/json")]
    [ApiController]
    public class BalanceController : Controller
    {
        private readonly IBalanceService _balanceService;
        private readonly IMapper _mapper;

        public BalanceController(IBalanceService balanceService, IMapper mapper)
        {
            _balanceService = balanceService;
            _mapper = mapper;
        }

        /// <summary>
        /// Lists players balances.
        /// </summary>
        /// <returns>List of players balances.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<BalanceResource>), 200)]
        public async Task<IEnumerable<BalanceResource>> ListAsync([FromQuery] PlayersBalanceQueryResource query)
        {
            var playersBalanceQuery = _mapper.Map<PlayersBalanceQueryResource, BalanceQuery>(query);

            var balances = await _balanceService.ListAsync(playersBalanceQuery);

            var resource = _mapper.Map<IEnumerable<Balance>, IEnumerable<BalanceResource>>(balances);

            return resource;
        }

        /// <summary>
        /// Creates new players wallet.
        /// </summary>
        /// <param name="resource">Players wallet.</param>
        /// <returns>Response for the request.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(WalletResource), 201)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> PostAsync([FromBody] CreateWalletResource resource)
        {
            var balance = _mapper.Map<CreateWalletResource, Balance>(resource);
            var result = await _balanceService.SaveAsync(balance);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            return Ok(new WalletResource { Id = resource.PlayerId, Asset = resource.Asset });
        }
    }
}