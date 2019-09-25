using Core.ApplicationService.Contracts;
using Core.ApplicationService.Implementation;
using Core.DomainModel.Entities;
using Core.DomainServices;
using Core.DomainServices.Repositoy;
using System.Linq;

namespace Core.ApplicationService
{
    public class EntityService : IEntityService
    {

        #region Properties

        public IUnitOfWork UnitOfWork { get; private set; }

        #region Repositories

        public ICountryRepository CountryRepository { get; private set; }

        public IBankRepository BankRepository { get; private set; }

        public IBranchRepository BranchRepository { get; private set; }

        #endregion /Repositories

        #region Services

        private ICountryService _countryService;
        public ICountryService CountryService
        {
            get
            {
                if (_countryService == null)
                {
                    _countryService = new CountryService(this);

                }
                return _countryService;
            }
        }

        private IBankService _bankService;
        public IBankService BankService
        {
            get
            {
                if (_bankService == null)
                {
                    _bankService = new BankService(this);

                }
                return _bankService;
            }
        }

        private IBranchService _branchService;
        public IBranchService BranchService
        {
            get
            {
                if (_branchService == null)
                {
                    _branchService = new BranchService(this);

                }
                return _branchService;
            }
        }

        #endregion /Repositories

        #endregion /Properties

        #region Constructors

        public EntityService(IRepository<Country, short> countryRepository, 
            IRepository<Bank, int> bankRepository,
            IRepository<Branch, int> branchRepository,
            IUnitOfWork unitOfWork)
        {
            this.CountryRepository = (countryRepository as ICountryRepository);
            this.BankRepository = (bankRepository as IBankRepository);
            this.BranchRepository = (branchRepository as IBranchRepository);

            this.UnitOfWork = unitOfWork;
        }

        #endregion /Constructors

        #region Methods

        public IReadOnlyRepository<TEntity, TKey> GetRepository<TEntity, TKey>()
            where TEntity : Entity<TKey>
        {
            string entityName = typeof(TEntity).Name;

            var prop = this.GetType().GetProperties()
                .Where(q => q.Name.Equals(entityName + "Repository"))
                .SingleOrDefault();

            return prop.GetValue(this) as IReadOnlyRepository<TEntity, TKey>;
        }

        #endregion /Methods

    }
}
