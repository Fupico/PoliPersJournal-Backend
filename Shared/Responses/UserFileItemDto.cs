namespace Shared.Responses
{
    public class UserFileItemDto
    {
        public string FileName { get; set; }
        public long FileSize { get; set; }
        public DateTime UploadDate { get; set; }
        public string Url { get; set; }

        public string FileSizeReadable
        {
            get
            {
                if (FileSize >= 1024 * 1024)
                    return $"{FileSize / (1024 * 1024)} MB";
                else
                    return $"{FileSize / 1024} KB";
            }
        }
    }
}
