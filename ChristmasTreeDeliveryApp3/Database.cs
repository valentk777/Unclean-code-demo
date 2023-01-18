using System.Security.Cryptography;
using System.Text;

namespace ChristmasTreeDeliveryApp3
{
    public class ResultAfterSave
    {
        public bool IsSaveWasSucessful { get; set; }

        public TreeObjectDtoData? Data { get; set; }

        public ResultAfterSave(bool isSaveWasSucessful, TreeObjectDtoData? data) 
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
        public List<TreeObjectDtoData> GetAllTrees(PresentsType type)
        {
            StreamReader file = null;
            var trees = new List<TreeObjectDtoData>();

            try
            {
                switch (type)
                {
                    case PresentsType.RedcedarTree:
                        if (File.Exists("treeRecord.txt"))
                        {
                            file = new("treeRecord.txt");
                            string ln = file.ReadLine();
                            while (ln != null)
                            {
                                var data = ln.Split(";");

                                if (data[1] == PresentsType.RedcedarTree.ToString())
                                {
                                    trees.Add(new TreeObjectDtoData
                                    {
                                        TreeName = data[0],
                                        TreeType = PresentsType.RedcedarTree,
                                        TreeDeliveredTo = data[2],
                                        TreeDeliveredDate = DateTime.ParseExact(data[3], "yyyy-MM-dd HH:mm:ss,fff",
                                           System.Globalization.CultureInfo.InvariantCulture)
                                    });
                                }

                                ln = file.ReadLine();
                            }
                        }
                        break;
                    case PresentsType.CedarTree:
                        if (File.Exists("treeRecord.txt"))
                        {
                            file = new("treeRecord.txt");
                            string ln = file.ReadLine();
                            while (ln != null)
                            {
                                var data = ln.Split(";");

                                if (data[1] == PresentsType.CedarTree.ToString())
                                {
                                    trees.Add(new TreeObjectDtoData
                                    {
                                        TreeName = data[0],
                                        TreeType = PresentsType.CedarTree,
                                        TreeDeliveredTo = data[2],
                                        TreeDeliveredDate = DateTime.ParseExact(data[3], "yyyy-MM-dd HH:mm:ss,fff",
                                           System.Globalization.CultureInfo.InvariantCulture)
                                    });
                                }

                                ln = file.ReadLine();
                            }
                        }
                        break;
                    case PresentsType.ConiferTree:
                        if (File.Exists("treeRecord.txt"))
                        {
                            file = new("treeRecord.txt");
                            string ln = file.ReadLine();
                            while (ln != null)
                            {
                                var data = ln.Split(";");

                                if (data[1] == PresentsType.ConiferTree.ToString())
                                {
                                    trees.Add(new TreeObjectDtoData
                                    {
                                        TreeName = data[0],
                                        TreeType = PresentsType.ConiferTree,
                                        TreeDeliveredTo = data[2],
                                        TreeDeliveredDate = DateTime.ParseExact(data[3], "yyyy-MM-dd HH:mm:ss,fff",
                                           System.Globalization.CultureInfo.InvariantCulture)
                                    });
                                }

                                ln = file.ReadLine();
                            }
                        }
                        break;
                    case PresentsType.CypressTree:
                        if (File.Exists("treeRecord.txt"))
                        {
                            file = new("treeRecord.txt");
                            string ln = file.ReadLine();
                            while (ln != null)
                            {
                                var data = ln.Split(";");

                                if (data[1] == PresentsType.CypressTree.ToString())
                                {
                                    trees.Add(new TreeObjectDtoData
                                    {
                                        TreeName = data[0],
                                        TreeType = PresentsType.CypressTree,
                                        TreeDeliveredTo = data[2],
                                        TreeDeliveredDate = DateTime.ParseExact(data[3], "yyyy-MM-dd HH:mm:ss,fff",
                                           System.Globalization.CultureInfo.InvariantCulture)
                                    });
                                }

                                ln = file.ReadLine();
                            }
                        }
                        break;
                    case PresentsType.FirTree:
                        if (File.Exists("treeRecord.txt"))
                        {
                            file = new("treeRecord.txt");
                            string ln = file.ReadLine();
                            while (ln != null)
                            {
                                var data = ln.Split(";");

                                if (data[1] == PresentsType.FirTree.ToString())
                                {
                                    trees.Add(new TreeObjectDtoData
                                    {
                                        TreeName = data[0],
                                        TreeType = PresentsType.FirTree,
                                        TreeDeliveredTo = data[2],
                                        TreeDeliveredDate = DateTime.ParseExact(data[3], "yyyy-MM-dd HH:mm:ss,fff",
                                           System.Globalization.CultureInfo.InvariantCulture)
                                    });
                                }

                                ln = file.ReadLine();
                            }
                        }
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

            var saveThis = new TreeObjectDtoData
            {
                TreeName = name,
                TreeType = type,
                TreeDeliveredTo = to,
                TreeDeliveredDate = DateTime.UtcNow
            };

            string sss = "";

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

                return new ResultAfterSave(false, saveThis);
            }

            return new ResultAfterSave(true, saveThis);
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
