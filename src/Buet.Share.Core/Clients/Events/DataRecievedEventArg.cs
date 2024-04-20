namespace Buet.Share.Core.Clients.Events
{
    public class DataRecievedEventArgs : EventArgs
    {
        public BinaryReader Reader { get; set; }
        public double DataSize { get; set; }
        public void Save(string path)
        {
            BinaryWriter writer = new BinaryWriter(new FileStream(path, FileMode.Create, FileAccess.Write));

            for (int i = 0; i < DataSize; i++)
                writer.Write(Reader.ReadByte());
            writer.Flush();
            writer.Close();
            Reader.Close();
        }
    }
}
