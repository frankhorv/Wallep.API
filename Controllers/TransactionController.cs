using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Wallet.API.Domain.Models;
using Wallet.API.Domain.Services;
using Wallet.API.Domain.Services.Communication;
using Wallet.API.Resources;

namespace Wallet.API.Controllers
{
    [Route("/api/transaction")]
    [Produces("application/json")]
    [ApiController]
    public class TransactionController : Controller
    {
        private readonly ITransactionService _transactionService;
        private readonly IMapper _mapper;

        public TransactionController(ITransactionService transactionService, IMapper mapper)
        {
            _transactionService = transactionService;
            _mapper = mapper;
        }

        /// <summary>
        /// Credits existing player balance.
        /// </summary>
        /// <param name="resource">Credit transaction.</param>
        /// <returns>Response for the request.</returns>
        [HttpPut]
        [ProducesResponseType(typeof(TransactionStatistics), 201)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> PutAsync([FromBody] SaveTransactionResource resource)
        {
            var transactions = _mapper.Map<IList<TransactionResource>, IList<Transaction>>(resource.Transactions);
            var result = await _transactionService.SaveAsync(resource.PlayerId, transactions);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            return Ok(result.Resource);
        }
    }
}