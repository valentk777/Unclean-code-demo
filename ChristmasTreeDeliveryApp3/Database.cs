using System.Security.Cryptography;
using System.Text;

namespace ChristmasTreeDeliveryApp3
{
    public class ResultAfterSave
    {
        public bool IsSaveWasSucessful { get; set; }

        public OrderedTree? Data { get; set; }

        public ResultAfterSave(bool isSaveWasSucessful, OrderedTree? data)
        {
            IsSaveWasSucessful = isSaveWasSucessful;
            Data = data;
        }
    }

    public class Database : IDatabase
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

        // TODO: write a method to return all trees.
        // TODO: Split code to files.
        // TODO: Clean current code.

        public static List<OrderedTree> GetOrdersByType(PresentsType presentType)
        {
            // check if file exist
            // open file
            // filter only orders by tree type
            // returns all orders from file (DB)
            var trees = new List<OrderedTree>();

            // Create a function named LoadDatabase (or similar and add database file creation in that function)
            if (!File.Exists("treeRecord.txt"))
            {
                return trees;
            }

            var lines = File.ReadAllLines("treeRecord.txt");

            foreach(var line in lines)
            {
                var data = line.Split(";");

                if (data[1] == presentType.ToString())
                {
                    trees.Add(new OrderedTree
                    {
                        Name = data[0],
                        Type = PresentsType.ConiferTree,
                        DeliveryAddress = data[2],
                        // TODO: create extention method 
                        DeliveryDate = DateTime.ParseExact(data[3], "yyyy-MM-dd HH:mm:ss,fff",
                           System.Globalization.CultureInfo.InvariantCulture)
                    });
                }
            }

            return trees;
        }

        /// <summary>
        /// This function only save to file new provided request.
        /// </summary>
        /// <param name="name">Name.</param>
        /// <param name="type">Tree type.</param>
        /// <param name="to">Getter</param>
        /// <returns></returns>
        public ResultAfterSave SaveTree(string name, PresentsType type, string to)
        {
            var newHashId = GetCustomHashFunction(name, to);

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
                    return new ResultAfterSave(true, null);
                }
            }

            // note: we allow to buy only one tree with same tree name for same requester.
            var order = new OrderedTree(name, type, to);

            try
            {
                File.AppendAllText(_databaseName, order.ToDatabaseFormat());
            }
            catch
            {
                File.WriteAllText(_databaseName, order.ToDatabaseFormat());
                return new ResultAfterSave(false, order);
            }

            return new ResultAfterSave(true, order);
        }

        private static int GetCustomHashFunction(string name, string to)
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
    }

    public enum PresentsType
    {
        RedcedarTree = 0,
        CedarTree = 1,
        ConiferTree = 2,
        CypressTree = 3,
        FirTree = 4,
        Special = 5,
    }
}
