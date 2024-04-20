using Buet.Share.Core.Clients;
using Buet.Share.Core.Clients.Events;
using Buet.Share.Core.Servers;

namespace Buet.Share.Core
{
    public static class Program
    {
        private static Client client;
        /// <summary>
        /// The entry point of the program, where the program control starts and ends.
        /// </summary>
        /// <param name="args">The command-line arguments.</param>
        public static void Main(string[] args)
        {
            if (args.Length <= 0)
                DisplayHelp();

            switch (args[0])
            {
                case "-s":
                case "--server":
                    new ServerUI(args[1], Convert.ToInt32(args[2])).RunConsole();
                    break;
                case "-c":
                case "--connect":
                    RunClient(args[1], Convert.ToInt32(args[2]));
                    break;
                default:
                    DisplayHelp();
                    break;
            }
        }

        private static void DisplayHelp()
        {
            Console.WriteLine("Buet.Share.Core.exe [FLAG] [IP] [PORT]");
            Console.WriteLine("-c --connect [IP] [PORT]\tConnects to a BuetShare server.");
            Console.WriteLine("-h --help\tDisplays this help and exits.");
            Console.WriteLine("-s --server [IP] [PORT]\tStarts a BuetShare server.");

            Environment.Exit(0);
        }

        private static void RunClient(string ip, int port)
        {
            client = new Client(ip, port);
            client.DataRecieved += Client_OnDataRecieved;
        }

        private static void Client_OnDataRecieved(object sender, DataRecievedEventArgs e)
        {
            Console.WriteLine("Data of size " + e.DataSize + " recieved! Accept? y/n ");
            switch (Console.ReadLine().ToLower())
            {
                case "y":
                case "yes":
                    Console.WriteLine("Enter path to save location: ");
                    e.Save(Console.ReadLine());
                    Console.WriteLine("File saved");
                    break;
                case "n":
                case "no":
                    break;
                default:
                    Client_OnDataRecieved(sender, e);
                    break;
            }
        }
    }
}