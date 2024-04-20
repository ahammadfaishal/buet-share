namespace Buet.Share.Core.Servers.Events
{
    public class ClientDisconnectedEventArgs : EventArgs
    {
        public ConnectingClient Client { get; set; }
    }
}
