namespace Core.DomainService
{
    public class Page
    {

        #region Constructors

        public Page(int pageNo, int count)
        {
            this.FirstRowIndex = ((pageNo - 1) * count);
            this.Count = count;
        }

        #endregion /Constructors

        #region Properties

        public int FirstRowIndex { get; private set; }

        public int Count { get; private set; }

        #endregion /Properties

    }
}
