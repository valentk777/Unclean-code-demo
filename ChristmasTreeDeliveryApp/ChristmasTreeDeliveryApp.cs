using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChristmasTreeDeliveryApp
{
    internal class ChristmasTreeDeliveryApp
    {
        /// <summary>
        /// This is a default constructor
        /// </summary>
        public ChristmasTreeDeliveryApp()
        {
        }

        public void StartService()
        {
            try
            {
                while (true)
                {
                    Console.WriteLine("Select option you want:" +
                        "\n[1] deliver a tree" +
                        "\n[0] close program");

                    string o = Console.ReadLine();

                    if (o == "1")
                    {
                        try
                        {
                            Console.WriteLine("selected 1");
                        }
                        catch (Exception ex)
                        {
                            // this time we do not want to handle error as normally
                            Console.WriteLine("selected 0");

                            if (File.Exists("error_file.txt"))
                            {
                                StreamWriter file = new("error_file.txt", append: false);
                                file.WriteLine(ex);
                                continue;
                                file.Close();
                            }
                        }
                    }
                    else if (o == "0")
                    {
                        Console.WriteLine("selected 0");
                        break;
                    }
                }
            }
            catch
            {
                // Alex: 2007-05-17.
                // This is juts to make sure your app works oki.
                // TODO: improve error in the future
                Console.WriteLine("we find error");
            }
        }
    }
}
