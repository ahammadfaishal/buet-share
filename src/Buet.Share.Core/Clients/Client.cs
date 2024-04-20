using Buet.Share.Core.Clients.Events;
using System.Net.Sockets;

namespace Buet.Share.Core.Clients
{
    public class Client
    {
        private TcpClient client;
        private BinaryWriter output;
        private BinaryReader input;
        /// <summary>
        /// Initializes a new instance of the <see cref="Buet.Share.Core.Clients"/> class.
        /// </summary>
        public Client() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="Buet.Share.Core.Clients"/> class.
        /// </summary>
        /// <param name="ip">Ip.</param>
        /// <param name="port">Port.</param>
        public Client(string ip, int port)
        {
            Connect(ip, port);
        }
        /// <summary>
        /// Connect the specified ip and port.
        /// </summary>
        /// <param name="ip">Ip.</param>
        /// <param name="port">Port.</param>
        public void Connect(string ip, int port)
        {
            client = new TcpClient(ip, port);
            output = new BinaryWriter(client.GetStream());
            input = new BinaryReader(client.GetStream());

            new Thread(() => ListenForData()).Start();
        }

        private void ListenForData()
        {
            while (true)
            {
                double size = Convert.ToDouble(input.ReadString());
                OnDataRecieved(new DataRecievedEventArgs { Reader = input, DataSize = size });
            }
        }
        /// <summary>
        /// Occurs when data recieved.
        /// </summary>
        public event EventHandler<DataRecievedEventArgs> DataRecieved;
        protected virtual void OnDataRecieved(DataRecievedEventArgs e)
        {
            EventHandler<DataRecievedEventArgs> handler = DataRecieved;
            if (handler != null)
                handler(this, e);
        }
    }
}
