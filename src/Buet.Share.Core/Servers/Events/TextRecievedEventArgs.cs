namespace Buet.Share.Core.Servers.Events
{
    public class TextRecievedEventArgs : EventArgs
    {
        public ConnectingClient Client { get; set; }
        public string Message { get; set; }
        public int MessageLength { get { return Message.Length; } }
    }
}
