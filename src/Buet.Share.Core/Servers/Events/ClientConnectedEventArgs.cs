namespace Buet.Share.Core.Servers.Events
{
    public class ClientConnectedEventArgs : EventArgs
    {
        public ConnectingClient ConnectingClient { get; set; }
    }
}
