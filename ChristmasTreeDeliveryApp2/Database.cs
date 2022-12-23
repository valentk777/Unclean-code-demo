namespace ChristmasTreeDeliveryApp2
{
    public class Database
    {
        public Database()
        {

        }

        public void GetAll(PresentsType type)
        {
            switch (type)
            {
                case PresentsType.RedcedarTree:
                    // 
                    break;
                default:
                    throw new EntryPointNotFoundException();
                    break;
            }
        }
    }

    public enum PresentsType
    {
        RedcedarTree = 0,
        CedarTree = 1,
        ConiferTree = 2,
        CypressTree = 3,
        FirTree = 4,
    }
}
