using System.Collections.Generic;

namespace Core.DomainModel.Entities
{
    public class Address : ValueObject
    {

        #region Properties

        public string CityName { get; private set; }
        public string Street { get; private set; }
        public string BlockNo { get; private set; }
        public string PostalCode { get; private set; }

        public virtual Country Country { get; private set; }

        #endregion /Properties

        #region Constructors

        private Address() { }

        public Address(Country country,
            string cityName,
            string street,
            string blockNo,
            string postalCode)
        {
            this.Country = country;
            this.CityName = cityName;
            this.Street = street;
            this.BlockNo = blockNo;
            this.PostalCode = postalCode;
        }

        #endregion /Constructors

        #region Methods

        protected override IEnumerable<object> GetAtomicValues()
        {
            // Using a yield return statement to return each element one at a time
            yield return this.Country;
            yield return this.CityName;
            yield return this.Street;
            yield return this.BlockNo;
            yield return this.PostalCode;
        }

        #endregion /Methods

    }
}
