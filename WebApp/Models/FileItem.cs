namespace WebApp.Models
{
    public class FileItem
    {
        public string Name { get; set; }
        public long Size { get; set; }
        public byte[]? Data { get; set; }
    }
}
