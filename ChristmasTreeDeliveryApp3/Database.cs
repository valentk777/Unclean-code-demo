using System.Security.Cryptography;
using System.Text;
using ChristmasTreeDeliveryApp3.DtoData;
using ChristmasTreeDeliveryApp3.Enums;
using static System.Net.WebRequestMethods;
using File = System.IO.File;

namespace ChristmasTreeDeliveryApp3
{
    public class Database
    {

        private void Name(StreamReader? file, PresentsType type, List<TreeObjectDtoData> trees) // didn't decide what name should be
        {
            if (File.Exists("treeRecord.txt"))
            {
                file = new("treeRecord.txt");
                string ln = string.Empty;
                while (file.ReadLine() != null)
                {
                    ln = file.ReadLine();
                    var data = ln.Split(";");

                    if (data[1] == type.ToString())
                    {
                        trees.Add(new TreeObjectDtoData
                        {
                            TreeName = data[0],
                            TreeType = type,
                            TreeDeliveredTo = data[2],
                            TreeDeliveredDate = DateTime.ParseExact(data[3], "yyyy-MM-dd HH:mm:ss,fff",
                               System.Globalization.CultureInfo.InvariantCulture)
                        });
                    }
                }
            }
        }

        /// <summary>
        /// RETURN ALL trees by type.
        /// </summary>
        /// <param name="type">type.</param>
        /// <exception cref="EntryPointNotFoundException"></exception>
        public List<TreeObjectDtoData> AllTrees(PresentsType type)
        {
            StreamReader? file = null;
            var trees = new List<TreeObjectDtoData>();

            try
            {
                switch (type)
                {
                    case PresentsType.RedcedarTree:
                        Name(file, PresentsType.RedcedarTree, trees);

                        break;
                    case PresentsType.CedarTree:
                        Name(file, PresentsType.CedarTree, trees);

                        break;
                    case PresentsType.ConiferTree:
                        Name(file, PresentsType.ConiferTree, trees);

                        break;
                    case PresentsType.CypressTree:
                        Name(file, PresentsType.CypressTree, trees);

                        break;
                    case PresentsType.FirTree:
                        Name(file, PresentsType.FirTree, trees);

                        break;
                    default:
                        throw new EntryPointNotFoundException();
                }
            }

            finally
            {
                if (file != null)
                {
                    file.Close();
                }
            }

            return trees;
        }

        /// <summary>
        /// missing sumary
        /// </summary>
        /// <param name="md5Hasher"></param>
        /// <param name="HashId"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private int SecondName(MD5 md5Hasher, int HashId, string data) // didn't decide what name should be
        {
            // calculate hash
            var hashed = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(data));
            // get int
            var ivalue = BitConverter.ToInt32(hashed, 0);
            // add int
            HashId += ivalue;
            
            return HashId;
        } 

        /// <summary>
        /// This function only save to file new provided request.
        /// </summary>
        /// <param name="name">Name.</param>
        /// <param name="type">Tree type.</param>
        /// <param name="to">Getter</param>
        /// <returns></returns>
        public Tuple<bool, TreeObjectDtoData?> SaveTree(string name, PresentsType type, string to)
        {
            // Get hash id of provided tree
            // create new object
            MD5 md5Hasher = MD5.Create();
            // create new variable
            var newHashId = 0;

            newHashId = SecondName(md5Hasher, newHashId, name);

            newHashId = SecondName(md5Hasher, newHashId, to);

            StreamReader? file = null;

            try
            {
                // read file and check if we not save same record before
                file = new("treeRecord.txt");

                string ln = string.Empty;
                while (file.ReadLine() != null)
                {
                    ln = file.ReadLine();
                    var data = ln.Split(";");
                    var oldHashId = 0;

                    oldHashId = SecondName(md5Hasher, oldHashId, data[0]);

                    oldHashId = SecondName(md5Hasher, oldHashId, data[2]);

                    if (oldHashId == newHashId)
                    {
                        return new Tuple<bool, TreeObjectDtoData?>(true, null);
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

            var saveThis = new TreeObjectDtoData
            {
                TreeName = name,
                TreeType = type,
                TreeDeliveredTo = to,
                TreeDeliveredDate = DateTime.UtcNow
            };

            string sss = ""; // bad variable name

            // Add text
            sss += saveThis.TreeName;
            // Add text
            sss += ";";
            // Add text
            sss += saveThis.TreeType;
            // Add text
            sss += ";";
            // Add text
            sss += saveThis.TreeDeliveredTo;
            // Add text
            sss += ";";
            // Add text
            sss += saveThis.TreeDeliveredDate.ToString("yyyy-MM-dd HH:mm:ss,fff");
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

                return new Tuple<bool, TreeObjectDtoData?>(false, saveThis );
            }

            return new Tuple<bool, TreeObjectDtoData?>(true, saveThis );
        }
    }
}
