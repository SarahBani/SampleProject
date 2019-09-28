using System.Threading.Tasks;
using System.Transactions;
using Core.ApplicationService.Contracts;
using Core.DomainModel.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankController : BaseAPIController
    {

        #region Properties

        private readonly IBankService _bankService;

        #endregion /Properties

        #region Constructors

        public BankController(IBankService bankService,
            IWebServiceAssignmentService webServiceAssignmentService)
            : base(webServiceAssignmentService)
        {
            this._bankService = bankService;
        }

        #endregion /Constructors

        #region Actions

        // GET: api/BankAPI
        [HttpGet]
        public async Task<IActionResult> GetAsync() // string token
        {
            var banks = await this._bankService.GetAllAsync();
            return new OkObjectResult(banks);
        }

        // GET: api/BankAPI/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> GetAsync(string token, int id)
        {
            var bank = await this._bankService.GetByIdAsync(id);
            return new OkObjectResult(bank);
        }

        // POST: api/BankAPI
        [HttpPost]
        public async Task<IActionResult> PostAsync(string token, [FromBody]  Bank bank)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await this._bankService.InsertAsync(bank);//.ConfigureAwait(false); not needed in .net Core
                scope.Complete();
            }
            return CreatedAtAction(nameof(GetAsync), new
            {
                id = bank.Id
            }, bank);
        }

        // PUT: api/BankAPI/
        [HttpPut]
        public async Task<IActionResult> Put(string token, [FromBody] Bank bank)
        {
            if (bank != null)
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    await this._bankService.UpdateAsync(bank);//.ConfigureAwait(false); not needed in .net Core
                    scope.Complete();
                }
                return new OkResult();
            }
            return new NoContentResult();
        }

        // DELETE: api/BankAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string token, int id)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await this._bankService.DeleteAsync(id);//.ConfigureAwait(false); not needed in .net Core
                scope.Complete();
            }
            return new OkResult();
        }

        #endregion /Actions

    }
}
