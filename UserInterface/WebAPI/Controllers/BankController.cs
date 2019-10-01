﻿using System.Threading.Tasks;
using System.Transactions;
using Core.ApplicationService.Contracts;
using Core.DomainModel.Entities;
using Core.DomainServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
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
        public async Task<IActionResult> GetAsync()
        {
            var banks = await this._bankService.GetAllAsync();
            return new OkObjectResult(banks);
        }

        // GET: api/BankAPI/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var bank = await this._bankService.GetByIdAsync(id);
            return new OkObjectResult(bank);
        }

        // POST: api/BankAPI
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]Bank bank)
        {
            TransactionResult result;
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                result = await this._bankService.InsertAsync(bank);
                if (result.IsSuccessful) {
                    scope.Complete();
                    return CreatedAtAction(nameof(GetAsync), new
                    {
                        id = bank.Id
                    }, bank);
                }
            }
            return BadRequest(result.ExceptionContentResult);
        }

        // PUT: api/BankAPI/
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Bank bank)
        {
            if (bank != null)
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    await this._bankService.UpdateAsync(bank);
                    scope.Complete();
                }
                return new OkResult();
            }
            return new NoContentResult();
        }

        // DELETE: api/BankAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await this._bankService.DeleteAsync(id);
                scope.Complete();
            }
            return new OkResult();
        }

        #endregion /Actions

    }
}
