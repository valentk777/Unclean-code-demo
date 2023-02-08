using System.Security.Cryptography;
using System.Text;

namespace ChristmasTreeDeliveryApp.Api.Integration
{
    public class FileDatabase : IDatabase
    {
        public readonly string _databaseName = "treeRecord.txt";

        /// <summary>
        /// RETURN ALL trees by type.
        /// </summary>
        /// <param name="type">type.</param>
        /// <exception cref="EntryPointNotFoundException"></exception>
        public List<OrderedTree> GetAllTrees(PresentsType type)
        {
            // SOLID. Open-close principle issue :(. Make an instances of the class
            switch (type)
            {
                case PresentsType.RedcedarTree:
                case PresentsType.CedarTree:
                case PresentsType.ConiferTree:
                case PresentsType.CypressTree:
                case PresentsType.FirTree:
                    return GetOrdersByType(type);
                default:
                    throw new EntryPointNotFoundException();
            }
        }

        // TODO: Clean current code.
        public List<OrderedTree> GetOrdersByType(PresentsType presentType) =>
            GetAllTrees().Where(x => x.Type == presentType).ToList();

        /// <summary>
        /// This function only save to file new provided request.
        /// </summary>
        /// <param name="name">Name.</param>
        /// <param name="type">Tree type.</param>
        /// <param name="to">Getter</param>
        /// <returns></returns>
        public bool SaveTree(OrderedTree orderedTree)
        {
            var newHashId = GetCustomHashFunction(orderedTree.Name, orderedTree.DeliveryAddress);

            // throw errors if not exist...
            // how can we fix?
            var lines = File.ReadAllLines(_databaseName);

            foreach (var line in lines)
            {
                var data = line.Split(";");
                var newName = data[0];
                var newTo = data[2];
                var existingHashId = GetCustomHashFunction(newName, newTo);

                if (existingHashId == newHashId)
                {
                    return true;
                }
            }

            // note: we allow to buy only one tree with same tree name for same requester.
            try
            {
                File.AppendAllText(_databaseName, orderedTree.ToDatabaseFormat());
            }
            catch
            {
                File.WriteAllText(_databaseName, orderedTree.ToDatabaseFormat());
                return false;
            }

            return true;
        }

        private int GetCustomHashFunction(string name, string to)
        {
            var md5Hasher = MD5.Create();
            var newHashId = 0;

            var hashed = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(name));
            var ivalue = BitConverter.ToInt32(hashed, 0);
            newHashId += ivalue;

            hashed = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(to));
            ivalue = BitConverter.ToInt32(hashed, 0);
            newHashId += ivalue;

            return newHashId;
        }

        public List<OrderedTree> GetAllTrees()
        {
            // Create a function named LoadDatabase (or similar and add database file creation in that function)
            if (!File.Exists(_databaseName))
            {
                return new List<OrderedTree>();
            }

            return File.ReadAllLines(_databaseName).Select(line => ToOrderedTree(line)).ToList();
        }

        private OrderedTree ToOrderedTree(string tree)
        {
            var data = tree.Split(";");

            return new OrderedTree
            {
                Name = data[0],
                Type = PresentsType.ConiferTree,
                DeliveryAddress = data[2],
                // TODO: create extention method 
                DeliveryDate = DateTime.ParseExact(data[3], "yyyy-MM-dd HH:mm:ss,fff",
                    System.Globalization.CultureInfo.InvariantCulture)
            };
        }
    }
}
