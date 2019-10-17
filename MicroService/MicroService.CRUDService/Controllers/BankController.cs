using System.Threading.Tasks;
using System.Transactions;
using Core.ApplicationService.Contracts;
using Core.DomainModel.Entities;
using Core.DomainService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MicroService.CRUDService
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BankController : ControllerBase
    {

        #region Properties

        private readonly IBankService _bankService;

        #endregion /Properties

        #region Constructors

        public BankController(IBankService bankService)
        {
            this._bankService = bankService;
        }

        #endregion /Constructors

        #region Actions

        // GET: api/Bank
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var banks = await this._bankService.GetAllAsync();
            return new OkObjectResult(banks);
        }

        // GET: api/Bank/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var bank = await this._bankService.GetByIdAsync(id);
            return new OkObjectResult(bank);
        }

        // POST: api/Bank
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]Bank bank)
        {
            if (bank == null)
            {
                return new NoContentResult();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            TransactionResult result;
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                result = await this._bankService.InsertAsync(bank);
                if (result.IsSuccessful)
                {
                    scope.Complete();
                    return CreatedAtAction(nameof(GetAsync), new
                    {
                        id = bank.Id
                    }, bank);
                }
            }
            return BadRequest(result.ExceptionContentResult);
        }

        // PUT: api/Bank/
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Bank bank)
        {
            if (bank == null)
            {
                return new NoContentResult();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            TransactionResult result;
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                result = await this._bankService.UpdateAsync(bank);
                if (result.IsSuccessful)
                {
                    scope.Complete();
                    return new OkResult();
                }
                else
                {
                    return BadRequest(result.ExceptionContentResult);
                }
            }
        }

        // DELETE: api/Bank/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            TransactionResult result;
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                result = await this._bankService.DeleteAsync(id);
                if (result.IsSuccessful)
                {
                    scope.Complete();
                    return new OkResult();
                }
            }
            return BadRequest(result.ExceptionContentResult);
        }

        #endregion /Actions

    }
}
