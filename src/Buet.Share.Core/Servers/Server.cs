using System.Net.Sockets;
using System.Net;
using Buet.Share.Core.Servers.Events;

namespace Buet.Share.Core.Servers
{
    public class Server
    {
        /// <summary>
        /// The connected clients.
        /// </summary>
        public List<ConnectingClient> ConnectedClients;
        /// <summary>
        /// The accepted clients.
        /// </summary>
        public List<ConnectingClient> AcceptedClients;

        private TcpListener listener;
        /// <summary>
        /// Initializes a new instance of the <see cref="Buet.Share.Core.Servers.Server"/> class.
        /// </summary>
        public Server() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="Buet.Share.Core.Servers.Server"/> class.
        /// </summary>
        /// <param name="ip">Ip.</param>
        /// <param name="port">Port.</param>
        public Server(string ip, int port)
        {
            Start(ip, port);
        }
        /// <summary>
        /// Start the specified ip and port.
        /// </summary>
        /// <param name="ip">Ip.</param>
        /// <param name="port">Port.</param>
        public void Start(string ip, int port)
        {
            ConnectedClients = new List<ConnectingClient>();
            AcceptedClients = new List<ConnectingClient>();

            listener = new TcpListener(IPAddress.Parse(ip), port);
            listener.Start();

            new Thread(() => ListenForConnections()).Start();
        }

        private void ListenForConnections()
        {
            while (true)
            {
                ConnectingClient client = new ConnectingClient(listener.AcceptTcpClient());
                client.RecieveThread = new Thread(() => ListenForMessages(client));
                client.RecieveThread.Start();

                ConnectedClients.Add(client);
                OnClientConnected(new ClientConnectedEventArgs { ConnectingClient = client });
            }
        }

        private void ListenForMessages(ConnectingClient client)
        {
            try
            {
                while (true)
                    OnTextRecieved(new TextRecievedEventArgs { Client = client, Message = client.Input.ReadString() });
            }
            catch (IOException ex)
            {
                OnClientDisconnected(new ClientDisconnectedEventArgs { Client = client });
            }
        }
        /// <summary>
        /// Sends to client.
        /// </summary>
        /// <param name="client">Client.</param>
        /// <param name="path">Path.</param>
        public void SendToClient(ConnectingClient client, string path)
        {
            try
            {
                client.Send(path);
            }
            catch (IOException ex)
            {
                OnClientDisconnected(new ClientDisconnectedEventArgs { Client = client });
            }
        }
        /// <summary>
        /// Sends to client.
        /// </summary>
        /// <param name="client">Client.</param>
        /// <param name="data">Data.</param>
        public void SendToClient(ConnectingClient client, byte[] data)
        {
            try
            {
                client.Send(data);
            }
            catch (IOException ex)
            {
                OnClientDisconnected(new ClientDisconnectedEventArgs { Client = client });
            }
        }
        /// <summary>
        /// Sends to connected clients.
        /// </summary>
        /// <param name="data">Data.</param>
        public void SendToConnectedClients(string path)
        {
            foreach (ConnectingClient client in ConnectedClients)
            {
                try
                {
                    client.Send(path);
                }
                catch (IOException ex)
                {
                    OnClientDisconnected(new ClientDisconnectedEventArgs { Client = client });
                }
            }
        }
        /// <summary>
        /// Sends to connected clients.
        /// </summary>
        /// <param name="data">Data.</param>
        public void SendToConnectedClients(byte[] data)
        {
            foreach (ConnectingClient client in ConnectedClients)
            {
                try
                {
                    client.Send(data);
                }
                catch (IOException ex)
                {
                    OnClientDisconnected(new ClientDisconnectedEventArgs { Client = client });
                }
            }
        }
        /// <summary>
        /// Sends to accepted clients.
        /// </summary>
        /// <param name="data">Data.</param>
        public void SendToAcceptedClients(string path)
        {
            foreach (ConnectingClient client in AcceptedClients)
            {
                try
                {
                    client.Send(path);
                }
                catch (IOException ex)
                {
                    OnClientDisconnected(new ClientDisconnectedEventArgs { Client = client });
                }
            }
        }
        /// <summary>
        /// Sends to accepted clients.
        /// </summary>
        /// <param name="data">Data.</param>
        public void SendToAcceptedClients(byte[] data)
        {
            foreach (ConnectingClient client in AcceptedClients)
            {
                try
                {
                    client.Send(data);
                }
                catch (IOException ex)
                {
                    OnClientDisconnected(new ClientDisconnectedEventArgs { Client = client });
                }
            }
        }
        /// <summary>
        /// Accepts the client.
        /// </summary>
        /// <param name="client">Client.</param>
        public void AcceptClient(ConnectingClient client)
        {
            AcceptedClients.Add(client);
        }
        /// <summary>
        /// Occurs when client connected.
        /// </summary>
        public event EventHandler<ClientConnectedEventArgs> ClientConnected;
        protected virtual void OnClientConnected(ClientConnectedEventArgs e)
        {
            EventHandler<ClientConnectedEventArgs> handler = ClientConnected;
            if (handler != null)
                handler(this, e);
        }
        /// <summary>
        /// Occurs when client disconnected.
        /// </summary>
        public event EventHandler<ClientDisconnectedEventArgs> ClientDisconnected;
        protected virtual void OnClientDisconnected(ClientDisconnectedEventArgs e)
        {
            EventHandler<ClientDisconnectedEventArgs> handler = ClientDisconnected;
            if (handler != null)
                handler(this, e);
        }
        /// <summary>
        /// Occurs when text recieved.
        /// </summary>
        public event EventHandler<TextRecievedEventArgs> TextRecieved;
        protected virtual void OnTextRecieved(TextRecievedEventArgs e)
        {
            EventHandler<TextRecievedEventArgs> handler = TextRecieved;
            if (handler != null)
                handler(this, e);
        }
    }
}
