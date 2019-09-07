namespace Core.DomainModel.Entities
{
    public class Branch : Entity<int>, IAggregateRoot
    {

        #region Properties

        public int BankId { get; private set; }
        public int Code { get; private set; }
        public string Name { get; private set; }
        public Address Address { get; private set; }

        public virtual Bank Bank { get; private set; }

        #endregion /Properties

        #region Constructors

        private Branch()
        {

        }

        public Branch(int id, int bankId, int code, string name, Address address)
        {
            this.Id = id;
            this.BankId = bankId;
            this.Code = code;
            this.Name = name;
            this.Address = address;
        }

        #endregion /Constructors

        #region Methods

        public void SetAddress(Address address)
        {
            if (string.IsNullOrEmpty(address.CityName))
            {
                throw new CustomException(ExceptionKey.Invalid_Address_City_Required);
            }
            /// & so on 
            this.Address = address;
        }

        #endregion /Methods

    }
}