﻿using Core.DomainModel.Entities;
using Core.DomainService;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.ApplicationService.Contracts
{
    public interface IBranchService
    {

        Task<Branch> GetByIdAsync(int id);

        Task<int> GetCountByBankIdAsync(int bankId);

        Task<IList<Branch>> GetListByBankIdAsync(int bankId);
        
        Task<TransactionResult> InsertAsync(Branch branch);

        Task<TransactionResult> UpdateAsync(Branch branch);

        Task<TransactionResult> DeleteAsync(int id);

        Task<TransactionResult> DeleteByBankIdAsync(int bankId);

    }
}
