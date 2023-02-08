using ChristmasTreeDeliveryApp3.Enums;
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

        private static List<OrderedTree> GetOrdersByType(PresentsType presentType)
        {
            // check if file exist
            // open file
            // filter only orders by tree type
            // returns all orders from file (DB)

            var trees = new List<OrderedTree>();

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
            // Get hash id of provided tree
            // create new object
            MD5 md5Hasher = MD5.Create();
            // create new variable
            var newHashId = 0;

            // calculate hash
            var hashed = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(name));
            // get int
            var ivalue = BitConverter.ToInt32(hashed, 0);
            // add int
            newHashId += ivalue;

            // calculate hash
            hashed = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(to));
            // get int
            ivalue = BitConverter.ToInt32(hashed, 0);
            // add int
            newHashId += ivalue;

            StreamReader file = null;

            try
            {
                // read file and check if we not save same record before
                file = new("treeRecord.txt");

                string ln = file.ReadLine();
                while (ln != null)
                {
                    var data = ln.Split(";");
                    var oldHashId = 0;

                    hashed = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(data[0]));
                    ivalue = BitConverter.ToInt32(hashed, 0);
                    oldHashId += ivalue;

                    hashed = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(data[2]));
                    ivalue = BitConverter.ToInt32(hashed, 0);
                    oldHashId += ivalue;

                    if (oldHashId != newHashId)
                    {
                        ln = file.ReadLine();
                        continue;
                    }
                    else
                    {
                        return new ResultAfterSave(true, null);
                    }
                }
            }
            finally
            {
                if (file != null)
                {
                    file.Close();
                }
            }

            // note: we allow to buy only one tree with same tree name for same requestor.

            var saveThis = new OrderedTree
            {
                Name = name,
                Type = type,
                DeliveryAddress = to,
                DeliveryDate = DateTime.UtcNow
            };

            string sss = "";

            // Add text
            sss += saveThis.Name;
            // Add text
            sss += ";";
            // Add text
            sss += saveThis.Type;
            // Add text
            sss += ";";
            // Add text
            sss += saveThis.DeliveryAddress;
            // Add text
            sss += ";";
            // Add text
            sss += saveThis.DeliveryDate.ToString("yyyy-MM-dd HH:mm:ss,fff");
            // Add text
            sss += ";";

            try
            {
                StreamWriter writter = new StreamWriter("treeRecord.txt", append: true);
                writter.WriteLine(sss);
                writter.Close();
            }
            catch
            {
                // File does not exist. create file and try again.
                using StreamWriter sw = File.CreateText("treeRecord.txt");
                StreamWriter writter = new StreamWriter("treeRecord.txt", append: true);
                writter.WriteLine(sss);
                writter.Close();

                return new ResultAfterSave(false, saveThis);
            }

            return new ResultAfterSave(true, saveThis);
        }
    }
}
