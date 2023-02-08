namespace ChristmasTreeDeliveryApp
{
    internal class ChristmasTreeDeliveryApp
    {
        /// <summary>
        /// User interface. 
        /// </summary>
        public void StartService()
        {
            const string deilverATree = "1";
            const string closeProgram = "0";
            try
            {
                while (true)
                {
                    Console.WriteLine("Select option you want:" +
                        "\n[1] deliver a tree" +
                        "\n[0] close program");

                    string? userInput = GetUserInput();

                    switch (userInput)
                    {
                        case deilverATree:
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
                                }
                            }
                            break;
                        case closeProgram:
                            Console.WriteLine("selected 0");
                            break;

                        default:
                            Console.WriteLine("Selected wrong option, please enter [1] or [0]");
                            continue;
                    }
                }
            }
            catch
            {
                // TODO: improve error in the future
                Console.WriteLine("we find error");
            }
        }

        /// <summary>
        /// Gets user input from console.
        /// </summary>
        /// <returns></returns>
        private static string? GetUserInput()
        {
            return Console.ReadLine();
        }
    }
}